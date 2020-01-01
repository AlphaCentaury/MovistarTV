namespace IpTviewr.Internal.Tools.UiFramework
{
    public interface IRibbonMdiForm
    {
        void SetActiveContexts(IRibbonMdiChild child, params string[] contexts);

        void SetStatusText(string status);
    } // interface IRibbonMdiForm
} // namespace