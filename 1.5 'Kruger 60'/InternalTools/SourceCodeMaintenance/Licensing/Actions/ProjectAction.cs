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

using System.Threading;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;
using JetBrains.Annotations;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions
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
} // namespace
