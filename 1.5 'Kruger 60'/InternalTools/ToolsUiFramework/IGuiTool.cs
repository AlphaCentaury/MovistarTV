using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.UiFramework
{
    public interface IGuiTool: ITool
    {
        Form CreateForm();
    } // interface IGuiTool
} // namespace