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
using System.Globalization;
using System.IO;
using System.Linq;
using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Tools.SourceCodeMaintenance.Properties;
using IpTviewr.Common.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions
{
    internal sealed partial class LicensesWriter
    {
        private void WriteHtml(LicensingUsage usageData, string language, string licenseFilenameHtml)
        {
            Writer.WriteLine("Writing '{0}'...", Path.GetFileName(licenseFilenameHtml));

            using var writer = new StreamWriter(licenseFilenameHtml, false, XmlSerialization.Utf8NoBomEncoding.Value);
            WriteHtml(writer, usageData, language, true);
        } // WriteHtml

        private void WriteHtml(TextWriter output, LicensingUsage usageData, string language, bool withLinks)
        {
            using var mdOutput = new StringWriter(CultureInfo.CurrentCulture);
            WriteMarkdown(mdOutput, usageData, language, true);
            var html = ChangeTextFormat(mdOutput.ToString(), "MD", "HTML");

            output.WriteLine(LicensingResources.WriteHtmlHead);
            output.WriteLine(LicensingResources.WriteHtmlTitle, GetLicensedItemType(usageData.Licensed.Type), usageData.Licensed.Assembly);
            output.WriteLine("<style>");
            output.WriteLine(LicensingResources.HtmlDefaultStylesheet);
            output.WriteLine("</style>");
            output.WriteLine("</head><body>");
            output.WriteLine(html);
            output.WriteLine("</body></html>");
        } // WriteHtml

        private void WriteHtmlLicensingData(LicensingData data, string filename)
        {
            TransformLicensingDataText(data, filename, "HTML", (text, format) => ChangeTextFormat(text, format, "HTML"));
        } // WriteHtmlLicensingData

        private void TransformLicensingDataText(LicensingData data, string filename, string format, Func<string, string, string> converter)
        {
            data = XmlSerialization.Clone(data);

            Writer.WriteLine("Writing '{0}'...", Path.GetFileName(filename));
            foreach (var terms in data.Licensed.TermsConditions)
            {
                terms.Text = converter(terms.Text, terms.Format);
                terms.Format = format;
            } // foreach

            if (!(data.Licensed.Remarks is null))
            {
                data.Licensed.Remarks = converter(data.Licensed.Remarks, data.Licensed.Remarks.Format);
                data.Licensed.Remarks.Format = format;
            } // if

            if (!(data.Licensed.Notes is null))
            {
                data.Licensed.Notes = converter(data.Licensed.Notes, data.Licensed.Notes.Format);
                data.Licensed.Notes.Format = format;
            } // if

            if (data.Dependencies != null)
            {
                if (data.Dependencies.LibrariesSpecified)
                {
                    foreach (var library in data.Dependencies.Libraries.Where(lib => !(lib.Remarks is null)))
                    {
                        library.Remarks.Text = converter(library.Remarks.Text, library.Remarks.Format);
                        library.Remarks.Format = format;
                    } // foreach
                } // if

                if (data.Dependencies.ThirdPartySpecified)
                {
                    foreach (var component in data.Dependencies.ThirdParty.Where(c => !(c.Remarks is null)))
                    {
                        component.Remarks.Text = converter(component.Remarks.Text, component.Remarks.Format);
                        component.Remarks.Format = format;
                    } // foreach
                } // if
            } // if

            if (data.LicensesSpecified)
            {
                foreach (var license in data.Licenses)
                {
                    license.Text = converter(license.Text, license.Format);
                    license.Format = format;
                } // foreach
            } // if

            XmlSerialization.Serialize(filename, data);
        } // TransformLicensingDataText
    } // partial class LicensesWriter
} // namespace
