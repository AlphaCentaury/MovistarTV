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

namespace IpTviewr.Internal.Tools.UiFramework
{
    public sealed class CliToolData : ToolData<ICliTool, IToolMetadata, ICliToolDataProvider>
    {
        public CliToolData(Guid guid, ICliToolDataProvider data, Lazy<ICliTool, IToolMetadata> tool, bool isGuiTool) : base(guid, data, tool, isGuiTool)
        {
            // no-op
        } // constructor

        internal static CliToolData New(Guid guid, ICliToolDataProvider data, Lazy<ICliTool, IToolMetadata> tool, bool isGuiTool)
        {
            return new CliToolData(guid, data, tool, isGuiTool);
        } // New
    } // class CliToolData
} // namespace
