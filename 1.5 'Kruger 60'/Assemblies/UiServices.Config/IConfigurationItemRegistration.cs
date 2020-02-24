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
using System.Drawing;

namespace IpTviewr.UiServices.Configuration
{
    public interface IConfigurationItemRegistration
    {
        Guid Id
        {
            get;
        } // Id

        Type ItemType
        {
            get;
        } // ItemType

        int DirectIndex
        {
            get;
            set;
        } // DirectIndex

        IConfigurationItem CreateDefault();

        bool HasEditor
        {
            get;
        } // HasEditor

        string EditorDisplayName
        {
            get;
        } // EditorDisplayName

        string EditorDescription
        {
            get;
        } // EditorDescription

        Image EditorImage
        {
            get;
        } // EditorImage

        int EditorPriority
        {
            get;
        } // EditorPriority

        IConfigurationItemEditor CreateEditor(IConfigurationItem data);
    } // interface IConfigurationItemRegistration
} // namespace
