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
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio
{
    [Export(typeof(IVsProjectReader))]
    public class VsCsProjectReader : IVsProjectReader
    {
        #region Implementation of IVsProjectReader

        public string[] SupportedExtensions { get; } = { ".csproj" };

        public VsProject Read(Stream stream, string type)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (string.IsNullOrEmpty(type)) throw new ArgumentNullException(nameof(type));
            if (!string.Equals(type, ".csproj", StringComparison.InvariantCultureIgnoreCase)) throw new ArgumentOutOfRangeException(nameof(type), type, null);

            var project = new VsCSharpProject();

            var xmlProj = XElement.Load(stream);
            var ns = XNamespace.Get("http://schemas.microsoft.com/developer/msbuild/2003");

            var propertyItems = from propGroup in xmlProj.Elements(ns + "PropertyGroup")
                                from item in propGroup.Elements()
                                select item;

            // project properties
            foreach (var item in propertyItems)
            {
                switch (item.Name.LocalName)
                {
                    case "ProjectGuid":
                        if (project.Guid != Guid.Empty) throw new InvalidDataException();
                        project.Guid = Guid.Parse(item.Value);
                        break;

                    case "OutputType":
                        if (project.Type != null) throw new InvalidDataException();
                        project.Type = item.Value;
                        break;

                    case "RootNamespace":
                        if (project.Namespace != null) throw new InvalidDataException();
                        project.Namespace = item.Value;
                        break;

                    case "AssemblyName":
                        if (project.AssemblyName != null) throw new InvalidDataException();
                        project.AssemblyName = item.Value;
                        break;
                } // switch
            } // foreach

            // project references
            var q2 = from itemGroup in xmlProj.Elements(ns + "ItemGroup")
                     from reference in itemGroup.Elements(ns + "ProjectReference")
                     let guid = reference.Element(ns + "Project")
                     where guid != null
                     select Guid.Parse(guid.Value);

            project.ReferencedProjects = q2.ToList();
            if (project.ReferencedProjects.Count == 0)
            {
                project.ReferencedProjects = null;
            } // if

            return project;
        } // Read

        #endregion
    } // class VsCsProjectReader
} // namespace
