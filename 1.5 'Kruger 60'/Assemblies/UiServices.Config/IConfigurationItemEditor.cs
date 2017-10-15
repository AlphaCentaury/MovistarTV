// Copyright (C) 2014-2017, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://movistartv.alphacentaury.org/ https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace IpTviewr.UiServices.Configuration
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
