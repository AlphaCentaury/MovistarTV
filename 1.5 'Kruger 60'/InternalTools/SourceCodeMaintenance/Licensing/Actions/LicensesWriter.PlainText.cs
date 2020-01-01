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
using System.IO;
using System.Linq;
using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Tools.SourceCodeMaintenance.Properties;
using IpTviewr.Common.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions
{
    internal sealed partial class LicensesWriter
    {
        private void WritePlainText(LicensingUsage data, string filename, string language)
        {
            if (data.Licensed == null) throw new ArgumentException();

            Writer.WriteLine("Writing '{0}'...", Path.GetFileName(filename));
            using var output = new StreamWriter(filename, false, XmlSerialization.Utf8NoBomEncoding.Value, 1024);

            // Header
            WritePlainTextHeader(output, data);

            // Terms and conditions
            output.WriteLine("A.- {0}", LicensingResources.WriteTermsAndCondition);
            output.WriteLine("==============================================================================");
            output.WriteLine();
            var terms = data.Licensed.TermsConditions.First(t => string.Equals(t.Language, language, StringComparison.InvariantCulture));
            output.WriteLine(ChangeTextFormat(terms.Text, terms.Format, null));
            output.WriteLine();
            output.WriteLine();

            // List of licenses
            output.WriteLine("B.- {0}", LicensingResources.WriteListLicenses);
            output.WriteLine("==============================================================================");
            output.WriteLine();
            foreach (var usage in data.Usage.Where(usage => usage.AppliesTo != null))
            {
                output.WriteLine("  * {0}", usage.License.Name);
            } // foreach

            // Licenses
            foreach (var usage in data.Usage.Where(usage => usage.AppliesTo != null))
            {
                output.WriteLine();
                output.WriteLine();
                output.WriteLine("////////////////////////////////////////////////////////////");
                output.WriteLine("//");
                output.WriteLine("//  {0}", usage.License.Name);
                output.WriteLine("//");
                output.WriteLine("////////////////////////////////////////////////////////////");
                output.WriteLine();
                output.WriteLine(ChangeTextFormat(usage.License.Text, usage.License.Format, null));
                output.WriteLine();
                output.WriteLine("////////////////////////////////////////////////////////////");
                output.WriteLine();

                // Third-party components
                int index;

                if (usage.AppliesTo.ThirdPartySpecified)
                {
                    output.WriteLine(LicensingResources.WriteAppliesToThirdPartyFormat, usage.License.Name);
                    output.WriteLine();

                    index = 0;
                    foreach (var thirdParty in usage.AppliesTo.ThirdParty.OrderBy(comp => comp.Name + ":"))
                    {
                        WriteComponentDependencyText(output, thirdParty, ++index);
                        output.WriteLine();
                    } // foreach
                } // if

                // Libraries
                if (!usage.AppliesTo.LibrariesSpecified) continue;

                output.WriteLine(LicensingResources.WriteAppliesToLibrariesFormat, usage.License.Name);
                output.WriteLine();
                index = 0;
                foreach (var library in usage.AppliesTo.Libraries.OrderBy(lib => lib.Assembly))
                {
                    WriteLibraryDependencyText(output, library, ++index);
                    output.WriteLine();
                } // foreach
            } // foreach var usage
        } // WritePlainText

        private void WritePlainTextHeader(TextWriter output, LicensingUsage data)
        {
            output.WriteLine("##############################################################################");
            output.WriteLine("##");
            output.WriteLine(LicensingResources.WriteLicensedFormat, GetLicensedItemType(data.Licensed.Type), data.Licensed.Assembly);
            output.WriteLine("##");
            output.WriteLine("##############################################################################");
            output.WriteLine("##");

            if (data.Licensed.Product != null)
            {
                output.WriteLine("## {0}", data.Licensed.Product);
            } // if

            if (data.Licensed.Authors != null)
            {
                output.WriteLine(LicensingResources.WriteAuthorsFormat, data.Licensed.Authors);
            } // if

            if ((data.Licensed.Product != null) && (data.Licensed.Authors != null))
            {
                output.WriteLine("##");
            } // if

            if (data.Licensed.Copyright != null)
            {
                output.WriteLine("## {0}", data.Licensed.Copyright);
            } // if

            if (data.Licensed.Remarks?.Text != null)
            {
                output.WriteLine("##");
                WriteIndented(output, ChangeTextFormat(data.Licensed.Remarks.Text, data.Licensed.Remarks.Format, null), "##    ");
            } // if

            if (data.Licensed.Notes?.Text != null)
            {
                output.WriteLine("##");
                WriteIndented(output, ChangeTextFormat(data.Licensed.Notes.Text, data.Licensed.Notes.Format, null), "##    ");
            } // if

            output.WriteLine("##");
            output.WriteLine("##############################################################################");
            output.WriteLine();
        } // WritePlainTextHeader

        private void WriteLibraryDependencyText(TextWriter output, LibraryDependency dependency, int index)
        {
            output.WriteLine("{0}. {2}{1}", index, dependency.Assembly, dependency.Authors is null ? $"{GetLicensedItemType(dependency.Type)} " : "");
            output.WriteLine("------------------------------");

            if (dependency.Authors != null)
            {
                var type = GetLicensedItemType(dependency.Type);
                if (type.Length == 0)
                {
                    output.WriteLine(LicensingResources.WriteDependencyAuthorsFormat, dependency.Authors);
                }
                else
                {
                    output.WriteLine(LicensingResources.WriteTypeDependencyAuthorsFormat, type, dependency.Authors);
                } // if-else
            } // if

            if (dependency.Copyright != null)
            {
                output.WriteLine(dependency.Copyright);
            } // if

            if (dependency.Remarks?.Text != null)
            {
                var md = ChangeTextFormat(dependency.Remarks.Text, dependency.Remarks.Format, null);
                WriteIndented(output, md, "   ");
            } // if
        } // WriteLibraryDependencyText

        private void WriteComponentDependencyText(TextWriter output, ThirdPartyDependency dependency, int index)
        {
            output.WriteLine("{0}. {2}{1}", index, dependency.Name, dependency.Authors is null ? $"{GetDependencyType(dependency)} " : "");
            output.WriteLine("------------------------------");

            if (dependency.Authors != null)
            {
                var type = GetDependencyType(dependency);
                if (type.Length == 0)
                {
                    output.WriteLine(LicensingResources.WriteDependencyAuthorsFormat, dependency.Authors);
                }
                else
                {
                    output.WriteLine(LicensingResources.WriteTypeDependencyAuthorsFormat, type, dependency.Authors);
                } // if-else
            } // if

            if (dependency.Copyright != null)
            {
                output.WriteLine(dependency.Copyright);
            } // if

            if (dependency.Remarks?.Text != null)
            {
                var md = ChangeTextFormat(dependency.Remarks.Text, dependency.Remarks.Format, null);
                WriteIndented(output, md, "   ");
            } // if

            if (dependency.Description != null)
            {
                output.WriteLine("   {0}", dependency.Description);
            } // if
        } // WriteComponentDependencyText
    } // partial class LicensesWriter
} // namespace
