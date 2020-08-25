using System.ComponentModel.Composition;
using System.Drawing;
using System.Windows.Forms;
using IpTviewr.Internal.Tools.UiFramework;

namespace IpTviewr.Internal.Tools.GuiTools
{
    [Export(typeof(IGuiTool))]
    [Export(typeof(IGuiToolDataProvider))]
    [ExportMetadata("Guid", ToolGuid)]
    public class MulticastStreamExplorerTool : IGuiTool, IGuiToolDataProvider
    {
        public const string ToolGuid = "{D082DC39-542C-42F9-9777-A824CFACCF43}";

        #region Implementation of IGuiTool

        public Form CreateForm() => new MulticastStreamExplorerForm();

        #endregion

        #region Implementation of IToolDataProvider

        public string Guid => ToolGuid;
        public string Category => "Multicast / DVB-STP";
        public string Name => "Multicast Stream Explorer";
        public Image GetLogo(int size) => null;

        #endregion
    } // class MulticastStreamExplorerTool
} // namespace