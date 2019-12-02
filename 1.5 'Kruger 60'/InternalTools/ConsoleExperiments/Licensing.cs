// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Text;
using AlphaCentaury.Licensing;
using AlphaCentaury.Licensing.Serialization;
using IpTviewr.Common.Serialization;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    internal sealed class Licensing : Experiment
    {
        protected override int Run(string[] args)
        {
            /*
            var file = XmlSerialization.Deserialize<LicensingFile>(@"C:\Users\Developer\source\repos\AlphaCentaury\MovistarTV\1.5 'Kruger 60'\Core\IpTviewr.Telemetry\licensing.xml");

            using var output = Console.OpenStandardOutput();
            XmlSerialization.Serialize(@"C:\Users\Developer\source\repos\AlphaCentaury\MovistarTV\1.5 'Kruger 60'\Core\IpTviewr.Telemetry\licensing.xml", file);

            return 0;
            */

            var files = new List<LicensingFile>
            {
                CreateDummy("A", "L1", new [] {"B", "D"}, new []{"1"}, new []{"L5"}),
                CreateDummy("B", "L2", new [] {"C", "F", "E"}, new []{"4", "6", "2"}, new []{"L2", "L1", "L4"}),
                CreateDummy("C", "L1", new [] {"D", "E"}, null, null),
                CreateDummy("D", "L3", null, new[]{"3","2", "7"}, new []{"L6", "L4", "L3"}),
                CreateDummy("E", "L1", new [] {"F" }, null, null),
                CreateDummy("F", "L2", new [] {"D" }, new[]{"1","3"}, new []{"L2", "L5"})
            };

            files.FirstPass();

            return 0;
        } // Run

        private static LicensingFile CreateDummy(string name, string licenseId, string[] dependencies, string[] thirdParty, string[] licenses)
        {
            var dummy = new LicensingFile
            {
                Licensing = new AlphaCentaury.Licensing.Serialization.Licensing
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
                dummy.Dependencies.Libraries = new List<DependencyLibrary>(dependencies.Length);
                foreach (var dependency in dependencies)
                {
                    dummy.Dependencies.Libraries.Add(new DependencyLibrary { Name = dependency, IsDirectDependency = true });
                } // foreach
            } // if

            if (thirdParty == null) return dummy;

            if (dummy.Dependencies == null) dummy.Dependencies = new Dependencies();
            dummy.Licensing.ThirdParty = new List<ThirdPartyLibrary>(thirdParty.Length);
            foreach (var third in thirdParty)
            {
                dummy.Licensing.ThirdParty.Add(new ThirdPartyLibrary { Name = third });
            } // foreach

            if (licenses == null) return dummy;
            if (licenses.Length != thirdParty.Length) throw new ArgumentException(null, nameof(licenses));
            for (var index = 0; index < licenses.Length; index++)
            {
                var id = licenses[index];
                var license = new License {Id = licenses[index]};
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
