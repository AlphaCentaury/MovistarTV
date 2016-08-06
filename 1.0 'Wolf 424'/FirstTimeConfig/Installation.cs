// Copyright (C) 2014-2015, Codeplex user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using Microsoft.Win32;
using Project.DvbIpTv.Tools.FirstTimeConfig.Properties;
using Project.DvbIpTv.UiServices.Configuration;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace Project.DvbIpTv.Tools.FirstTimeConfig
{
    internal class Installation
    {
        public static AppUiConfiguration LoadRegistrySettings(out InitializationResult initializationResult)
        {
            return AppUiConfiguration.LoadRegistryAppConfiguration(out initializationResult);
        } // LoadRegistrySettings

        public static bool IsNetInstalled(out string message)
        {
            RegistryKey key;
            object value;
            int intValue;

            string[] keys = new string[]
            {
                "SOFTWARE",
                "Microsoft",
                "NET Framework Setup",
                "NDP",
                "v3.5"
            };

            try
            {
                key = Microsoft.Win32.Registry.LocalMachine;
                foreach (var keyName in keys)
                {
                    var newKey = key.OpenSubKey(keyName);
                    key.Close();
                    key = newKey;
                    if (key == null) { message = Properties.Texts.IsNetInstalledNotInstalled; return false; }
                } // foreach

                value = key.GetValue("Install");
                if (value == null) { message = string.Format(Properties.Texts.IsNetInstalledKeyValueNotFound, "Install"); return false; }
                intValue = 0;
                if ((!int.TryParse(value.ToString(), out intValue)) || (intValue < 0))
                {
                    message = string.Format(Properties.Texts.IsNetInstalledKeyValueLessThan, "Install", "1"); return false;
                } // if

                value = key.GetValue("SP");
                if (value == null) { message = string.Format(Properties.Texts.IsNetInstalledKeyValueNotFound, "SP"); return false; }
                intValue = 0;
                if ((!int.TryParse(value.ToString(), out intValue)) || (intValue < 0))
                {
                    message = string.Format(Properties.Texts.IsNetInstalledKeyValueLessThan, "SP", "1"); return false;
                } // if

                message = Properties.Texts.IsNetInstalledOk;
                return true;
            }
            catch (Exception ex)
            {
                message = string.Format(Properties.Texts.IsNetInstalledException, ex.ToString());
                return false;
            } // try-catch
        } // IsNetInstalled

        public static bool IsEmbInstalled(out string message)
        {
            try
            {
                Version assemblyVersion, fileVersion;

                var found = IsAssemblyInstalled(Resources.EmbComponentAssemblyName, out assemblyVersion, out fileVersion);

                if (!found)
                {
                    message = Properties.Texts.IsEmbInstalledNotInstalled;
                    return false;
                } // if

                message = string.Format(Texts.IsEmbInstalledOk, fileVersion);
                return true;
            }
            catch (Exception ex)
            {
                message = string.Format(Texts.IsEmbInstalledException, ex.ToString());
                return false;
            } // try-catch
        }  // IsEmbInstalled

        public static bool IsVlcInstalled(out string message, ref string path)
        {
            try
            {
                // locate VLC at it's default location
                if (string.IsNullOrEmpty(path))
                {
                    var programFiles = GetProgramFilesAnyFolder();
                    path = Path.Combine(programFiles, Resources.VlcDefaultLocation);
                } // if

                // file exists?
                if (!File.Exists(path))
                {
                    message = string.Format(Texts.IsVlcInstalledNotInstalled, path);
                    return false;
                } // if

                // check VLC.exe file version
                var vlcVersion = FileVersionInfo.GetVersionInfo(path);
                var vlcFileVersion = new Version(vlcVersion.FileMajorPart, vlcVersion.FileMinorPart, vlcVersion.FileBuildPart);
                var expectedVersion = new Version(Resources.VlcExeTargetVersion);
                if (vlcFileVersion < expectedVersion)
                {
                    message = string.Format(Texts.IsVlcInstalledVersion, expectedVersion, vlcFileVersion);
                    return false;
                } // if

                // locate vlclib.dll
                var vlcLibPath = Path.Combine(Path.GetDirectoryName(path), Resources.VlcLibFilename);
                if (!File.Exists(vlcLibPath))
                {
                    message = string.Format(Texts.IsVlcLibInstalledNotInstalled, vlcLibPath);
                    return false;
                } // if

                // check VLC.exe file version
                var vlcLibVersion = FileVersionInfo.GetVersionInfo(vlcLibPath);
                var vlcLibFileVersion = new Version(vlcLibVersion.FileMajorPart, vlcLibVersion.FileMinorPart, vlcLibVersion.FileBuildPart);
                var expectedVlcLibVersion = new Version(Resources.VlcLibTargetVersion);
                if (vlcLibFileVersion < expectedVlcLibVersion)
                {
                    message = string.Format(Texts.IsVlcLibInstalledVersion, expectedVlcLibVersion, vlcLibFileVersion);
                    return false;
                } // if

                message = string.Format(Texts.IsVlcInstalledOk, vlcFileVersion, vlcLibFileVersion);
                return true;
            }
            catch (Exception ex)
            {
                message = string.Format(Texts.IsVlcInstalledException, ex.ToString());
                return false;
            } // try-catch
        } // IsVlcInstalled

        public static bool TestVlcInstallation(out string message, string path, string testVideoPath)
        {
            try
            {
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo()
                    {
                        FileName = path,
                        Arguments = (testVideoPath != null) ? string.Format("\"{0}\"", testVideoPath) : null,
                        UseShellExecute = false,
                    };
                    process.Start();

                    // TODO: WaitForExit in a non-blocking manner
                    // process.WaitForExit();
                } // using process
            }
            catch (Exception ex)
            {
                message = string.Format(Properties.Texts.TestVlcInstallationException, ex.ToString());
                return false;
            } // try-catch

            message = Properties.Texts.TestVlcInstallationOk;
            return true;
        } // IsVlcInstalled

        public static InitializationResult RunSelfForFirewall(string binPath, string vlcPath)
        {
            int exitCode;

            try
            {
                var arguments = new StringBuilder();
                arguments.AppendFormat("/ForceUiCulture:{0}", CultureInfo.CurrentUICulture.Name);
                arguments.Append(" /firewall");
                if (!string.IsNullOrEmpty(binPath))
                {
                    arguments.Append (" \"");
                    arguments.AppendFormat("/decoder:{0}", binPath);
                    // this trick is to avoid a nasty 'feature' of .NET (or even Windows) when parsing arguments
                    // The bin path ends with '\' and if followed by '"', then it will be interpreted as '"'
                    // ["/decoder:foo\bar\" "/vlc:C:\Program Files\foo\bar.exe"] is incorrectly interpreted as [/decoder:foo\bar" /vlc:C:\Program] and [Files\foo\bar.exe]
                    // WARNING: be sure to call Path.GetDirectoryName() before using the path to remove '*.exe'
                    arguments.Append("*.exe");
                    arguments.Append("\"");
                } // if
                if (!string.IsNullOrEmpty(vlcPath))
                {
                    arguments.Append(" \"");
                    arguments.AppendFormat("/vlc:{0}", vlcPath);
                    arguments.Append("\"");
                } // if

                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo()
                    {
                        FileName = Application.ExecutablePath,
                        Arguments = arguments.ToString(),
                        UseShellExecute = true,
                        Verb = "runas",
                    };
                    process.Start();
                    process.WaitForExit();
                    exitCode = process.ExitCode;
                } // using process
            }
            catch (Win32Exception win32)
            {
                if (win32.NativeErrorCode == 1223) // operation cancelled by user
                {
                    return new InitializationResult(Texts.FirewallUserCancel);
                }
                else
                {
                    return new InitializationResult(win32);
                } // if-else
            }
            catch (Exception ex)
            {
                return new InitializationResult(ex);
            } // try-catch

            if (exitCode == 0)
            {
                return new InitializationResult(Texts.FirewallOk)
                {
                    IsOk = true
                };
            }
            else if (exitCode > 0)
            {
                return new InitializationResult(Texts.FirewallUserCancel);
            }
            else
            {
                return new InitializationResult((string)null);
            } // if-else
        } // RunSelfForFirewall

        public static bool ConfigureFirewall(string binPath, string vlcPath, out string message)
        {
            WindowsFirewall firewall;

            firewall = null;
            try
            {
                firewall = new WindowsFirewall();

                if (binPath != null)
                {
                    binPath = Path.GetDirectoryName(binPath);
                    var programs = Resources.FirewallProgramList.Split(new string[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var program in programs)
                    {
                        var programPath = Path.Combine(binPath, program);
                        var fileVersionInfo = FileVersionInfo.GetVersionInfo(programPath);
                        var name = string.Format(Resources.FirewallRulePrefix, fileVersionInfo.OriginalFilename);
                        var description = string.Format("Allow {0} (part of the DVB-IPTV v1.0 'Wolf 424' virtual decoder) {1} inbound connections",
                            fileVersionInfo.OriginalFilename, "{0}");

                        // for reasons unknown the path can not contain the '~' symbol!!
                        // before discovering this, the installation path was \Documents\DVB-IPTV\MovistarTV~1.0~Wolf424\bin\
                        // so the WiX setup needs to be changed to remove the '~' from the path name
                        firewall.AllowProgram(programPath, name, description);
                    } // foreach program
                } // if

                if (vlcPath != null)
                {
                    firewall.AllowProgram(vlcPath, string.Format(Resources.FirewallRulePrefix, "VLC media player"), "Allow VLC media player {0} inbound connections");
                } // if
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                return false;
            }
            finally
            {
                if (firewall != null) firewall.Dispose();
            }  // try-catch-finally

            message = null;
            return true;
        } // ConfigureFirewall

        public static string GetProgramFilesAnyFolder()
        {
            try
            {
                return Installation.GetProgramFilesx86Folder();
            }
            catch
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            } // try-catch
        } // GetProgramFilesAnyFolder

        public static string GetProgramFilesx86Folder()
        {
            var folder = KnownFolders.GetKnownFolder(KnownFolders.System.ProgramFiles_x86, KnownFolders.Flags.None);
            return System.Environment.ExpandEnvironmentVariables(folder);
        } // GetProgramFilesx86Folder

        public static string GetCurrentUserVideosFolder()
        {
            var folder = KnownFolders.GetKnownFolder(KnownFolders.CurrentUser.Videos, KnownFolders.Flags.None);
            return System.Environment.ExpandEnvironmentVariables(folder);
        } // GetCurrentUserVideosFolder

        public static string GetTestMedia()
        {
            string folder;
            int step;

            step = 1;
            folder = null;
            while (true)
            {
                try
                {
                    switch (step)
                    {
                        case 1: folder = KnownFolders.GetKnownFolder(KnownFolders.Common.SampleVideos, KnownFolders.Flags.None); break;
                        case 2: folder = KnownFolders.GetKnownFolder(KnownFolders.CurrentUser.Videos, KnownFolders.Flags.None); break;
                        case 3: folder = KnownFolders.GetKnownFolder(KnownFolders.Common.SampleMusic, KnownFolders.Flags.None); break;
                        case 4: folder = KnownFolders.GetKnownFolder(KnownFolders.CurrentUser.Music, KnownFolders.Flags.None); break;
                        default:
                            return null;
                    } // switch

                    var files = Directory.GetFiles(folder);
                    var q = from file in files
                            let ext = Path.GetExtension(file).ToLowerInvariant()
                            where ((ext == ".wmv") || (ext == ".mp4") || (ext == ".mkv") || (ext == ".avi") ||
                                (ext == ".wma") || (ext == ".mp3") || (ext == ".aac") || (ext == ".wav"))
                            select file;
                    var media = q.FirstOrDefault();
                    if (media != null) return media;
                }
                catch
                {
                    // ignore
                } // try-catch
                step++;
            } // while
        } // GetTestMedia

        public static void OpenUrl(Form parent, string url)
        {
            try
            {
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo()
                    {
                        FileName = url,
                        UseShellExecute = true,
                        ErrorDialog = true,
                        ErrorDialogParentHandle = parent.Handle,
                    };
                    process.Start();
                } // using process
            }
            catch (Exception ex)
            {
                MessageBox.Show(parent,
                    string.Format(Properties.Texts.OpenUrlError, url, ex.ToString()),
                    parent.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            } // try-catch
        } // OpenUrl

        internal static string Launch(IWin32Window parent, string basePath, string programFile)
        {
            try
            {
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo()
                    {
                        FileName = Path.Combine(basePath, programFile),
                        UseShellExecute = true,
                        ErrorDialog = true,
                        ErrorDialogParentHandle = parent.Handle,
                    };
                    process.Start();
                    return null;
                } // using process
            }
            catch (Exception ex)
            {
                return ex.ToString();
            } // try-catch
        } // Launch

        private static bool IsAssemblyInstalled(string assemblyName, out Version assemblyVersion, out Version fileVersion)
        {
            AppDomain domain;
            string location;
            
            assemblyVersion = new Version();
            fileVersion = new Version();
            domain = null;
            location = null;

            try
            {
                domain = AppDomain.CreateDomain("AssemblyLoadTest");
                try
                {
                    var assembly = domain.Load(assemblyName);
                    assemblyVersion = assembly.GetName().Version;
                    location = assembly.Location;
                }
                catch
                {
                    return false;
                } // try-finally

                var fileVersionInfo = FileVersionInfo.GetVersionInfo(location);
                fileVersion = new Version(fileVersionInfo.FileMajorPart, fileVersionInfo.FileMinorPart, fileVersionInfo.FileBuildPart, fileVersionInfo.FilePrivatePart);

                return true;
            }
            finally
            {
                if (domain != null)
                {
                    AppDomain.Unload(domain);
                } // if
            } // try-catch
        } // IsAssemblyInstalled
    } // class Installation
} // namespace
