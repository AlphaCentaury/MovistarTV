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
using System.Collections.Generic;
using System.IO;
using System.Linq;
using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using IpTviewr.Common.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions
{
    public class LicensingDefaultsPool
    {
        public const string AppliesToDefaultScope = "#default#";

        public LicensingDefaultsPool(string defaultsFolder, IToolOutputWriter writer)
        {
            var defaultsFiles = from file in Directory.EnumerateFiles(defaultsFolder, "licensing.defaults*.xml", SearchOption.TopDirectoryOnly)
                                select XmlSerialization.Deserialize<LicensingDefaults>(file);

            Pool = defaultsFiles.ToDictionary(@default => @default.AppliesTo ?? AppliesToDefaultScope, StringComparer.InvariantCulture);
            ApplyInheritance(writer);
        } // constructor

        public IReadOnlyDictionary<string, LicensingDefaults> Pool { get; }

        public LicensingDefaults this[string scope] => Pool.TryGetValue(scope ?? AppliesToDefaultScope, out var item) ? item : null;

        public bool TryGetValue(string scope, out LicensingDefaults defaults)
        {
            return Pool.TryGetValue(scope ?? AppliesToDefaultScope, out defaults);
        } // TryGet

        private void ApplyInheritance(IToolOutputWriter writer)
        {
            if (this[null] == null)
            {
                writer.WriteLine("ERROR: unable to find '" + AppliesToDefaultScope + "' in licensing defaults pool");
                return;
            } // if

            var done = new HashSet<string>();

            foreach (var licensingDefaults in Pool.Values)
            {
                var inheritFrom = GetInheritFrom(licensingDefaults.InheritsFrom);
                licensingDefaults.Inherit(inheritFrom);
            } // foreach default

            LicensingDefaults GetInheritFrom(string inheritsFrom)
            {
                if (string.IsNullOrEmpty(inheritsFrom)) return null;

                var from = this[inheritsFrom];
                if (from == null)
                {
                    writer.WriteLine("WARNING: unable to find '{0}' in licensing defaults pool", inheritsFrom);
                    return null;
                } // if

                if (done.Contains(inheritsFrom)) return from;

                // avoid inheritance cycles
                done.Add(inheritsFrom);

                var parent = GetInheritFrom(from.InheritsFrom);
                from.Inherit(parent);

                return from;
            } // GetInheritFrom
        } // Inherit
    } // class SolutionDefaults
} // namespace
