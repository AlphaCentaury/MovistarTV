using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows.Forms;
using IpTviewr.Internal.Tools.UiFramework;

namespace IpTviewr.Internal.Tools.GuiTools
{
    [Export(typeof(IGuiTool))]
    [Export(typeof(IGuiToolDataProvider))]
    [ExportMetadata("Guid", ToolGuid)]
    public class RawEpgDownloaderTool : IGuiTool, IGuiToolDataProvider
    {
        public const string ToolGuid = "{BFE0F5FE-AF74-42F9-BD1E-42E0D718C59F}";

        #region Implementation of IGuiTool

        public Form CreateForm() => new RawEpgDownloaderForm();

        #endregion

        #region Implementation of IToolDataProvider

        public string Guid => ToolGuid;
        public string Category => "EPG";
        public string Name => "Raw EPG Downloader";
        public Image GetLogo(int size) => null;

        #endregion
    } // class RawEpgDownloaderTool
} // namespace