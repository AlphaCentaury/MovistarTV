// Copyright (C) 2014-2016, Codeplex/GitHub user AlphaCentaury
// All rights reserved, except those granted by the governing license of this software. See 'license.txt' file in the project root for complete license information.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace IpTViewr.Internal.Logos
{
    class PackLogos
    {
        private string FromFolder;
        private string ToFolder;

        public bool ProcessArguments(IDictionary<string, string> arguments)
        {
            string value;
            string assemblyLocation;

            assemblyLocation = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location);

            // FromFolder
            if (arguments.TryGetValue("from", out value))
            {
                FromFolder = value;
            }
            else
            {
#if DEBUG
                if (assemblyLocation.EndsWith("\\bin\\debug", StringComparison.InvariantCultureIgnoreCase))
                {
                    FromFolder = Path.GetFullPath(Path.Combine(assemblyLocation, "..\\..\\"));
                }
                else
#endif
                {
                    Program.DisplayError("No 'from' folder has been specified");
                    return false;
                } // if-else
            } // if-else

            if (!Directory.Exists(FromFolder))
            {
                Program.DisplayError("The specified 'from' folder does not exists: " + FromFolder);
                return false;
            } // if

            // ToFolder
            if (arguments.TryGetValue("to", out value))
            {
                ToFolder = value;
                Directory.CreateDirectory(ToFolder);
            }
            else
            {
                ToFolder = Directory.GetCurrentDirectory();
            } // if-else

            return true;
        } // ProcessArguments

        public Program.Result Execute()
        {
            string fromFolder, filenameFormat;

            var sizes = new short[] { 32, 48, 64, 96, 128, 256 };

            // providers
            fromFolder = Path.Combine(FromFolder, "Providers");
            filenameFormat = "{{logos-providers}} {0}.png";
            Packager.PackLogos(fromFolder, ToFolder, "logos-providers", null, sizes, false);

            // services
            fromFolder = Path.Combine(FromFolder, "Services");
            var folders = Directory.EnumerateDirectories(fromFolder);
            foreach (var folder in folders)
            {
                var domainName = Path.GetFileName(folder);
                filenameFormat = "{{logos-services}} " + domainName + "@{0}.png";
                Packager.PackLogos(folder, ToFolder, "logos-services", domainName, sizes, true);
            } // foreach folder

            return Program.Result.Ok;
        } // Execute
    } // class PackLogos
} // namespace
