// ==============================================================================
// 
//   Copyright (C) 2014-2020, GitHub/Codeplex user AlphaCentaury
//   All rights reserved.
// 
//     See 'LICENSE.MD' file (or 'license.txt' if missing) in the project root
//     for complete license information.
// 
//   http://www.alphacentaury.org/movistartv
//   https://github.com/AlphaCentaury
// 
// ==============================================================================

using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using IpTviewr.Common.Configuration;
using IpTviewr.Common.Telemetry;
using IpTviewr.Native;
using IpTviewr.Tools.FirstTimeConfig.Properties;
using IpTviewr.UiServices.Configuration;
using JetBrains.Annotations;
using Microsoft.Win32;

namespace IpTviewr.Tools.FirstTimeConfig
{
    internal class Installation
    {
        private static string _redistFolder;

        public static bool Is32BitWindows { get; set; }

        public static AppUiConfigurationFolders LoadFolders(out InitializationResult initializationResult)
        {
            Is32BitWindows = WindowsBitness.Is32BitWindows();
            var folders = AppConfig.LoadFoldersConfiguration(out initializationResult);
#if DEBUG
            _redistFolder = Path.Combine(folders.Base, "Bin\\Redist");
#else
            _redistFolder = Path.Combine(folders.Install, "Redist");
#endif
            return folders;
        } // LoadFolders

        public static void GetProgramFilesFolder([NotNull] out string folder, [CanBeNull] out string altFolder)
        {
            folder = Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles);
            altFolder = Is32BitWindows ? null : Environment.GetFolderPath(Environment.SpecialFolder.ProgramFilesX86);
        } // GetProgramFilesAnyFolder

        public static string GetTestMedia()
        {
            var step = 1;
            while (true)
            {
                try
                {
                    var folder = step switch
                    {
                        1 => Environment.GetFolderPath(Environment.SpecialFolder.CommonVideos),
                        2 => Environment.GetFolderPath(Environment.SpecialFolder.MyVideos),
                        3 => Environment.GetFolderPath(Environment.SpecialFolder.CommonMusic),
                        4 => Environment.GetFolderPath(Environment.SpecialFolder.MyMusic),
                        _ => null,
                    }; // switch

                    switch (folder)
                    {
                        case "":
                            continue;
                        case null:
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
                using var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = url,
                        UseShellExecute = true,
                        ErrorDialog = true,
                        ErrorDialogParentHandle = parent.Handle
                    }
                };
                process.Start();
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
                using var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = filename,
                        UseShellExecute = true,
                        ErrorDialog = true,
                        ErrorDialogParentHandle = parent?.Handle ?? IntPtr.Zero
                    }
                };
                process.Start();
                return null;
            }
            catch (Exception ex)
            {
                return string.Format(Texts.LaunchProgramException, filename, ex);
            } // try-catch
        } // Launch

        private static bool IsAssemblyInstalled(string assemblyName, out Version assemblyVersion, out Version fileVersion)
        {
            assemblyVersion = new Version();
            fileVersion = new Version();
            AppDomain domain = null;

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

        public static bool IsNet35Installed(out string message)
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
                    if (key != null) continue;

                    message = Texts.IsNetInstalledNotInstalled;
                    return false;
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
        } // IsNet35Installed

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

        public static bool IsVlcInstalled(out string message, ref string path, ref bool isX86OnX64)
        {
            try
            {
                bool exists;

                // locate VLC at it's default location
                GetProgramFilesFolder(out var programFiles, out var programFiles86);

                if (string.IsNullOrEmpty(path))
                {
                    path = Path.Combine(programFiles, Resources.VlcDefaultLocation);
                    exists = File.Exists(path);
                    isX86OnX64 = false;

                    if (!exists && (programFiles86 != null))
                    {
                        // try x86 version
                        path = Path.Combine(programFiles86, Resources.VlcDefaultLocation);
                        exists = File.Exists(path);

                        if (exists)
                        {
                            isX86OnX64 = true;
                        }
                        else
                        {
                            // revert path to Program Files
                            path = Path.Combine(programFiles, Resources.VlcDefaultLocation);
                        } // if-else
                    } // if
                }
                else
                {
                    exists = File.Exists(path);
                    if (exists)
                    {
                        isX86OnX64 = (programFiles86 != null) && (path.StartsWith(programFiles86, StringComparison.CurrentCultureIgnoreCase));
                    } // if
                } // if-else

                if (!exists)
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
                using var process = new Process
                {
                    StartInfo = new ProcessStartInfo
                    {
                        FileName = path,
                        Arguments = testVideoPath != null ? $"\"{testVideoPath}\"" : "",
                        UseShellExecute = false
                    }
                };
                process.Start();

                // TODO: WaitForExit in a non-blocking manner
                // process.WaitForExit();
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
            var text = string.Format(Is32BitWindows ? Texts.DownloadFromVendor32bit : Texts.DownloadFromVendor64bit,
                vendor, Is32BitWindows ? file32Bit : file64Bit);

            MessageBox.Show(owner, text, owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Information);
        } // PromptDownloadFromVendor

        public static bool RedistSetup([NotNull] Form owner, string file64Bit, string file32Bit, string productName, Label labelProduct, Action<int> setupExitCallback)
        {
            var filename = GetRedistFileFullPath(file64Bit, file32Bit);

            try
            {
                LaunchSetup(owner, filename, productName, labelProduct, RedistSetup_ProcessExited, setupExitCallback);

                return true;
            }
            catch (Win32Exception ex) when (ex.NativeErrorCode == 1223) // Operation cancelled by user
            {
                // do nothing
            }
            catch (Exception ex)
            {
                var message = string.Format(Texts.LaunchSetupException, filename, productName, ex);
                MessageBox.Show(owner, message, owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);
            } // try-catch

            return false;
        } // RedistSetup

        public static bool Windows10LoadNetFx35([NotNull] Form owner, string productName, Label labelProduct, Action<int> setupExitCallback)
        {
            var filename = Path.Combine(_redistFolder, "LoadNetFx35.exe");

            var msg = string.Format(Texts.NetFxRequired, productName);
            MessageBox.Show(owner, msg, owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);

            try
            {
                LaunchSetup(owner, filename, productName, labelProduct, Windows10LoadNetFx35_ProcessExited, setupExitCallback);

                return true;
            }
            catch (Exception ex)
            {
                var message = string.Format(Texts.LaunchSetupException, filename, productName, ex);
                MessageBox.Show(owner, message, owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Stop);

                return false;
            } // try-catch
        } // Windows10LoadNetFx35

        private static void LaunchSetup([NotNull] Form owner, string filename, string productName, Label labelProduct, SetupProcessExitedDelegate processExitedDelegate,
            Action<int> setupExitCallback)
        {
            var process = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = filename,
                    UseShellExecute = true,
                    ErrorDialog = true,
                    ErrorDialogParentHandle = owner.Handle
                },
                EnableRaisingEvents = true
            };
            process.Exited += (o, e) =>
            {
                var exitCode = process.ExitCode;
                process.Dispose();

                owner?.BeginInvoke(processExitedDelegate, exitCode, owner, productName, labelProduct, setupExitCallback);
            };
            process.Start();

            labelProduct.Text = string.Format(Texts.RedistSetupInstalling, productName);
        } // LaunchSetup

        private delegate void SetupProcessExitedDelegate(int exitCode, Form owner, string productName, Label labelProduct, Action<int> setupExitCallback);

        private static void RedistSetup_ProcessExited(int exitCode, Form owner, string productName, Label labelProduct, Action<int> setupExitCallback)
        {
            if (exitCode != 0)
            {
                var message = string.Format(Texts.LaunchSetupError, productName, exitCode);
                MessageBox.Show(owner, message, owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } // if

            labelProduct.Text = productName;
            setupExitCallback(exitCode);
        } // RedistSetup_ProcessExited

        private static void Windows10LoadNetFx35_ProcessExited(int exitCode, Form owner, string productName, Label labelProduct, Action<int> setupExitCallback)
        {
            if (exitCode != -2146232576) // 0x80131700 = .Net FX not installed
            {
                var message = string.Format(Texts.LaunchSetupError, productName, exitCode);
                MessageBox.Show(owner, message, owner.Text, MessageBoxButtons.OK, MessageBoxIcon.Warning);
            } // if

            labelProduct.Text = productName;
            setupExitCallback(exitCode);
        } // Windows10LoadNetFx35_ProcessExited

        private static string GetRedistFileFullPath(string file64Bit, string file32Bit)
        {
            var file = Is32BitWindows ? file32Bit : file64Bit;
            file = file.Replace('\\', Path.DirectorySeparatorChar);
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
                AppTelemetry.ScreenEvent(AppTelemetry.LoadEvent, "FirewallForm");

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
            catch (Win32Exception win32) when (win32.NativeErrorCode == 1223) // operation cancelled by user
            {
                AppTelemetry.ScreenEvent("UACancel", "FirewallForm");
                return new InitializationResult(Texts.FirewallUserCancel);
            }
            catch (Exception ex)
            {
                AppTelemetry.ScreenEvent("Exception", "FirewallForm");
                AppTelemetry.ScreenException(ex, "FirewallForm", "process.Start()");
                return new InitializationResult(ex, "Unable to execute Firewall configuration program.");
            } // try-catch

            if (exitCode == 0)
            {
                AppTelemetry.ScreenEvent("Ok", "FirewallForm");
                return new InitializationResult(Texts.FirewallOk)
                {
                    IsOk = true
                };
            }

            AppTelemetry.ScreenEvent($"Cancel ({exitCode})", "FirewallForm");
            return new InitializationResult(Texts.FirewallUserCancel);
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
                    var programs = Resources.FirewallProgramList.Split(new[] { "\r\n" }, StringSplitOptions.RemoveEmptyEntries);
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
