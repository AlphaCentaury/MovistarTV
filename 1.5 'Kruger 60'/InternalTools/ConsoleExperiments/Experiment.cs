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
