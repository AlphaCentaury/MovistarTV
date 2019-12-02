// Copyright (C) 2014-2019, GitHub/Codeplex user AlphaCentaury
// 
// All rights reserved, except those granted by the governing license of this software.
// See 'license.txt' file in the project root for complete license information.
// 
// http://www.alphacentaury.org/movistartv https://github.com/AlphaCentaury

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using IpTviewr.UiServices.Configuration.Schema2014.Logos;

namespace IpTviewr.Internal.Tools.ChannelLogosEditor
{
    public partial class DialogCollectionsEditor : Form
    {
        public DialogCollectionsEditor()
        {
            InitializeComponent();
        } // constructor

        public bool IsReadOnly
        {
            get => editor.IsReadOnly;
            set => editor.IsReadOnly = value;
        } // IsReadOnly

        public ServiceCollection[] Collections
        {
            get => editor.Collections;
            set => editor.Collections = value;
        } // Collections

        public ServiceCollection SelectedCollection => editor.SelectedCollection;
    } // class DialogCollectionsEditor
} // namespace
