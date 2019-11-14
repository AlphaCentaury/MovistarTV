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
