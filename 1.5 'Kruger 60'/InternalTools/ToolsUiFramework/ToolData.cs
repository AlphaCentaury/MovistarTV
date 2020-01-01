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
using System.Drawing;
using System.Linq;

namespace IpTviewr.Internal.Tools.UiFramework
{
    public abstract class ToolData<TTool, TMetadata, TProvider> where TTool : ITool where TMetadata : IToolMetadata where TProvider : IToolDataProvider
    {
        private readonly IList<KeyValuePair<int, Image>> _images;
        private readonly TProvider _data;

        internal ToolData(Guid guid, TProvider data, Lazy<TTool, TMetadata> tool, bool isGuiTool)
        {
            _images = new List<KeyValuePair<int, Image>>();
            _data = data;
            Guid = guid;
            Category = data.Category;
            Name = data.Name;
            IsGuiTool = isGuiTool;
            Data = data;
            Tool = tool;
        } // constructor

        public Guid Guid { get; }

        public string Category { get; }

        public string Name { get; }

        public bool IsGuiTool { get; }

        public Image GetLogo(int size)
        {
            var img = _images.FirstOrDefault(item => item.Key == size);
            if (img.Value != null) return img.Value;

            img = new KeyValuePair<int, Image>(size, _data.GetLogo(size));
            _images.Add(img);
            return img.Value;
        } // GetLogo

        public Lazy<TTool, TMetadata> Tool { get; }
        public TProvider Data { get; }
    } // // class ToolData
} // namespace
