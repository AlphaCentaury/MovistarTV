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

using AlphaCentaury.Licensing.Data;
using AlphaCentaury.Licensing.Data.Serialization;
using AlphaCentaury.Tools.SourceCodeMaintenance.Interfaces;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.TextConverters;
using AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.VisualStudio;
using AlphaCentaury.Tools.SourceCodeMaintenance.Properties;
using IpTviewr.Common.Serialization;
using System;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using File = System.IO.File;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.Actions
{
    internal sealed partial class LicensesWriter : ProjectAction
    {
        private readonly WriterOptions _writerOptions;
        private readonly WriterOptions _solutionOptions;
        private readonly ITextFormatConverter _plainTextConverter;
        private readonly ITextFormatConverter _markdownConverter;
        private readonly ITextFormatConverter _htmlConverter;
        private readonly ITextFormatConverter _rtfConverter;
        private WriterOptions _options;

        public LicensesWriter(VsSolution solution, IToolOutputWriter writer, WriterOptions options, WriterOptions solutionOptions, CancellationToken token) : base(solution, writer, token)
        {
            MarkdownConverter mdConverter;

            _options = options;
            _writerOptions = options;
            _solutionOptions = solutionOptions;
            _plainTextConverter = new PlainTextConverter(writer);
            _markdownConverter = mdConverter = new MarkdownConverter(writer);
            _htmlConverter = new HtmlConverter(writer, mdConverter.Pipeline);
            _rtfConverter = new RtfConverter(writer, mdConverter.Pipeline, token);
        } // constructor

        public override void Do(VsProject project, bool standalone)
        {
            Token.ThrowIfCancellationRequested();

            _options = VsSolutionProject.IsSolutionProject(project) ? _solutionOptions : _writerOptions;

            Writer.WriteLine("Project '{0}'", project.Name);
            Writer.IncreaseIndent();
            try
            {
                if (_options.DeleteOldFiles)
                {
                    DeleteOldFiles(project, standalone);
                } // if

                var data = LoadLicensingData(project, standalone, out var baseFilename);
                if (data == null) return;

                if (_options.LicensingHtml)
                {
                    var licensingHtml = baseFilename + ".html.xml";
                    WriteHtmlLicensingData(data, licensingHtml);
                } // if

                if (_options.LicensingRtf)
                {
                    var licensingRtf = baseFilename + ".rtf.xml";
                    WriteRtfLicensingData(data, licensingRtf);
                } // if

                // save current culture settings
                var currentCulture = CultureInfo.CurrentCulture;
                var currentUiCulture = CultureInfo.CurrentUICulture;

                WriteLanguageLicenseFiles(project, standalone, data);
                
                // restore culture settings
                CultureInfo.CurrentCulture = currentCulture;
                CultureInfo.CurrentUICulture = currentUiCulture;
            }
            finally
            {
                Writer.DecreaseIndent();
            } // finally
        } // Do

        public override void End()
        {
            _plainTextConverter.Dispose();
            _markdownConverter.Dispose();
            _htmlConverter.Dispose();
            _rtfConverter.Dispose();
        } // End

        private LicensingData LoadLicensingData(VsProject project, bool standalone, out string baseFilename)
        {
            var filename = LicensingMaintenance.Helper.GetLicensingFilename(project, standalone);
            if (!File.Exists(filename))
            {
                Writer.WriteLine("ERROR: file '{0}' not found", Path.GetFileName(filename));
                baseFilename = null;
                return null;
            } // if

            var folder = Path.GetDirectoryName(filename) ?? Path.GetPathRoot(filename);
            baseFilename = Path.Combine(folder, Path.GetFileNameWithoutExtension(filename));

            Writer.WriteLine("Loading '{0}'", Path.GetFileName(filename));
            var data = XmlSerialization.Deserialize<LicensingData>(filename);
            data.FilePath = filename;

            // check if data seems valid enough
            if ((data.Licensed != null) &&
                data.Licensed.TermsConditionsSpecified &&
                data.LicensesSpecified) return data;

            Writer.WriteLine("ERROR: Invalid licensing data");
            return null;
        } // LoadLicensingData

        private void WriteLanguageLicenseFiles(VsProject project, bool standalone, LicensingData data)
        {
            var usage = data.GetUsage();

            foreach (var language in data.Licensed.TermsConditions.Select(terms => terms.Language))
            {
                if ((language != null) && !_options.Translated) continue;

                CultureInfo.CurrentCulture = (language is null) ? CultureInfo.InvariantCulture : new CultureInfo(language);
                CultureInfo.CurrentUICulture = (language is null) ? CultureInfo.InvariantCulture : new CultureInfo(language);

                var path = LicensingMaintenance.Helper.GetLicenseFilename(project, standalone, "LICENSE") + (language != null ? "." : "") + language;
                var folder = Path.GetDirectoryName(path) ?? Path.GetPathRoot(path);
                var licenseFilename = Path.GetFileName(path);

                if (_options.PlainText && ((language == null) || _options.TranslatedPlainText))
                {
                    var plainTextFile = Path.Combine(folder, (licenseFilename + ".txt").ToLowerInvariant());
                    WritePlainText(usage, plainTextFile, language);
                } // if

                if (_options.Markdown && ((language == null) || _options.TranslatedMarkdown))
                {
                    var mdFile = Path.Combine(folder, licenseFilename + ".MD");
                    WriteMarkdown(usage, mdFile, language);
                } // if

                if (_options.Html && ((language == null) || _options.TranslatedHtml))
                {
                    var htmlFile = Path.Combine(folder, (licenseFilename + ".htm").ToLowerInvariant());
                    WriteHtml(usage, language, htmlFile);
                } // if

                if (_options.Rtf && ((language == null) || _options.TranslatedRtf))
                {
                    var rtfFile = Path.Combine(folder, (licenseFilename + ".rtf").ToLowerInvariant());
                    WriteRtf(usage, language, rtfFile);
                } // if
            } // foreach
        } // WriteLanguageLicenseFiles

        private void DeleteOldFiles(VsProject project, bool standalone)
        {
            var baseFile = LicensingMaintenance.Helper.GetLicensingFilename(project, standalone);
            var folder = Path.GetDirectoryName(baseFile) ?? Path.GetPathRoot(baseFile);
            var search = Path.GetFileNameWithoutExtension(baseFile) + ".*.xml";

            Writer.WriteLine("Deleting old files...");
            foreach (var file in Directory.EnumerateFiles(folder, search, SearchOption.TopDirectoryOnly))
            {
                if (file == baseFile) continue; // just in case
                try
                {
                    File.Delete(file);
                }
                catch (Exception e)
                {
                    Writer.WriteException(e, $"Unable to delete file '{file}'");
                } // catch
            } // foreach

            baseFile = LicensingMaintenance.Helper.GetLicenseFilename(project, standalone, "LICENSE");
            folder = Path.GetDirectoryName(baseFile) ?? Path.GetPathRoot(baseFile);
            search = Path.GetFileName(baseFile) + ".*";
            foreach (var file in Directory.EnumerateFiles(folder, search, SearchOption.TopDirectoryOnly))
            {
                try
                {
                    File.Delete(file);
                }
                catch (Exception e)
                {
                    Writer.WriteException(e, $"Unable to delete file '{file}'");
                } // catch
            } // foreach
        } // DeleteOldFiles

        private string ChangeTextFormat(string text, string fromFormat, string toFormat)
        {
            switch (toFormat)
            {
                case null:
                    return _plainTextConverter.ConvertFrom(fromFormat, text);
                case "MD":
                    return _markdownConverter.ConvertFrom(fromFormat, text);
                case "HTML":
                    return _htmlConverter.ConvertFrom(fromFormat, text);
                case "RTF":
                    return _rtfConverter.ConvertFrom(fromFormat, text);
                default:
                    Writer.WriteLine("ERROR: unable to transform '{0}' to {1}", fromFormat, toFormat);
                    return null;
            } // switch
        } // ChangeTextFormat

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

        private static string GetLicensedItemType(LicensedItemType type)
        {
            return type switch
            {
                LicensedItemType.Library => LicensingResources.WriteTypeLibrary,
                LicensedItemType.Program => LicensingResources.WriteTypeProgram,
                LicensedItemType.Installer => LicensingResources.WriteTypeInstaller,
                LicensedItemType.Solution => LicensingResources.WriteTypeSolution,
                _ => LicensingResources.WriteUnknown
            };
        } // GetLicensedItemType

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
    } // class LicensesWriter
} // namespace
