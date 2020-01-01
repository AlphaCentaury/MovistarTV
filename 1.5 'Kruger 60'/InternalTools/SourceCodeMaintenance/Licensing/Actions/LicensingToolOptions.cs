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
using System.Xml.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions
{
    [Serializable]
    [XmlRoot("LicensingTool.Options")]
    public class LicensingToolOptions
    {
        public LicensingToolOptions()
        {
            // no-op
        } // constructor

        public LicensingToolOptions(bool notNull)
        {
            if (!notNull) return;

            Checker = new CheckerOptions();
            Writer = WriterOptions.CreateDefaultsOptions();
            SolutionWriter = WriterOptions.CreateSolutionDefaultsOptions();
        } // constructor

        public CheckerOptions Checker { get; set; }

        public WriterOptions Writer { get; set; }

        public WriterOptions SolutionWriter { get; set; }

        public LicensingToolOptions Clone()
        {
            return new LicensingToolOptions
            {
                Checker = Checker?.Clone(),
                Writer = Writer?.Clone(),
                SolutionWriter =  SolutionWriter?.Clone()
            };
        } // Clone
    } // class LicensingToolOptions
} // namespace
