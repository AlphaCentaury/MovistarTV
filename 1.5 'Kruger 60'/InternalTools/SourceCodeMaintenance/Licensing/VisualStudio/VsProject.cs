// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio
{
    public class VsProject
    {
        public Guid Guid { get; set; }
        public string Language { get; set; }
        public string Name { get; set; }
        public string Filename { get; set; }
        public string AssemblyName { get; set; }
        public string Namespace { get; set; }
        public List<Guid> ReferencedProjects { get; set; }
        public string Type { get; set; }
        public string LicensingDefaultsKey { get; set; }

        public override string ToString()
        {
            return $"{Type}:{Namespace}";
        } // ToString

        public bool IsLibrary
        {
            get
            {
                return Type switch
                {
                    "Library" => true,
                    "Exe" => false,
                    "WinExe" => false,
                    _ => true // we will assume all kinds of projects are libraries (.dll) and not executables (.exe)
                };
            } // get
        }  // IsLibrary
    } // VsProject
}
