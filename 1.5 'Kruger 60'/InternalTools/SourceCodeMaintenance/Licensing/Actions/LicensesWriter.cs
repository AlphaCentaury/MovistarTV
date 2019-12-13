using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using AlphaCentaury.Licensing.Data;
using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;
using AlphaCentaury.Tools.SourceCodeMaintenance.Properties;
using IpTviewr.Common.Serialization;
using Markdig;
using File = System.IO.File;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions
{
    internal sealed class LicensesWriter : ProjectAction
    {
        private readonly WriterOptions _options;
        private readonly MarkdownPipeline _mdPipeline;

        public LicensesWriter(VsSolution solution, IToolOutputWriter writer, WriterOptions options, CancellationToken token) : base(solution, writer, token)
        {
            _options = options;
            _mdPipeline = new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();
        } // constructor

        public override void Do(VsProject project, bool standalone)
        {
            Token.ThrowIfCancellationRequested();
            Writer.WriteLine("Project '{0}'", project.Name);
            Writer.IncreaseIndent();
            try
            {
                var filename = LicensingMaintenance.Helper.GetLicensingFilename(project, standalone);
                if (!File.Exists(filename))
                {
                    Writer.WriteLine("ERROR: file '{0}' not found", Path.GetFileName(filename));
                    return;
                } // if

                Writer.WriteLine("Loading '{0}'", Path.GetFileName(filename));
                var data = XmlSerialization.Deserialize<LicensingData>(filename);
                if ((data.Licensed == null) ||
                    !data.Licensed.TermsConditionsSpecified ||
                    !data.LicensesSpecified)
                {
                    Writer.WriteLine("ERROR: Invalid licensing data");
                    return;
                } // if

                data.FilePath = filename;

                if (!_options.SkipLicensingHtml)
                {
                    var licensingHtml = Path.Combine(Path.GetDirectoryName(filename), Path.GetFileNameWithoutExtension(filename) + ".html.xml");
                    WriteHtmlLicensingData(data, licensingHtml);
                } // if

                var usage = LicensingDataTools.GetUsage(data);
                var currentCulture = CultureInfo.CurrentCulture;
                var currentUiCulture = CultureInfo.CurrentUICulture;

                CultureInfo.CurrentCulture = CultureInfo.InvariantCulture;
                foreach (var language in data.Licensed.TermsConditions.Select(terms => terms.Language))
                {
                    CultureInfo.CurrentCulture = (language is null)? CultureInfo.InvariantCulture : new CultureInfo(language);
                    CultureInfo.CurrentUICulture = (language is null) ? CultureInfo.InvariantCulture : new CultureInfo(language);

                    var licenseFilename = LicensingMaintenance.Helper.GetLicenseFilename(project, standalone, "LICENSE") + (language != null ? "." : "");
                    var mdFile = licenseFilename + language + ".MD";
                    WriteMarkdown(usage, mdFile, language);
                    if (_options.WriteHtml)
                    {
                        WriteHtml(usage, mdFile, (licenseFilename + language + ".htm").ToLowerInvariant());
                    } // if

                    if (language == null)
                    {
                        var plainTextFile = (licenseFilename + ".txt").ToLowerInvariant();
                        WritePlainText(usage, plainTextFile, language);
                    } // if
                } // foreach

                CultureInfo.CurrentCulture = currentCulture;
                CultureInfo.CurrentUICulture = currentUiCulture;
            }
            finally
            {
                Writer.DecreaseIndent();
            } // finally
        } // Do

        #region Markdown

        private void WriteMarkdown(LicensingUsage data, string filename, string language)
        {
            if (data.Licensed == null) throw new ArgumentException();

            Writer.WriteLine("Writing '{0}'...", filename);
            using var output = new StreamWriter(filename, false, XmlSerialization.Utf8NoBomEncoding.Value, 1024);

            // Header
            WriteMarkdownHeader(output, data);

            // Terms and conditions
            output.WriteLine("## {0}", LicensingResources.WriteTermsAndCondition);
            output.WriteLine();
            var terms = data.Licensed.TermsConditions.First(t => string.Equals(t.Language, language, StringComparison.InvariantCulture));
            output.WriteLine(TransformToMarkdown(terms.Text, terms.Format));
            output.WriteLine();
            output.WriteLine("_{0}_", LicensingResources.WriteSeeLicensingXml);
            output.WriteLine();

            // List of licenses
            output.WriteLine("## {0}", LicensingResources.WriteListLicenses);
            output.WriteLine();
            foreach (var usage in data.Usage.Where(usage => usage.AppliesTo != null))
            {
                output.WriteLine("  * [{0}]", usage.License.Name);
            } // foreach
            output.WriteLine();

            // Licenses
            foreach (var usage in data.Usage.Where(usage => usage.AppliesTo != null))
            {
                output.WriteLine("### {0}", usage.License.Name);
                output.WriteLine();
                output.WriteLine(TransformToMarkdown(usage.License.Text, usage.License.Format));
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
            output.WriteLine(LicensingResources.WriteLicensedFormatMd, GetLicensedType(data.Licensed), data.Licensed.Assembly);
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
                output.WriteLine(TransformToMarkdown(data.Licensed.Remarks.Text, data.Licensed.Remarks.Format));
                output.WriteLine();
            } // if

            if (data.Licensed.Notes?.Text != null)
            {
                output.WriteLine("_" + TransformToMarkdown(data.Licensed.Notes.Text, data.Licensed.Notes.Format) + "_");
                output.WriteLine();
            } // if
        } // WriteMarkdownHeader

        private void WriteLibraryDependencyMarkdown(TextWriter output, LibraryDependency dependency, int index)
        {
            output.WriteLine("#### {0}. {1}", index, dependency.Assembly);

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

            if (dependency.Remarks?.Text == null) return;

            var md = TransformToMarkdown(dependency.Remarks.Text, dependency.Remarks.Format);
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
                var md = TransformToMarkdown(dependency.Remarks.Text, dependency.Remarks.Format);
                WriteIndented(output, md, "   ");
                output.WriteLine();
            } // if

            if (dependency.Description != null)
            {
                output.WriteLine(dependency.Description);
                output.WriteLine();
            } // if
        } // WriteComponentDependencyMarkdown

        private static void WriteIndented(TextWriter output, string multiLineText, string indent)
        {
            var lines = multiLineText.Trim().Split('\n');
            foreach (var line in lines)
            {
                if (line.Length == 0)
                {
                    output.WriteLine();
                }
                else
                {
                    output.WriteLine("{0}{1}", indent, line.Trim());
                } // if-else
            } // foreach
        } // WriteIndented

        #endregion

        #region Plain text

        private void WritePlainText(LicensingUsage data, string filename, string language)
        {
            if (data.Licensed == null) throw new ArgumentException();

            Writer.WriteLine("Writing '{0}'...", filename);
            using var output = new StreamWriter(filename, false, XmlSerialization.Utf8NoBomEncoding.Value, 1024);

            // Header
            WritePlainTextHeader(output, data);

            // Terms and conditions
            output.WriteLine("A.- {0}", LicensingResources.WriteTermsAndCondition);
            output.WriteLine("==============================================================================");
            output.WriteLine();
            var terms = data.Licensed.TermsConditions.First(t => string.Equals(t.Language, language, StringComparison.InvariantCulture));
            output.WriteLine(TransformToPlainText(terms.Text, terms.Format));
            output.WriteLine();
            output.WriteLine("({0})", LicensingResources.WriteSeeLicensingXml);
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
                output.WriteLine(TransformToPlainText(usage.License.Text, usage.License.Format));
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
            output.WriteLine(LicensingResources.WriteLicensedFormat, GetLicensedType(data.Licensed), data.Licensed.Assembly);
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
                WriteIndented(output, TransformToPlainText(data.Licensed.Remarks.Text, data.Licensed.Remarks.Format), "##    ");
            } // if

            if (data.Licensed.Notes?.Text != null)
            {
                output.WriteLine("##");
                WriteIndented(output, TransformToPlainText(data.Licensed.Notes.Text, data.Licensed.Notes.Format), "##    ");
            } // if

            output.WriteLine("##");
            output.WriteLine("##############################################################################");
            output.WriteLine();
        } // WritePlainTextHeader

        private void WriteLibraryDependencyText(TextWriter output, LibraryDependency dependency, int index)
        {
            output.WriteLine("{0}. {2}{1}", index, dependency.Assembly, dependency.Authors is null? $"{GetDependencyType(dependency)} " : "");
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
                var md = TransformToPlainText(dependency.Remarks.Text, dependency.Remarks.Format);
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
                var md = TransformToPlainText(dependency.Remarks.Text, dependency.Remarks.Format);
                WriteIndented(output, md, "   ");
            } // if

            if (dependency.Description != null)
            {
                output.WriteLine("   {0}", dependency.Description);
            } // if
        } // WriteComponentDependencyText

        #endregion

        #region HTML

        private void WriteHtml(LicensingUsage usageData, string licenseFilenameMd, string licenseFilenameHtml)
        {
            Writer.WriteLine("Writing '{0}'...", licenseFilenameHtml);

            var text = File.ReadAllText(licenseFilenameMd);
            var html = Markdown.ToHtml(text, _mdPipeline);

            using var writer = new StreamWriter(licenseFilenameHtml, false, XmlSerialization.Utf8NoBomEncoding.Value);
            writer.WriteLine(LicensingResources.WriteHtmlHead);
            writer.WriteLine(LicensingResources.WriteHtmlTitle, GetLicensedType(usageData.Licensed), usageData.Licensed.Assembly);
            writer.WriteLine(LicensingResources.WriteHtmlCss);
            writer.WriteLine("</head><body>");
            writer.WriteLine(html);
            writer.WriteLine("</body></html>");
        } // WriteHtml

        private void WriteHtmlLicensingData(LicensingData data, string filename)
        {
            data = XmlSerialization.Clone(data);

            Writer.WriteLine("Writing '{0}'...", filename);
            foreach (var terms in data.Licensed.TermsConditions)
            {
                terms.Text = TransformToHtml(terms.Text, terms.Format);
                terms.Format = "HTML";
            } // foreach

            if (data.Dependencies != null)
            {
                if (data.Dependencies.LibrariesSpecified)
                {
                    foreach (var library in data.Dependencies.Libraries.Where(lib => !(lib.Remarks is null)))
                    {
                        library.Remarks.Text = TransformToHtml(library.Remarks.Text, library.Remarks.Format);
                        library.Remarks.Format = "HTML";
                    } // foreach
                } // if

                if (data.Dependencies.ThirdPartySpecified)
                {
                    foreach (var component in data.Dependencies.ThirdParty.Where(c => !(c.Remarks is null)))
                    {
                        component.Remarks.Text = TransformToHtml(component.Remarks.Text, component.Remarks.Format);
                        component.Remarks.Format = "HTML";
                    } // foreach
                } // if
            } // if

            if (data.LicensesSpecified)
            {
                foreach (var license in data.Licenses)
                {
                    license.Text = TransformToHtml(license.Text, license.Format);
                    license.Format = "HTML";
                } // foreach
            } // if
            XmlSerialization.Serialize(filename, data);
        } // WriteHtmlXml

        #endregion

        private static string GetLicensedType(LicensedItem item)
        {
            return item.Type switch
            {
                LicensedItemType.Library => LicensingResources.WriteTypeLibrary,
                LicensedItemType.Program => LicensingResources.WriteTypeProgram,
                LicensedItemType.Installer => LicensingResources.WriteTypeInstaller,
                _ => LicensingResources.WriteUnknown
            };
        } // GetLicensedType

        private static string GetDependencyType(LibraryDependency dependency)
        {
            return dependency.Type switch
            {
                LicensedItemType.Library => LicensingResources.WriteTypeLibrary,
                LicensedItemType.Program => LicensingResources.WriteTypeProgram,
                LicensedItemType.Installer => LicensingResources.WriteTypeInstaller,
                _ => LicensingResources.WriteUnknown
            };
        } // GetDependencyType

        private static string GetDependencyType(ThirdPartyDependency dependency)
        {
            return dependency.Type switch
            {
                ThirdPartyDependencyType.NugetPackage => LicensingResources.WriteTypeNugetPackage,
                ThirdPartyDependencyType.Library => LicensingResources.WriteTypeThirdPartyLibrary,
                ThirdPartyDependencyType.ImageLibrary => LicensingResources.WriteTypeImageLibrary,
                ThirdPartyDependencyType.SourceCode => LicensingResources.WriteTypeSourceCode,
                _ => LicensingResources.WriteTypeOtherThirdParty
            };
        } // GetDependencyType

        private string TransformToPlainText(string text, string format)
        {
            switch (format)
            {
                case null:
                    return text.Trim();
                case "MD":
                    // TODO: parse MD to text
                    return text.Trim();
                default:
                    Writer.WriteLine("ERROR: unable to transform '{0}' to Markdown", format);
                    return null;
            } // switch
        } // TransformToPlainText

        private string TransformToMarkdown(string text, string format)
        {
            switch (format)
            {
                case null:
                    return TextToMarkdown(text.Trim());
                case "MD":
                    return text.Trim();
                default:
                    Writer.WriteLine("ERROR: unable to transform '{0}' to Markdown", format);
                    return null;
            } // switch
        } // TransformToMarkdown

        private string TransformToHtml(string text, string format)
        {
            switch (format)
            {
                case null:
                    return Markdown.ToHtml(TextToMarkdown(text.Trim()), _mdPipeline);
                case "MD":
                    return Markdown.ToHtml(text, _mdPipeline);
                case "HTML":
                    return text;
                default:
                    Writer.WriteLine("ERROR: unable to transform '{0}' to HTML", format);
                    return null;
            } // switch
        } // TransformToHtml

        private static string TextToMarkdown(string text)
        {
            var lines = text.Trim().Split('\n');

            for (var index = 0; index < lines.Length; index++)
            {
                if (string.IsNullOrWhiteSpace(lines[index])) lines[index] = string.Empty;
            } // for

            for (var index = 0; index < lines.Length; index++)
            {
                if (((index + 1) < lines.Length) && (lines[index + 1].Trim().Length != 0))
                {
                    lines[index] += "  \\"; // break line <br />
                } // if
            } // for

            return string.Join("\n", lines);
        } // TextToMarkdown
    } // class LicensesWriter
} // namespace