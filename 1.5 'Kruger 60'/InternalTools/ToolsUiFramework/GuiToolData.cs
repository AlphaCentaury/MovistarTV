using System;

namespace IpTviewr.Internal.Tools.UiFramework
{
    public sealed class GuiToolData : ToolData<IGuiTool, IToolMetadata, IGuiToolDataProvider>
    {
        public GuiToolData(Guid guid, IGuiToolDataProvider data, Lazy<IGuiTool, IToolMetadata> tool, bool isGuiTool) : base(guid, data, tool, isGuiTool)
        {
            // no-op
        } // constructor

        internal static GuiToolData New(Guid guid, IGuiToolDataProvider data, Lazy<IGuiTool, IToolMetadata> tool, bool isGuiTool)
        {
            return new GuiToolData(guid, data, tool, isGuiTool);
        } // New
    } // class GuiToolData
} // namespace
