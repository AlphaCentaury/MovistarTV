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
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using HtmlAgilityPack;
using Markdig;
using Markdig.Helpers;
using HtmlDocument = HtmlAgilityPack.HtmlDocument;

namespace AlphaCentaury.Tools.SourceCodeMaintenance.Licensing.TextConverters
{
    // **************************************************************************
    //
    // This code DOES NOT WORK as intented!
    //
    // **************************************************************************
    //
    // A naive attempt at transforming simple HTML to RTF
    // Currently chokes on nested lists

    public partial class NaiveRtfRenderer
    {
        private readonly Encoding _ansi1252;
        private readonly string[] _bullets;
        private readonly (string Tag, string Rtf)[] _defaults;
        private readonly DefaultsTagComparer _defaultsTagComparer;
        private readonly Dictionary<string, (string Open, string Close)> _tags;
        private readonly Dictionary<string, Action<HtmlNode>> _specialTags;
        private readonly List<List<string>> _listTable;
        private readonly Stack<ListInfo> _listsStack;
        private readonly int _indentTwips;
        private MarkdownPipeline _mdPipeline;
        private List<string> _currentListDefinition;

        private StringBuilder Output { get; }

        #region Public API

        public NaiveRtfRenderer()
        {
            Output = new StringBuilder();
            _ansi1252 = Encoding.GetEncoding(1252, EncoderFallback.ReplacementFallback, DecoderFallback.ReplacementFallback);

            _bullets = CreateBulletsList();
            _defaultsTagComparer = new DefaultsTagComparer();
            _defaults = CreateDefaultsList();
            _specialTags = CreateSpecialTagsDictionary();
            _tags = CreateTagsDictionary();
            _listTable = new List<List<string>>();
            _listsStack = new Stack<ListInfo>();

            _indentTwips = 360;
        } // constructor

        public string HtmlToRtf(string html)
        {
            var doc = new HtmlDocument();
            doc.LoadHtml(html);

            HtmlToRtf(doc.DocumentNode);

            var header = GetHeader();
            WriteFooter();
            Output.EnsureCapacity(Output.Length + header.Length);
            Output.Insert(0, header.ToString());

            return Output.ToString();
        } // HtmlToRtf

        public string MarkdownToRtf(string markdown)
        {
            _mdPipeline ??= new MarkdownPipelineBuilder().UseAdvancedExtensions().Build();

            markdown = Markdown.Normalize(markdown, null, _mdPipeline);
            return HtmlToRtf(Markdown.ToHtml(markdown));
        } // MarkdownToRtf

        #endregion

        #region Initialization

        private static string[] CreateBulletsList()
        {
            return new[]
            {
                @"\'B7",
                @"\'A8",
                @"\'AE",
                @"\'BE",
            };
        } // CreateBulletsList

        private static (string Tag, string Rtf)[] CreateDefaultsList()
        {
            var list = new (string Tag, string Rtf)[]
            {  
                // sa     = space after in twips (default is 0)
                // sl     = space between lines
                // slmult = line spacing multiple
                ("p", @"\sa200\sl276\slmult1 "),
                ("ol:level", @"\levelnfc0\leveljc0\levelstartat1{{\leveltext\'02\'{0:00}.;}}{{\levelnumbers\'01;}}\jclisttab\tx360"), // {0} = level
                ("ul:level", @"\levelnfc23\leveljc0\levelstartat1{{\leveltext\'01{0};}}{{\levelnumbers;}}\f1\jclisttab\tx360"), // {0} = bullet symbol
            };

            var sorted = list.OrderBy(item => item.Tag, StringComparer.InvariantCultureIgnoreCase);

            var result = new (string Tag, string Rtf)[list.Length];
            var index = 0;
            foreach (var item in sorted)
            {
                result[index++] = item;
            } // foreach

            return result;
        } // CreateDefaultsList

        private string GetDefaults(string tag)
        {
            var index = Array.BinarySearch(_defaults, (tag, null), _defaultsTagComparer);
            return (index >= 0) ? _defaults[index].Rtf : null;
        } // tag

        private Dictionary<string, Action<HtmlNode>> CreateSpecialTagsDictionary()
        {
            return new Dictionary<string, Action<HtmlNode>>(StringComparer.InvariantCultureIgnoreCase)
            {
                {"head", TagHead },
                {"a", TagA },
                {"ol", TagList },
                {"ul", TagList },
                {"li", TagLi },
                {"#text", WriteText }
            };
        } // CreateSpecialTagsDictionary

        private Dictionary<string, (string Open, string Close)> CreateTagsDictionary()
        {
            var tags = new Dictionary<string, (string Open, string Close)>(StringComparer.InvariantCultureIgnoreCase)
            {
                // pard = resets to default paragraph properties
                // par  = new paragraph
                {"p", (@"\pard{0}", "\\par\r\n")}, // {0} = <p> defaults

                // b  = bold
                // b0 = bold off
                {"b", (@"\b ", @"\b0 ") },
                {"strong", (@"\b ", @"\b0 ") },

                // i  = italic
                // i0 = italic off
                {"i", (@"\i ", @"\i0 ") },
                {"em", (@"\i ", @"\i0 ") },

                // i  = underline
                // i0 = underline off
                {"u", (@"\ul ", @"\ul0 ") },

                // line = required line break (no paragraph break).
                {"br", (@"\line ", "") },

                // pard = resets to default paragraph properties
                // \fs  = font size in half-points (default is 24 = 12pt)
                // par  = new paragraph
                {"h1", (@"\pard{0}\fs52", "\\fs22\\par\r\n") },
                {"h2", (@"\pard{0}\fs46", "\\fs22\\par\r\n") },
                {"h3", (@"\pard{0}\fs36", "\\fs22\\par\r\n") },
                {"h4", (@"\pard{0}\fs30\i", "\\fs22\\i0\\par\r\n") },
                {"h5", (@"\pard{0}\fs24\b\i", "\\fs22\\b0\\i0\\par\r\n") },
            };

            // add 'defaults'
            var defaultP = GetDefaults("p");

            var tag = tags["p"];
            tags["p"] = (string.Format(tag.Open, defaultP), tag.Close);

            tag = tags["h1"];
            tags["h1"] = (string.Format(tag.Open, defaultP), tag.Close);
            tag = tags["h2"];
            tags["h2"] = (string.Format(tag.Open, defaultP), tag.Close);
            tag = tags["h3"];
            tags["h3"] = (string.Format(tag.Open, defaultP), tag.Close);
            tag = tags["h3"];
            tags["h4"] = (string.Format(tag.Open, defaultP), tag.Close);
            tag = tags["h4"];
            tags["h4"] = (string.Format(tag.Open, defaultP), tag.Close);
            tag = tags["h5"];
            tags["h5"] = (string.Format(tag.Open, defaultP), tag.Close);

            return tags;
        } // CreateTagsDictionary

        #endregion

        #region RTF header & footer

        private StringBuilder GetHeader()
        {
            var header = new StringBuilder();

            header.AppendLine(@"{\rtf1\ansi\ansicpg1252\deff0\nouicompat\deflang1033\deflangfe3082");
            AddFontTable(header);
            AddColorTable(header);
            AddListTable(header);
            header.AppendFormat(@"{{\*\generator {0} v{1}}}", GetType().FullName, Application.ProductVersion);
            header.Append(@"\viewkind0\uc1");
            header.Append(@"\fs22");
            header.AppendLine();

            return header;
        } // WriteHeader

        private void AddFontTable(StringBuilder header)
        {
            header.Append(@"{\fonttbl");
            header.Append(@"{\f0\fnil\fcharset0 Calibri;}");
            header.Append(@"{\f1\fnil\fcharset2 Symbol;}");
            header.AppendLine(@"}");
        } // WriteFontTable

        private void AddColorTable(StringBuilder header)
        {

        } // AddColorTable

        private void AddListTable(StringBuilder header)
        {
            if (_listTable.Count == 0) return;

            // \listtable = List Table: a list of lists (destination \list).
            //              Each list contains a number of list properties that pertain to the entire list,
            //              and a list of levels (destination \listlevel), each of which contains properties that pertain only to that level.
            header.AppendLine(@"{\*\listtable");
            for (var index = 0; index < _listTable.Count; index++)
            {
                var table = _listTable[index];
                // \list       = start of list definitions
                // \listhybrid = present if the list has 9 levels, each of which is the equivalent of a simple list; only one of \listsimpleN and \listhybrid should be present.
                header.AppendLine(@"{\list\listhybrid");
                foreach (var level in table)
                {
                    // \listlevel = start of list level
                    header.Append(@"{\listlevel");
                    header.Append(level);
                    header.AppendLine("}");
                } // foreach

                // \listid = each list must have a unique list ID [that should be randomly generated]
                header.AppendFormat("\\listid{0} }}", index + 1);
                header.AppendLine();
            } // for

            header.AppendLine(@"}");
        } // AddListTable

        private void WriteFooter()
        {
            Output.Append("}\0");
        } // WriteFooter

        #endregion

        private void HtmlToRtf(HtmlNode node)
        {
            var name = node.Name;
            if (_specialTags.TryGetValue(name, out var action))
            {
                action(node);
                return;
            } // if

            if (_tags.TryGetValue(name, out var tuple))
            {
                Output.Append(tuple.Open);
                foreach (var child in node.ChildNodes)
                {
                    HtmlToRtf(child);
                } // foreach
                Output.Append(tuple.Close);
                return;
            } // if

            foreach (var child in node.ChildNodes)
            {
                HtmlToRtf(child);
            } // foreach
        } // HtmlToRtf

        #region Special tags handling

        private static void TagHead(HtmlNode node)
        {
            // ignore node
        } // TagHead

        private void TagA(HtmlNode node)
        {
            // TODO: transform to RTF link
            WriteText(node.InnerText);
        } // TagA

        #endregion

        #region Special tags: ul / ol / li

        private void TagList(HtmlNode node)
        {
            BeginListDefinition(node.Name);
            var previous = GetPreviousListInfo();

            var info = new ListInfo
            {
                ListId = previous.ListId,
                Depth = previous.Depth + 1,
                Tag = node.Name
            };

            switch (info.Tag)
            {
                case "ul":
                    info.OlLevel = -1;
                    info.UlLevel = previous.UlLevel + 1;
                    info.Bullet = _bullets[info.UlLevel % _bullets.Length];
                    if (info.Depth == _currentListDefinition.Count)
                    {
                        _currentListDefinition.Add(string.Format(GetDefaults("ul:level"), info.Bullet));
                    } // if

                    break;

                case "ol":
                    info.OlLevel = previous.OlLevel + 1;
                    info.UlLevel = -1;
                    if (info.Depth == _currentListDefinition.Count)
                    {
                        _currentListDefinition.Add(string.Format(CultureInfo.InvariantCulture, GetDefaults("ol:level"), info.Depth));
                    } // if

                    break;

                default:
                    throw new InvalidDataException();
            } // switch

            // if nested, 'end' li
            Output.Append("<end>\\par\r\n");

            _listsStack.Push(info);
            foreach (var child in node.ChildNodes)
            {
                HtmlToRtf(child);
            } // foreach

            _listsStack.Pop();
            EndListDefinition();
        } // TagList

        private void BeginListDefinition(string tag)
        {
            if (_currentListDefinition != null) return;

            _currentListDefinition = new List<string>();
            _listTable.Add(_currentListDefinition);
        } // BeginListDefinition

        private void EndListDefinition()
        {
            if (_listsStack.Count == 0)
            {
                _currentListDefinition = null;
            } // if
        } // EndListDefinition

        private ListInfo GetPreviousListInfo()
        {
            if (_listsStack.Count > 0) return _listsStack.Peek();

            return new ListInfo
            {
                ListId = _listTable.Count,
                Depth = -1,
                OlLevel = -1,
                UlLevel = -1,
                Tag = null,
            };
        } // GetPreviousListInfo

        private void TagLi(HtmlNode node)
        {
            var info = _listsStack.Peek();

            if (info.ItemCount == 0)
            {
                // start first li
                Output.AppendFormat(@"\pard");
                AddAlternateText();
                Output.AppendFormat(@"\jclisttab\tx{0}\ls{1}", _indentTwips, info.ListId);
                if (info.Depth > 0) Output.AppendFormat(@"\ilvl{0}", info.Depth);
                Output.AppendFormat(@"\widctlpar\fi{0}\li{1}\sa{2} ", (-1 * _indentTwips), (_indentTwips / 2) + (_indentTwips * info.Depth), 100);
            }
            else
            {
                // start new li
                AddAlternateText();
            } // if-else

            foreach (var child in node.ChildNodes)
            {
                if (string.Equals(child.Name, "p", StringComparison.InvariantCultureIgnoreCase))
                {
                    foreach (var pChild in child.ChildNodes)
                    {
                        HtmlToRtf(pChild);
                    } // foreach
                }
                else
                {
                    HtmlToRtf(child);
                } // if-else
            } // foreach

            // close li
            Output.AppendLine("</li>\\par\r\n");

            void AddAlternateText()
            {
                info.ItemCount++;
                if (info.Bullet == null)
                {
                    Output.AppendFormat(@"{{\listtext\f0 {0:D}.\tab}}", info.ItemCount);
                }
                else
                {
                    Output.AppendFormat(@"{{\listtext\f1{0}\tab}}", info.Bullet);
                } // if-else
            } // AddAlternateText
        } // TagLi

        #endregion

        private void WriteText(HtmlNode node)
        {
            WriteText(node.InnerText);
        } // WriteText

        private void WriteText(string text)
        {
            if (string.IsNullOrEmpty(text)) return;

            var start = true;
            for (var index = 0; index < text.Length; index++)
            {
                var c = text[index];

                if (c.IsWhiteSpaceOrZero())
                {
                    var prev = index > 0 ? text[index - 1] : '\0';
                    // skip repeated spaces
                    if (prev == ' ') continue;
                    if (!start) Output.Append(' ');
                }
                else
                {
                    start = false;
                    index = WriteChar(text, index);
                } // if-else
            } // for
        } // WriteTex

        private int WriteChar(string text, int index)
        {
            var c = text[index];

            switch (c)
            {
                case '\r':
                    Output.Append("\r\n");

                    var n = (index + 1) < text.Length ? text[index + 1] : '\0';
                    if ((n == '\n') || (n == '\0'))
                    {
                        index++;
                    } // if

                    break;

                case '\n':
                    Output.Append("\r\n");
                    break;

                case '\\':
                    Output.Append(@"\\");
                    break;

                case '&':
                    var end = text.IndexOf(';', index);
                    if (end > 0)
                    {
                        WriteText(HtmlEntity.DeEntitize(text.Substring(index, 1 + end - index)));
                        index = end + 1;
                    }
                    else
                    {
                        Output.Append('&');
                    } // if-else

                    break;

                default:
                    if (c < ' ')
                    {
                        // ommit char
                    }
                    else if (c < '\x80')
                    {
                        Output.Append(c);
                    }
                    else
                    {
                        unchecked
                        {
                            Output.AppendFormat(@"\u{0}\'{1:x2}", (short)(ushort)c, _ansi1252.GetBytes(c.ToString())[0]);
                        } // unchecked
                    } // if-ese

                    break;
            } // switch

            return index;
        } // WriteChar
    } // class NaiveRtfRenderer
} // namespace
