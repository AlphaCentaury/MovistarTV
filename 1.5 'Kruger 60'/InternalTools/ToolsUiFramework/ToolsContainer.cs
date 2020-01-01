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

using JetBrains.Annotations;
using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Composition;
using System.ComponentModel.Composition.Hosting;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.UiFramework
{
    public sealed class ToolsContainer
    {
        private static readonly Lazy<ToolsContainer> LazyCurrent = new Lazy<ToolsContainer>();

        public ToolsContainer()
        {
            Compose();
        } // constructor

        public static ToolsContainer Current => LazyCurrent.Value;

#pragma warning disable CS0649

        [ImportMany]
        [SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]
        [CanBeNull]
        private Lazy<IGuiTool, IToolMetadata>[] _guiTools;

        [ImportMany]
        [SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]
        [CanBeNull]
        private Lazy<ICliTool, IToolMetadata>[] _cliTools;

        [ImportMany]
        [SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]
        [CanBeNull]
        private Lazy<IGuiToolDataProvider>[] _guiData;

        [ImportMany]
        [SuppressMessage("Style", "IDE0044:Add readonly modifier", Justification = "<Pending>")]
        [CanBeNull]
        private Lazy<ICliToolDataProvider>[] _cliData;

#pragma warning restore CS0649

        private List<(CultureInfo Culture, IReadOnlyList<(string Category, GuiToolData[] Data)> List)> _sortedGuiTools;

        private List<(CultureInfo Culture, IReadOnlyList<(string Category, CliToolData[] Data)> List)> _sortedCliTools;

        public IReadOnlyList<GuiToolData> GuiTools { get; private set; }

        public IReadOnlyList<CliToolData> CliTools { get; private set; }

        public IReadOnlyList<(string Category, GuiToolData[] Data)> GuiToolsList { get; private set; }

        public IReadOnlyList<(string Category, CliToolData[] Data)> CliToolsList { get; private set; }

        public CompositionContainer Container { get; private set; }

        public AggregateCatalog Catalog { get; private set; }

        public IReadOnlyList<(string Category, GuiToolData[] Data)> GetSortedGuiTools([CanBeNull] CultureInfo culture)
        {
            return GetSortedTools<IGuiTool, IToolMetadata, IGuiToolDataProvider, GuiToolData>(culture, GuiToolsList, _sortedGuiTools);
        } // GetSortedGuiTools

        public IReadOnlyList<(string Category, CliToolData[] Data)> GetSortedCliTools([CanBeNull] CultureInfo culture)
        {
            return GetSortedTools<ICliTool, IToolMetadata, ICliToolDataProvider, CliToolData>(culture, CliToolsList, _sortedCliTools);
        } // GetSortedCliTools

        internal void Compose()
        {
            var catalog = new AggregateCatalog(new ApplicationCatalog());
            Catalog = catalog;
            var toolsPath = Path.Combine(Application.StartupPath, "tools");
            if (Directory.Exists(toolsPath))
            {
                catalog.Catalogs.Add(new DirectoryCatalog(toolsPath));
            } // if

            var container = new CompositionContainer(catalog);
            Container = container;
            container.ComposeParts(this);

            GuiTools = Build(_guiTools, _guiData, true, GuiToolData.New);
            CliTools = Build(_cliTools, _cliData, true, CliToolData.New);

            GuiToolsList = BuildList<IGuiTool, IToolMetadata, IGuiToolDataProvider, GuiToolData>(GuiTools);
            CliToolsList = BuildList<ICliTool, IToolMetadata, ICliToolDataProvider, CliToolData>(CliTools);

            _sortedGuiTools = new List<(CultureInfo Culture, IReadOnlyList<(string Category, GuiToolData[] Data)> List)>();
            _sortedCliTools = new List<(CultureInfo Culture, IReadOnlyList<(string Category, CliToolData[] Data)> List)>();

            _ = GetSortedGuiTools(null);
            _ = GetSortedCliTools(null);
        } // Compose

        private static List<TToolData> Build<TTool, TMetadata, TProvider, TToolData>(ICollection<Lazy<TTool, TMetadata>> tools,
            IEnumerable<Lazy<TProvider>> toolsData,
            bool isGuiTool,
            Func<Guid, TProvider, Lazy<TTool, TMetadata>, bool, TToolData> createNew)
            where TTool : ITool
            where TMetadata : IToolMetadata
            where TProvider : IToolDataProvider
            where TToolData : ToolData<TTool, TMetadata, TProvider>
        {
            var toolsDictionary = new Dictionary<Guid, TProvider>();
            var list = new List<TToolData>(tools.Count);

            foreach (var data in toolsData.Select(lazy => lazy.Value))
            {
                if (!Guid.TryParse(data.Guid, out var guid)) continue;

                toolsDictionary.Add(guid, data);
            } // foreach data

            foreach (var tool in tools)
            {
                if (!Guid.TryParse(tool.Metadata.Guid, out var guid)) continue;
                if (!toolsDictionary.TryGetValue(guid, out var data)) continue;

                var toolData = createNew(guid, data, tool, isGuiTool);
                list.Add(toolData);
            } // foreach tool

            return list;
        } // namespace

        private static List<(string Category, TToolData[] Data)> BuildList<TTool, TMetadata, TProvider, TToolData>(IEnumerable<TToolData> tools) where TTool : ITool
            where TMetadata : IToolMetadata
            where TProvider : IToolDataProvider
            where TToolData : ToolData<TTool, TMetadata, TProvider>
        {
            var q = from tool in tools
                    group tool by tool.Category;

            var list = q.Select(z => (z.Key, z.ToArray())).ToList();
            return list;
        } // BuildList

        private static IReadOnlyList<(string Category, TToolData[] Data)> GetSortedTools<TTool, TMetadata, TProvider, TToolData>(CultureInfo culture,
            IEnumerable<(string Category, TToolData[] Data)> toolsList,
            ICollection<(CultureInfo Culture, IReadOnlyList<(string Category, TToolData[] Data)> List)> cultureList) where TTool : ITool
            where TMetadata : IToolMetadata
            where TProvider : IToolDataProvider
            where TToolData : ToolData<TTool, TMetadata, TProvider>
        {
            if (culture == null) culture = CultureInfo.CurrentCulture;
            var list = cultureList.FirstOrDefault(item => item.Culture.Equals(culture));
            if (list.List != null) return list.List;

            var comparer = StringComparer.Create(culture, true);
            var result = toolsList.OrderBy(x => x.Category, comparer).ToList();
            for (var index = 0; index < result.Count; index++)
            {
                var (category, data) = result[index];
                var tools = data.OrderBy(tool => tool.Name, comparer);
                result[index] = (category, tools.ToArray());
            } // for index

            cultureList.Add((culture, result));
            return result;
        } // GetSortedTools
    } // class ToolsContainer
}
