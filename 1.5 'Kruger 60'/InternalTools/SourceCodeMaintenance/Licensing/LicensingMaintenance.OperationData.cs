using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    public partial class LicensingMaintenance
    {
        internal class OperationData
        {
            public OperationData(VsSolution solution, IToolOutputWriter writer)
            {
                Solution = solution;
                Writer = writer;
            } // constructor

            public VsSolution Solution { get; }
            public IToolOutputWriter Writer { get; }
        } // class OperationData
    } // partial class LicensingMaintenance
} // namespace