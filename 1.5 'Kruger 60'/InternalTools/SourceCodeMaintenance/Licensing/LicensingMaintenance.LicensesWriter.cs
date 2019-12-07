using System;
using System.Threading;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    public sealed partial class LicensingMaintenance
    {
        internal sealed class LicensesWriter : ProjectAction
        {
            public LicensesWriter(VsSolution solution, IToolOutputWriter writer, CancellationToken token) : base(solution, writer, token)
            {
            } // constructor

            public override void Do(VsProject project, bool standalone)
            {
                throw new NotImplementedException();
            } // Do
        } // class LicensesWriter
    } // partial class LicensingMaintenance
} // namespace