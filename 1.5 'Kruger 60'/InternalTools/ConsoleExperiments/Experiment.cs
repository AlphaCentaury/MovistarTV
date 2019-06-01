// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
