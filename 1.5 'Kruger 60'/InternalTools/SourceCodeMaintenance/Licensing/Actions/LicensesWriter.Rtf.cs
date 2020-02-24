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

using System.Globalization;
using System.IO;
using System.Text;
using AlphaCentaury.Licensing.Data.Serialization;
using IpTviewr.Common.Serialization;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions
{
    internal partial class LicensesWriter
    {
        private void WriteRtf(LicensingUsage usageData, string language, string licenseFilenameRtf)
        {
            Writer.WriteLine("Writing '{0}'...", Path.GetFileName(licenseFilenameRtf));

            using var writer = new StreamWriter(licenseFilenameRtf, false, Encoding.ASCII);
            WriteRtf(writer, usageData, language);

        } // WriteRtf

        private void WriteRtf(TextWriter output, LicensingUsage usageData, string language)
        {
            using var mdOutput = new StringWriter(CultureInfo.CurrentCulture);
            WriteMarkdown(mdOutput, usageData, language, false);
            var rtf = ChangeTextFormat(mdOutput.ToString(), "MD", "RTF");

            output.Write(rtf);
        } // WriteRtf

        private void WriteRtfLicensingData(LicensingData data, string filename)
        {
            TransformLicensingDataText(data, filename, "RTF", (text, format) => ChangeTextFormat(text, format, "RTF"));
        } // WriteRtfLicensingData
    } // partial class LicensesWriter
} // namespace
