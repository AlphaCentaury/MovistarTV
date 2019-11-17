// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IpTviewr.Common.Telemetry;
using IpTviewr.Native;
using IpTviewr.Tools.FirstTimeConfig.Properties;
using IpTviewr.UiServices.Configuration;
using Microsoft.Win32;

namespace IpTviewr.Tools.FirstTimeConfig
{
    internal class Installation
    {
        private static string _redistFolder;

        public static bool Is32BitWindows { get; set; }

        public static AppUiConfiguration LoadRegistrySettings(out InitializationResult initializationResult)
        {
            Is32BitWindows = WindowsBitness.Is32BitWindows();
            var result = AppUiConfiguration.LoadRegistryAppConfiguration(out initializationResult);
#if DEBUG
            _redistFolder = Path.Combine(result.Folders.Base, "Bin\\Redist");
#else
            RedistFolder = Path.Combine(result.Folders.Install, "Redist");
#endif

            return result;
        } // LoadRegistrySettings

        public static string GetProgramFilesAnyFolder()
        {
            try
            {
                return WindowsBitness.Is64BitWindows() ? GetProgramFilesx64Folder() : GetProgramFilesx86Folder();
            }
            catch
            {
                return Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            } // try-catch
        } // GetProgramFilesAnyFolder

        public static string GetProgramFilesx86Folder()
        {
            var folder = KnownFolders.GetKnownFolder(KnownFolders.System.ProgramFilesX86, KnownFolders.Flags.None);
            return Environment.ExpandEnvironmentVariables(folder);
        } // GetProgramFilesx86Folder

        public static string GetProgramFilesx64Folder()
        {
            var folder = KnownFolders.GetKnownFolder(KnownFolders.System.ProgramFilesX64, KnownFolders.Flags.None);
            return Environment.ExpandEnvironmentVariables(folder);
        } // GetProgramFilesx64Folder

        public static string GetCurrentUserVideosFolder()
        {
            var folder = KnownFolders.GetKnownFolder(KnownFolders.CurrentUser.Videos, KnownFolders.Flags.None);
            return Environment.ExpandEnvironmentVariables(folder);
        } // GetCurrentUserVideosFolder

        public static string GetTestMedia()
        {
            string folder;
            int step;

            step = 1;
            while (true)
            {
                try
                {
                    switch (step)
                    {
                        case 1:
                            folder = KnownFolders.GetKnownFolder(KnownFolders.Common.SampleVideos, KnownFolders.Flags.None);
                            break;
                        case 2:
                            folder = KnownFolders.GetKnownFolder(KnownFolders.CurrentUser.Videos, KnownFolders.Flags.None);
                            break;
                        case 3:
                            folder = KnownFolders.GetKnownFolder(KnownFolders.Common.SampleMusic, KnownFolders.Flags.None);
                            break;
                        case 4:
                            folder = KnownFolders.GetKnownFolder(KnownFolders.CurrentUser.Music, KnownFolders.Flags.None);
                            break;
                        default:
                            return null;
                    } // switch

                    var files = Directory.GetFiles(folder);
                    var q = from file in files
                        let ext = Path.GetExtension(file).ToLowerInvariant()
                        where ext == ".wmv" || ext == ".mp4" || ext == ".mkv" || ext == ".avi" ||
                              ext == ".wma" || ext == ".mp3" || ext == ".aac" || ext == ".wav"
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
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true,
                        ErrorDialog = true,
                        ErrorDialogParentHandle = parent.Handle
                    };
                    process.Start();
                } // using process
            }
            catch (Exception ex)
            {
                MessageBox.Show(parent,
                    string.Format(Texts.OpenUrlError, url, ex),
                    parent.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            } // try-catch
        } // OpenUrl

        internal static string Launch(IWin32Window parent, string basePath, string programFile)
        {
            var filename = programFile == null ? basePath : Path.Combine(basePath, programFile);

            try
            {
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = filename,
                        UseShellExecute = true,
                        ErrorDialog = true,
                        ErrorDialogParentHandle = parent != null ? parent.Handle : IntPtr.Zero
                    };
                    process.Start();
                    return null;
                } // using process
            }
            catch (Exception ex)
            {
                return string.Format(Texts.LaunchProgramException, filename, ex);
            } // try-catch
        } // Launch

        private static bool IsAssemblyInstalled(string assemblyName, out Version assemblyVersion, out Version fileVersion)
        {
            AppDomain domain;

            assemblyVersion = new Version();
            fileVersion = new Version();
            domain = null;

            try
            {
                string location;

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
                if (domain != null) AppDomain.Unload(domain);
            } // try-catch
        } // IsAssemblyInstalled

        #region IsComponentInstalled

        public static bool IsNetInstalled(out string message)
        {
            var keys = new[]
            {
                "SOFTWARE",
                "Microsoft",
                "NET Framework Setup",
                "NDP",
                "v3.5"
            };

            try
            {
                var key = Registry.LocalMachine;
                foreach (var keyName in keys)
                {
                    var newKey = key.OpenSubKey(keyName);
                    key.Close();
                    key = newKey;
                    if (key == null)
                    {
                        message = Texts.IsNetInstalledNotInstalled;
                        return false;
                    }
                } // foreach

                var value = key.GetValue("Install");
                if (value == null)
                {
                    message = string.Format(Texts.IsNetInstalledKeyValueNotFound, "Install");
                    return false;
                }

                if (!int.TryParse(value.ToString(), out var intValue) || intValue < 0)
                {
                    message = string.Format(Texts.IsNetInstalledKeyValueLessThan, "Install", "1");
                    return false;
                } // if

                value = key.GetValue("SP");
                if (value == null)
                {
                    message = string.Format(Texts.IsNetInstalledKeyValueNotFound, "SP");
                    return false;
                }

                if (!int.TryParse(value.ToString(), out intValue) || intValue < 0)
                {
                    message = string.Format(Texts.IsNetInstalledKeyValueLessThan, "SP", "1");
                    return false;
                } // if

                message = Texts.IsNetInstalledOk;
                return true;
            }
            catch (Exception ex)
            {
                message = string.Format(Texts.IsNetInstalledException, ex);
                return false;
            } // try-catch
        } // IsNetInstalled

        public static bool IsEmbInstalled(out string message)
        {
            try
            {
                // Solve bug per work item 1757
                var found = IsAssemblyInstalled(Resources.EmbComponentAssemblyName, out _, out var fileVersion);

                if (!found)
                {
                    message = Texts.IsEmbInstalledNotInstalled;
                    return false;
                } // if

                message = string.Format(Texts.IsEmbInstalledOk, fileVersion);
                return true;
            }
            catch (Exception ex)
            {
                message = string.Format(Texts.IsEmbInstalledException, ex);
                return false;
            } // try-catch
        } // IsEmbNotInstalled

        public static bool IsSqlCeInstalled(out string message)
        {
            try
            {
                var found = IsAssemblyInstalled(Resources.SqlCeComponentAssemblyName, out _, out var fileVersion);

                if (!found)
                {
                    message = Texts.IsSqlCeInstalledNotInstalled;
                    return false;
                } // if

                message = string.Format(Texts.IsSqlCeInstalledOk, fileVersion);
                return true;
            }
            catch (Exception ex)
            {
                message = string.Format(Texts.IsSqlCeInstalledException, ex);
                return false;
            } // try-catch
        } // IsSqlCeInstalled

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
                var vlcFileVersion = new Version(vlcVersion.FileMajorPart, vlcVersion.FileMinorPart, vlcVersion.FileBuildPart, vlcVersion.FilePrivatePart);
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
                var vlcLibFileVersion = new Version(vlcLibVersion.FileMajorPart, vlcLibVersion.FileMinorPart, vlcLibVersion.FileBuildPart, vlcLibVersion.FilePrivatePart);
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
                message = string.Format(Texts.IsVlcInstalledException, ex);
                return false;
            } // try-catch
        } // IsVlcInstalled

        public static bool TestVlcInstallation(out string message, string path, string testVideoPath)
        {
            try
            {
                using (var process = new Process())
                {
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = path,
                        Arguments = testVideoPath != null ? $"\"{testVideoPath}\"" : null,
                        UseShellExecute = false
                    };
                    process.Start();

                    // TODO: WaitForExit in a non-blocking manner
                    // process.WaitForExit();
                } // using process
            }
            catch (Exception ex)
            {
                message = string.Format(Texts.TestVlcInstallationException, ex);
                return false;
            } // try-catch

            message = Texts.TestVlcInstallationOk;
            return true;
        } // TestVlcInstallation

        #endregion

        #region Redist setup

        public static bool CheckRedistFile(string file64Bit, string file32Bit)
        {
            var file = GetRedistFileFullPath(file64Bit, file32Bit);
            return File.Exists(file);
        } // CheckRedistFile

        public static void PromptDownloadFromVendor(Form owner, string vendor, string file64Bit, string file32Bit)
        {
            string text;

            if (Is32BitWindows)
                text = string.Format(Texts.DownloadFromVendor32bit, vendor, file32Bit);
            else
                text = string.Format(Texts.DownloadFromVendor64bit, vendor, file64Bit);

            MessageBox.Show(owner, text, owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        } // PromptDownloadFromVendor

        public static bool RedistSetup(Form owner, string file64Bit, string file32Bit, string productName, Label labelProduct, Action<bool> setupExitCallback)
        {
            var filename = GetRedistFileFullPath(file64Bit, file32Bit);

            Exception exception = null;
            try
            {
                var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = filename,
                        UseShellExecute = true,
                        ErrorDialog = true,
                        ErrorDialogParentHandle = owner != null ? owner.Handle : IntPtr.Zero
                    },
                    EnableRaisingEvents = true
                };
                process.Exited += (o, e) =>
                {
                    var exitCode = process.ExitCode;
                    process.Dispose();

                    owner?.BeginInvoke(new RedistSetupProcessExitedDelegate(RedistSetup_ProcessExited), exitCode, owner, productName, labelProduct, setupExitCallback);
                };
                process.Start();

                labelProduct.Text = string.Format(Texts.RedistSetupInstalling, productName);

                return true;
            }
            catch (Win32Exception ex)
            {
                if (ex.NativeErrorCode != 1223) // Operation cancelled by user
                    exception = ex;
            }
            catch (Exception ex)
            {
                exception = ex;
            } // try-catch

            if ((exception == null) || (owner == null)) return false;

            var message = string.Format(Texts.LaunchSetupException, filename, labelProduct.Text, exception);
            MessageBox.Show(owner, message, owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);

            return false;
        } // RedistSetup

        private delegate void RedistSetupProcessExitedDelegate(int exitCode, Form owner, string productName, Label labelProduct, Action<bool> setupExitCallback);

        private static void RedistSetup_ProcessExited(int exitCode, Form owner, string productName, Label labelProduct, Action<bool> setupExitCallback)
        {
            var format = exitCode == 0 ? Texts.LaunchSetupSuccess : Texts.LaunchSetupError;
            var message = string.Format(format, productName, exitCode);
            MessageBox.Show(owner, message, owner.Text, MessageBoxButtons.OK, exitCode == 0 ? MessageBoxIcon.Information : MessageBoxIcon.Warning);

            labelProduct.Text = productName;

            setupExitCallback(exitCode == 0);
        } // RedistSetup_ProcessExited

        private static string GetRedistFileFullPath(string file64Bit, string file32Bit)
        {
            var file = Is32BitWindows ? file32Bit : file64Bit;
            file.Replace('\\', Path.DirectorySeparatorChar);
            file = Path.Combine(_redistFolder, file);

            return file;
        } // GetRedistFileFullPath

        #endregion

        #region Firewall installation

        public static InitializationResult RunSelfForFirewall(string binPath, string vlcPath)
        {
            int exitCode;

            try
            {
                BasicGoogleTelemetry.SendScreenHit("FirewallForm");

                var arguments = new StringBuilder();
                arguments.AppendFormat("/ForceUiCulture:{0}", CultureInfo.CurrentUICulture.Name);
                arguments.Append(" /firewall");
                if (!string.IsNullOrEmpty(binPath))
                {
                    arguments.Append(" \"");
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
                    process.StartInfo = new ProcessStartInfo
                    {
                        FileName = Application.ExecutablePath,
                        Arguments = arguments.ToString(),
                        UseShellExecute = true,
                        Verb = "runas"
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
                    BasicGoogleTelemetry.SendScreenHit("FirewallForm: UACancel");
                    return new InitializationResult(Texts.FirewallUserCancel);
                }

                BasicGoogleTelemetry.SendScreenHit("FirewallForm: Exception");
                return new InitializationResult(win32);
            }
            catch (Exception ex)
            {
                BasicGoogleTelemetry.SendScreenHit("FirewallForm: Exception");
                BasicGoogleTelemetry.SendExtendedExceptionHit(ex, true, "FirewallForm: Execute", "FirewallForm");
                return new InitializationResult(ex);
            } // try-catch

            if (exitCode == 0)
            {
                BasicGoogleTelemetry.SendScreenHit("FirewallForm: Ok");
                return new InitializationResult(Texts.FirewallOk)
                {
                    IsOk = true
                };
            }

            if (exitCode > 0)
            {
                BasicGoogleTelemetry.SendScreenHit("FirewallForm: Cancel");
                return new InitializationResult(Texts.FirewallUserCancel);
            }

            BasicGoogleTelemetry.SendScreenHit("FirewallForm: " + exitCode);
            return new InitializationResult((string) null);
        } // RunSelfForFirewall

        public static bool ConfigureFirewall(string binPath, string vlcPath, out string message)
        {
            WindowsFirewall firewall = null;
            try
            {
                firewall = new WindowsFirewall();

                if (binPath != null)
                {
                    binPath = Path.GetDirectoryName(binPath);
                    var programs = Resources.FirewallProgramList.Split(new[] {"\r\n"}, StringSplitOptions.RemoveEmptyEntries);
                    foreach (var program in programs)
                    {
                        var programPath = Path.Combine(binPath, program);
                        var fileVersionInfo = FileVersionInfo.GetVersionInfo(programPath);
                        var name = string.Format(Resources.FirewallRulePrefix, fileVersionInfo.OriginalFilename);
                        var description = string.Format(Texts.FirewallDvbIpTvRuleDescription,
                            fileVersionInfo.OriginalFilename, "{0}", SolutionVersion.AssemblyProduct);

                        // for reasons unknown the path can not contain the '~' symbol!!
                        // before discovering this, the installation path was \Documents\IPTV\MovistarTV~1.0~Wolf424\bin\
                        // so the WiX setup needs to be changed to remove the '~' from the path name
                        firewall.AllowProgram(programPath, name, description);
                    } // foreach program
                } // if

                if (vlcPath != null) firewall.AllowProgram(vlcPath, string.Format(Resources.FirewallRulePrefix, "VLC media player"), Texts.FirewallVlcRuleDescription);
            }
            catch (Exception ex)
            {
                message = ex.ToString();
                return false;
            }
            finally
            {
                firewall?.Dispose();
            } // try-catch-finally

            message = null;
            return true;
        } // ConfigureFirewall

        #endregion
    } // class Installation
} // namespace