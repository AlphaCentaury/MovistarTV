using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows.Forms;
using IpTviewr.Internal.Tools.UiFramework;

namespace IpTviewr.Internal.Tools.GuiTools
{
    [Export(typeof(IGuiTool))]
    [Export(typeof(IGuiToolDataProvider))]
    [ExportMetadata("Guid", ToolGuid)]
    public class SimpleDvbStpDownloadTool : IGuiTool, IGuiToolDataProvider
    {
        public const string ToolGuid = "{9CD277C0-F9FF-4145-A428-D5A631840D91}";

        #region Implementation of IGuiTool

        public Form CreateForm() => new SimpleDvbStpDownloadForm();

        #endregion

        #region Implementation of IToolDataProvider

        public string Guid => ToolGuid;
        public string Category => "Multicast / DVB-STP";
        public string Name => "DVB-STP Payload Downloader";
        public Image GetLogo(int size) => null;

        #endregion
    } // class SimpleDvbStpDownloadTool
} // namespace