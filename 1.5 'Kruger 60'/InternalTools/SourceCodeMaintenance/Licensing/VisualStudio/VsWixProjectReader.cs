using System;
using System.ComponentModel.Composition;
using System.IO;
using System.Linq;
using System.Xml.Linq;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio
{
    [Export(typeof(IVsProjectReader))]
    public class VsWixProjectReader: IVsProjectReader
    {
        public string[] SupportedExtensions { get; } = { ".wixproj" };

        public VsProject Read(Stream stream, string type)
        {
            if (stream == null) throw new ArgumentNullException(nameof(stream));
            if (string.IsNullOrEmpty(type)) throw new ArgumentNullException(nameof(type));
            if (!string.Equals(type, ".wixproj", StringComparison.InvariantCultureIgnoreCase)) throw new ArgumentOutOfRangeException(nameof(type), type, null);

            var project = new VsProject
            {
                Language = "WIX"
            };

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

                    case "OutputName":
                        if (project.AssemblyName != null) throw new InvalidDataException();
                        project.AssemblyName = item.Value;
                        break;
                } // switch
            } // foreach

            project.Namespace = $"MSI:{project.AssemblyName}";

            return project;
        } // Read
    } // class VsWixProjectReader
} // namespace