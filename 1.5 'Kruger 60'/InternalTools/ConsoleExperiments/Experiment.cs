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

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    internal abstract class Experiment
    {
        public int Execute(string[] args)
        {
            Console.WriteLine(ExperimentName);
            Console.WriteLine(SolutionVersion.DefaultCopyright);
            Console.WriteLine();

            return Run(args);
        } // Execute

        public virtual string ExperimentName
        {
            get
            {
                return GetType().Name;
            } // get
        } // ExperimentName

        protected abstract int Run(string[] args);
    } // abstract class Experiment
} // namespace
