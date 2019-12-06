// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Windows.Forms.VisualStyles;
using AlphaCentaury.Licensing.Data;
using AlphaCentaury.Licensing.Data.Serialization;
using IpTviewr.Common.Serialization;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    internal sealed class Licensing : Experiment
    {
        protected override int Run(string[] args)
        {
            CreateDefaults();
            return 0;
        } // Run

        private static void CreateDefaults()
        {
            var file = XmlSerialization.Deserialize<LicensingData>(@"C:\Users\Developer\source\repos\AlphaCentaury\MovistarTV\1.5 'Kruger 60'\Core\IpTviewr.Telemetry\licensing.xml");

            var library = file.Licensing.Licensed;
            var defaults = new LicensingDefaults
            {
                Libraries = new LicensedLibrary()
                {
                    Authors = library.Authors,
                    Copyright = library.Copyright,
                    LicenseId = library.LicenseId,
                    Product = library.Product,
                    Remarks = library.Remarks,
                    Terms = library.Terms
                },
                Programs = new LicensedProgram
                {
                    Authors = library.Authors,
                    Copyright = library.Copyright,
                    LicenseId = library.LicenseId,
                    Product = library.Product,
                    Remarks = library.Remarks,
                    Terms = library.Terms
                },
                Licenses = new List<License>()
            };

            defaults.Licenses.Add(file.Licenses.FirstOrDefault(license => license.Id == defaults.Libraries.LicenseId));
            if (defaults.Licenses[0].Id != defaults.Programs.LicenseId)
            {
                defaults.Licenses.Add(file.Licenses.FirstOrDefault(license => license.Id == defaults.Programs.LicenseId));
            } // if

            XmlSerialization.Serialize("licensing.defaults.xml", defaults);
            XmlSerialization.Serialize("licensing.internal.defaults.xml", defaults);
        } // CreateDefaults

        private static void TestExpandDependencies()
        {
            var files = new List<LicensingData>
            {
                CreateDummy("A", "L1", new [] {"B", "D"}, new []{"1"}, new []{"L5"}),
                CreateDummy("B", "L2", new [] {"C", "F", "E"}, new []{"4", "6", "2"}, new []{"L2", "L1", "L4"}),
                CreateDummy("C", "L1", new [] {"D", "E"}, null, null),
                CreateDummy("D", "L3", null, new[]{"3","2", "7"}, new []{"L6", "L4", "L3"}),
                CreateDummy("E", "L1", new [] {"F" }, null, null),
                CreateDummy("F", "L2", new [] {"D" }, new[]{"1","3"}, new []{"L2", "L5"})
            };

            files.ExpandDependencies();
        } // TestExpandDependencies

        private static LicensingData CreateDummy(string name, string licenseId, string[] dependencies, string[] thirdParty, string[] licenses)
        {
            var dummy = new LicensingData
            {
                Licensing = new AlphaCentaury.Licensing.Data.Serialization.Licensing
                {
                    Licensed = new LicensedLibrary { Name = name, LicenseId = licenseId }
                },
                Licenses = new List<License>
                {
                    new License {Id=licenseId}
                }
            };

            if (dependencies != null)
            {
                if (dummy.Dependencies == null) dummy.Dependencies = new Dependencies();
                dummy.Dependencies.Libraries = new List<LibraryDependency>(dependencies.Length);
                foreach (var dependency in dependencies)
                {
                    dummy.Dependencies.Libraries.Add(new LibraryDependency { Namespace = dependency, IsDirectDependency = true });
                } // foreach
            } // if

            if (thirdParty == null) return dummy;

            if (dummy.Dependencies == null) dummy.Dependencies = new Dependencies();
            dummy.Licensing.ThirdParty = new List<ThirdPartyDependency>(thirdParty.Length);
            foreach (var third in thirdParty)
            {
                dummy.Licensing.ThirdParty.Add(new ThirdPartyDependency { Name = third });
            } // foreach

            if (licenses == null) return dummy;
            if (licenses.Length != thirdParty.Length) throw new ArgumentException(null, nameof(licenses));
            for (var index = 0; index < licenses.Length; index++)
            {
                var id = licenses[index];
                var license = new License { Id = licenses[index] };
                dummy.Licensing.ThirdParty[index].LicenseId = id;
                if (!dummy.Licenses.Contains(license))
                {
                    dummy.Licenses.Add(license);
                } // if
            } // for

            return dummy;
        } // CreateDummy
    } // class Licensing
} // namespace
