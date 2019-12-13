using System.Collections.Generic;
using System.IO;
using System.Threading;
using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;
using IpTviewr.Common.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions
{
    internal sealed class Creator : ProjectAction
    {
        public readonly IReadOnlyDictionary<string, LicensingDefaults> Defaults;

        public Creator(VsSolution solution, IToolOutputWriter writer, string defaultsPath, CancellationToken token) : base(solution, writer, token)
        {
            writer.WriteLine("Reading licensing defaults...");
            Defaults = LicensingMaintenance.Helper.ReadLicensingDefaults(defaultsPath);
        } // constructor

        public override void Do(VsProject project, bool standalone)
        {
            Token.ThrowIfCancellationRequested();

            Writer.WriteLine("Project '{0}'", project.Name);
            var filename = LicensingMaintenance.Helper.GetLicensingFilename(project, standalone);

            Writer.IncreaseIndent();
            if (File.Exists(filename))
            {
                Writer.WriteLine("File '{0}' not needed", Path.GetFileName(filename));
            }
            else
            {
                Writer.WriteLine("Info: Creating '{0}'...", Path.GetFileName(filename));
                var licensingData = GetLicensingData(project);
                licensingData.FilePath = filename;
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

            var licensedDefaults = project.IsLibrary ? defaults.ForLibraries : defaults.ForPrograms;
            licensed.Product = licensedDefaults.Product;
            licensed.TermsConditions = licensedDefaults.TermsConditions;
            licensed.Authors = licensedDefaults.Authors;
            licensed.Copyright = licensedDefaults.Copyright;
            licensed.LicenseId = licensedDefaults.LicenseId;
            licensed.Remarks = licensedDefaults.Remarks;

            return licensingData;
        } // GetLicensingData
    } // class Creator
} // namespace