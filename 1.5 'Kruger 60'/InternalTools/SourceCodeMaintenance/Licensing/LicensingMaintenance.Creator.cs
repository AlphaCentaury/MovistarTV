using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;
using IpTviewr.Common.Serialization;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    public sealed partial class LicensingMaintenance
    {
        internal class Creator : ProjectAction
        {
            public readonly IReadOnlyDictionary<string, LicensingDefaults> Defaults;

            public Creator(VsSolution solution, IToolOutputWriter writer, string defaultsPath, CancellationToken token) : base(solution, writer, token)
            {
                Defaults = Helper.ReadLicensingDefaults(defaultsPath);
            } // constructor

            public override void Do(VsProject project, bool standalone)
            {
                Token.ThrowIfCancellationRequested();

                Writer.WriteLine("Project '{0}'", project.Name);
                var filename = Helper.GetLicensingFilename(project, standalone);

                Writer.IncreaseIndent();
                if (File.Exists(filename))
                {
                    Writer.WriteLine("File '{0}' not needed", Path.GetFileName(filename));
                }
                else
                {
                    Writer.WriteLine("Creating '{0}'", Path.GetFileName(filename));
                    var licensingData = GetLicensingData(project);
                    XmlSerialization.Serialize(filename, licensingData);
                } // if-else
                Writer.DecreaseIndent();
            } // ForProject

            private LicensingData GetLicensingData(VsProject project)
            {
                var defaults = Defaults[project.LicensingDefaultsKey ?? ""];
                var licensed = project.IsLibrary switch
                {
                    true => (LicensedItem)new LicensedLibrary(),
                    false => new LicensedProgram { IsGuiApp = (project.Type != "WinExe") }
                };

                var licensingData = new LicensingData
                {
                    Licensed = licensed,
                    Licenses = defaults.Licenses
                };

                var licensedDefaults = project.IsLibrary ? defaults.Libraries : defaults.Programs;
                licensed.Product = licensedDefaults.Product;
                licensed.Terms = licensedDefaults.Terms;
                licensed.Authors = licensedDefaults.Authors;
                licensed.Copyright = licensedDefaults.Copyright;
                licensed.LicenseId = licensedDefaults.LicenseId;
                licensed.Remarks = licensedDefaults.Remarks;

                return licensingData;
            } // GetLicensingData
        } // class Creator
    } // partial class LicensingMaintenance
} // namespace