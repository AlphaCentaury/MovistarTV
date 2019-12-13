// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using AlphaCentaury.Licensing.Data;
using AlphaCentaury.Licensing.Data.Serialization;
using System;
using System.Collections.Generic;
using System.IO;
using IpTviewr.Common.Serialization;

namespace IpTviewr.Internal.Tools.ConsoleExperiments
{
    internal sealed class Licensing : Experiment
    {
        protected override int Run(string[] args)
        {
            return 0;
        } // Run

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
                Licensed = new LicensedLibrary { Name = name, LicenseId = licenseId },
                Licenses = new List<License>
                {
                    new License {Id=licenseId}
                }
            };

            if (dependencies != null)
            {
                if (dummy.Dependencies == null) dummy.Dependencies = new LicensingDependencies();
                dummy.Dependencies.Libraries = new List<LibraryDependency>(dependencies.Length);
                foreach (var dependency in dependencies)
                {
                    dummy.Dependencies.Libraries.Add(new LibraryDependency { Name = dependency, DependencyType = LicensedDependencyType.Direct });
                } // foreach
            } // if

            if (thirdParty == null) return dummy;

            if (dummy.Dependencies == null) dummy.Dependencies = new LicensingDependencies();
            dummy.Dependencies.ThirdParty = new List<ThirdPartyDependency>(thirdParty.Length);
            foreach (var third in thirdParty)
            {
                dummy.Dependencies.ThirdParty.Add(new ThirdPartyDependency { Name = third, DependencyType = LicensedDependencyType.Direct});
            } // foreach

            if (licenses == null) return dummy;
            if (licenses.Length != thirdParty.Length) throw new ArgumentException(null, nameof(licenses));
            for (var index = 0; index < licenses.Length; index++)
            {
                var id = licenses[index];
                var license = new License { Id = licenses[index] };
                dummy.Dependencies.ThirdParty[index].LicenseId = id;
                if (!dummy.Licenses.Contains(license))
                {
                    dummy.Licenses.Add(license);
                } // if
            } // for

            return dummy;
        } // CreateDummy
    } // class Licensing
} // namespace
