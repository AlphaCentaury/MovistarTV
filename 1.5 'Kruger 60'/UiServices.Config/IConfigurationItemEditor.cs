using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Project.IpTv.UiServices.Configuration
{
    public interface IConfigurationItemEditor
    {
        UserControl UserInterfaceItem
        {
            get;
        } // UserInterfaceItem

        bool SupportsWinFormsValidation
        {
            get;
        } // SupportsWinFormsValidation

        bool IsDataChanged
        {
            get;
        } // IsDataChanged

        bool IsAppRestartNeeded
        {
            get;
        } // IsAppRestartNeeded

        bool Validate();
        IConfigurationItem GetNewData();

        void EditorClosing(out bool cancelClose);
        void EditorClosed(bool userCancel);
    } // interface IConfigurationFormItem
} // namespace
