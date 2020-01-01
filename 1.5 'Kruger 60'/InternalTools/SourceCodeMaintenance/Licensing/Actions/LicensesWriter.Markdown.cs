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
        private void WriteMarkdown(LicensingUsage data, string filename, string language)
        {
            if (data.Licensed == null) throw new ArgumentException();

            Writer.WriteLine("Writing '{0}'...", Path.GetFileName(filename));

            using var output = new StreamWriter(filename, false, XmlSerialization.Utf8NoBomEncoding.Value, 1024);
            WriteMarkdown(output, data, language, true);
        } // WriteMarkdown

        private void WriteMarkdown(TextWriter output, LicensingUsage data, string language, bool withLinks)
        {
            // Header
            WriteMarkdownHeader(output, data);

            // Terms and conditions
            output.WriteLine("## {0}", LicensingResources.WriteTermsAndCondition);
            output.WriteLine();
            var terms = data.Licensed.TermsConditions.First(t => string.Equals(t.Language, language, StringComparison.InvariantCulture));
            output.WriteLine(ChangeTextFormat(terms.Text, terms.Format, "MD"));
            output.WriteLine();

            // List of licenses
            output.WriteLine("## {0}", LicensingResources.WriteListLicenses);
            output.WriteLine();
            foreach (var usage in data.Usage.Where(usage => usage.AppliesTo != null))
            {
                var format = withLinks ? "  * [{0}]" : "  * {0}";
                output.WriteLine(format, usage.License.Name);
            } // foreach

            output.WriteLine();

            // Licenses
            foreach (var usage in data.Usage.Where(usage => usage.AppliesTo != null))
            {
                output.WriteLine("### {0}", usage.License.Name);
                output.WriteLine();
                output.WriteLine(ChangeTextFormat(usage.License.Text, usage.License.Format, "MD"));
                output.WriteLine();
                output.WriteLine();

                // Third-party components
                int index;

                if (usage.AppliesTo.ThirdPartySpecified)
                {
                    output.WriteLine(LicensingResources.WriteAppliesToThirdPartyFormatMd, usage.License.Name);
                    output.WriteLine();

                    index = 0;
                    foreach (var thirdParty in usage.AppliesTo.ThirdParty.OrderBy(comp => comp.Name + ":"))
                    {
                        WriteComponentDependencyMarkdown(output, thirdParty, ++index);
                        output.WriteLine();
                    } // foreach

                    output.WriteLine();
                } // if

                // Libraries
                if (!usage.AppliesTo.LibrariesSpecified) continue;

                output.WriteLine(LicensingResources.WriteAppliesToLibrariesFormatMd, usage.License.Name);
                output.WriteLine();
                index = 0;
                foreach (var library in usage.AppliesTo.Libraries.OrderBy(lib => lib.Assembly))
                {
                    WriteLibraryDependencyMarkdown(output, library, ++index);
                    output.WriteLine();
                } // foreach

                output.WriteLine();
            } // foreach var usage
        } // WriteMarkdown

        private void WriteMarkdownHeader(TextWriter output, LicensingUsage data)
        {
            output.WriteLine(LicensingResources.WriteLicensedFormatMd, GetLicensedItemType(data.Licensed.Type), data.Licensed.Assembly);
            if (data.Licensed.Product != null)
            {
                output.WriteLine("{0}\\", data.Licensed.Product);
            } // if

            if (data.Licensed.Authors != null)
            {
                output.WriteLine(LicensingResources.WriteAuthorsFormatMd, data.Licensed.Authors);
                output.WriteLine();
            } // if

            if (data.Licensed.Copyright != null)
            {
                output.WriteLine("**{0}**", data.Licensed.Copyright);
                output.WriteLine();
            } // if

            if (data.Licensed.Remarks?.Text != null)
            {
                output.WriteLine(ChangeTextFormat(data.Licensed.Remarks.Text, data.Licensed.Remarks.Format, "MD"));
                output.WriteLine();
            } // if

            if (data.Licensed.Notes?.Text != null)
            {
                output.WriteLine("_" + ChangeTextFormat(data.Licensed.Notes.Text, data.Licensed.Notes.Format, "MD") + "_");
                output.WriteLine();
            } // if
        } // WriteMarkdownHeader

        private void WriteLibraryDependencyMarkdown(TextWriter output, LibraryDependency dependency, int index)
        {
            output.WriteLine("#### {0}. {1}", index, dependency.Assembly);

            if (dependency.Authors != null)
            {
                var type = GetLicensedItemType(dependency.Type);
                if (type.Length == 0)
                {
                    output.WriteLine(LicensingResources.WriteAuthorsFormatMd, dependency.Authors);
                }
                else
                {
                    output.WriteLine(LicensingResources.WriteTypeDependencyAuthorsFormatMd, type, dependency.Authors);
                } // if-else

                output.WriteLine();
            }
            else
            {
                output.WriteLine("_{0}_", GetLicensedItemType(dependency.Type));
                output.WriteLine();
            } // if-else

            if (dependency.Copyright != null)
            {
                output.WriteLine("**{0}**", dependency.Copyright);
                output.WriteLine();
            } // if

            if (dependency.Remarks?.Text == null) return;

            var md = ChangeTextFormat(dependency.Remarks.Text, dependency.Remarks.Format, "MD");
            WriteIndented(output, md, "   ");
            output.WriteLine();
        } // WriteLibraryDependencyMarkdown

        private void WriteComponentDependencyMarkdown(TextWriter output, ThirdPartyDependency dependency, int index)
        {
            output.WriteLine("#### {0}. {1}", index, dependency.Name);

            if (dependency.Authors != null)
            {
                var type = GetDependencyType(dependency);
                if (type.Length == 0)
                {
                    output.WriteLine(LicensingResources.WriteAuthorsFormatMd, dependency.Authors);
                }
                else
                {
                    output.WriteLine(LicensingResources.WriteTypeDependencyAuthorsFormatMd, type, dependency.Authors);
                } // if-else

                output.WriteLine();
            }
            else
            {
                output.WriteLine("_{0}_", GetDependencyType(dependency));
                output.WriteLine();
            } // if-else

            if (dependency.Copyright != null)
            {
                output.WriteLine("**{0}**", dependency.Copyright);
                output.WriteLine();
            } // if

            if (dependency.Remarks?.Text != null)
            {
                var md = ChangeTextFormat(dependency.Remarks.Text, dependency.Remarks.Format, "MD");
                WriteIndented(output, md, "   ");
                output.WriteLine();
            } // if

            if (dependency.Description != null)
            {
                output.WriteLine(dependency.Description);
                output.WriteLine();
            } // if
        } // WriteComponentDependencyMarkdown
    } // partial class LicensesWriter
} // namespace
