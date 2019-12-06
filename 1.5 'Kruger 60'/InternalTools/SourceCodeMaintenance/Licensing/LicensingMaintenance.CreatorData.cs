using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;
using IpTviewr.Common.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    public sealed partial class LicensingMaintenance
    {
        internal class CreatorData : OperationData
        {
            public readonly IReadOnlyDictionary<string, LicensingDefaults> Defaults;
            public readonly CancellationToken Token;

            public CreatorData(VsSolution solution, IToolOutputWriter writer, CancellationToken token) : base(solution, writer)
            {
                var defaultsFiles = from file in Directory.EnumerateFiles(Path.GetDirectoryName(Application.ExecutablePath), "*licensing.defaults.xml", SearchOption.TopDirectoryOnly)
                    select XmlSerialization.Deserialize<LicensingDefaults>(file);
                Defaults = defaultsFiles.ToDictionary(@default => @default.AppliesTo ?? "");
                Token = token;
            } // constructor
        } // class CreatorData
    } // partial class LicensingMaintenance
} // namespace