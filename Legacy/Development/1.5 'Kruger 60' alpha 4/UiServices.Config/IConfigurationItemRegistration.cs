using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;

namespace Project.IpTv.UiServices.Configuration
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
