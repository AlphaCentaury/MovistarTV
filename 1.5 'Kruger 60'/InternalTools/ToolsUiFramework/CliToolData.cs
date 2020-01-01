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
