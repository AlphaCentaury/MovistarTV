using System.Threading;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;
using JetBrains.Annotations;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing
{
    public partial class LicensingMaintenance
    {
        internal abstract class ProjectAction
        {
            protected ProjectAction(VsSolution solution, IToolOutputWriter writer, CancellationToken token)
            {
                Solution = solution;
                Writer = writer;
                Token = token;
            } // constructor

            public VsSolution Solution { get; }
            public IToolOutputWriter Writer { get; }
            public CancellationToken Token { get; }

            public virtual void Init()
            {
                // no-op
            } // Init

            public abstract void Do([NotNull] VsProject project, bool standalone);

            public virtual void End()
            {
                // no-op
            } // End
        } // class ProjectAction
    } // partial class LicensingMaintenance
} // namespace