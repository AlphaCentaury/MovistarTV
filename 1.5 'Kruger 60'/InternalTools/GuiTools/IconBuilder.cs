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

using IpTviewr.Native;
using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace IpTviewr.Internal.Tools.GuiTools
{
    public partial class IconBuilder : Form
    {
        private class FileInfo
        {
            public string FullPath;
            public Bitmap Image;
            public WindowsIcon.SaveAs SaveAs;
            public Exception Exception;
            internal int Index;
        } // FileInfo

        public IconBuilder()
        {
            InitializeComponent();
            this.Icon = Properties.Resources.GuiTools;
        } // constructor

        private void IconBuilder_Load(object sender, EventArgs e)
        {
            buttonClear_Click(sender, e);

            comboDefaultSaveAs.Items.Add(WindowsIcon.SaveAs.Auto);
            comboDefaultSaveAs.Items.Add(WindowsIcon.SaveAs.Bmp);
            comboDefaultSaveAs.Items.Add(WindowsIcon.SaveAs.Png);
            comboDefaultSaveAs.SelectedIndex = 0;

            comboSaveAs.Items.Add(WindowsIcon.SaveAs.Auto);
            comboSaveAs.Items.Add(WindowsIcon.SaveAs.Bmp);
            comboSaveAs.Items.Add(WindowsIcon.SaveAs.Png);
        } // IconBuilder_Load

        private void listFiles_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop, true);
            var saveAs = (WindowsIcon.SaveAs)comboDefaultSaveAs.SelectedItem;

            if (listFiles.Items[0].Text.StartsWith("<"))
            {
                listFiles.Items.Clear();
            } // if

            foreach (var file in files)
            {
                var info = new FileInfo()
                {
                    FullPath = file,
                    SaveAs = saveAs,
                };
                var item = new ListViewItem(Path.GetFileName(file))
                {
                    Tag = info
                };
                item.SubItems.Add("-");
                item.SubItems.Add("-");
                item.SubItems.Add(saveAs.ToString());
                info.Index = listFiles.Items.Add(item).Index;

                if (textBox1.Text == "")
                {
                    var filename = Path.GetFileNameWithoutExtension(file);
                    var index = filename.LastIndexOf('@');
                    if (index > 0)
                    {
                        textBox1.Text = Path.Combine(Path.GetDirectoryName(file), filename.Substring(0, index) + ".ico");
                    }
                    else
                    {
                        textBox1.Text = Path.ChangeExtension(file, ".ico");
                    } // if-else
                } // if
            } // foreach

            buttonLoadImages.Enabled = true;
            buttonClear.Enabled = true;
        } // listFiles_DragDrop

        private void listFiles_DragEnter(object sender, DragEventArgs e)
        {
            e.Effect = e.Data.GetDataPresent(DataFormats.FileDrop) ? DragDropEffects.Link : DragDropEffects.None;
        } // listFiles_DragEnter

        private void listFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listFiles.SelectedItems.Count == 0)
            {
                comboSaveAs.SelectedIndex = -1;
                return;
            } // if

            var item = listFiles.SelectedItems[0];
            var info = (FileInfo)item.Tag;

            comboSaveAs.SelectedItem = info.SaveAs;
        } // listFiles_SelectedIndexChanged

        private void comboSaveAs_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listFiles.SelectedItems.Count == 0) return;
            var item = listFiles.SelectedItems[0];
            var info = (FileInfo)item.Tag;

            var saveAs = (WindowsIcon.SaveAs)comboDefaultSaveAs.SelectedItem;
            item.SubItems[3].Text = saveAs.ToString();
            info.SaveAs = saveAs;
        } // comboSaveAs_SelectedIndexChanged

        private void buttonLoadImages_Click(object sender, EventArgs e)
        {
            var infos = new FileInfo[listFiles.Items.Count];
            for (var index = 0; index < infos.Length; index++)
            {
                infos[index] = (FileInfo) listFiles.Items[index].Tag;
            } // for index

            buttonLoadImages.Enabled = false;
            buttonSave.Enabled = false;
            listFiles.Enabled = false;

            LoadImages(infos);
        } // buttonLoadImages_Click

        private void buttonSave_Click(object sender, EventArgs e)
        {
            var infos = new FileInfo[listFiles.Items.Count];
            for (var index = 0; index < infos.Length; index++)
            {
                infos[index] = (FileInfo)listFiles.Items[index].Tag;
            } // for index

            var count = 0;
            for (var index = 0; index < infos.Length; index++)
            {
                if ((infos[index].Exception == null) && (infos[index].Image != null))
                {
                    count++;
                } // if
            } // for index

            var icon = new WindowsIcon(count);
            icon.AllowNonSquareImages = true;
            for (var index = 0; index < infos.Length; index++)
            {
                if ((infos[index].Exception == null) && (infos[index].Image != null))
                {
                    try
                    {
                        icon.AddImage(infos[index].Image, infos[index].SaveAs);
                    }
                    catch (ArgumentException)
                    {
                        // ignore: trying to add a duplicate image
                    } // try-catch
                } // if
            } // for index

            icon.Save(textBox1.Text);

            buttonSave.Enabled = false;
        } // buttonSave_Click

        private async void LoadImages(FileInfo[] files)
        {
            for (var index = 0; index < files.Length; index++)
            {
                var file = files[index];
                if ((file.Image != null) || (file.Exception != null)) continue;

                await Task.Run(() => LoadImage(file));

                var item = listFiles.Items[file.Index];
                item.EnsureVisible();
                item.SubItems[1].Text = GetFormat(file);
                if (file.Image != null)
                {
                    item.SubItems[2].Text = string.Format("{0}x{1}@{2}",
                        file.Image.Width, file.Image.Height, GetBitsPerPixel(file.Image.PixelFormat));
                    if ((file.Image.Width > 256) || (file.Image.Height > 256))
                    {
                        file.Exception = new ArgumentOutOfRangeException("Icons bigger than 256x256 are not supported in Windows");
                    } // if
                    if ((file.Image.Width < 16) || (file.Image.Height < 16))
                    {
                        file.Exception = new ArgumentOutOfRangeException("Icons smaller than 16x16 are not supported in Windows");
                    } // if
                } // if
                if (file.Exception != null)
                {
                    if (file.Image != null)
                    {
                        file.Image.Dispose();
                        file.Image = null;
                    } // if
                    item.ForeColor = SystemColors.GrayText;
                    item.ToolTipText = file.Exception.Message;
                } // if
            } // for

            buttonLoadImages.Enabled = false;
            buttonSave.Enabled = true;
            listFiles.Enabled = true;
        } // LoadImages

        private void LoadImage(FileInfo file)
        {
            try
            {
                using (var input = new FileStream(file.FullPath, FileMode.Open, FileAccess.Read))
                {
                    file.Image = new Bitmap(input);
                } // using input
            }
            catch (Exception ex)
            {
                file.Exception = ex;
            } // try-catch
        } // LoadImage

        private string GetFormat(FileInfo file)
        {
            if (file.Exception != null) return "Error";
            if (file.Image.RawFormat.Guid == ImageFormat.Png.Guid) return "Png";
            if (file.Image.RawFormat.Guid == ImageFormat.Jpeg.Guid) return "Jpeg";
            if (file.Image.RawFormat.Guid == ImageFormat.Bmp.Guid) return "Bmp";
            if (file.Image.RawFormat.Guid == ImageFormat.Gif.Guid) return "Gif";
            return "Other";
        } // GetFormat

        private byte GetBitsPerPixel(PixelFormat format)
        {
            switch (format)
            {
                case PixelFormat.Format1bppIndexed:
                    return 1;

                case PixelFormat.Format4bppIndexed:
                    return 4;

                case PixelFormat.Format8bppIndexed:
                    return 8;

                case PixelFormat.Format16bppArgb1555:
                case PixelFormat.Format16bppGrayScale:
                case PixelFormat.Format16bppRgb555:
                case PixelFormat.Format16bppRgb565:
                    return 16;

                case PixelFormat.Format24bppRgb:
                    return 24;

                case PixelFormat.Canonical:
                case PixelFormat.Format32bppArgb:
                case PixelFormat.Format32bppPArgb:
                case PixelFormat.Format32bppRgb:
                    return 32;

                case PixelFormat.Format48bppRgb:
                    return 48;

                case PixelFormat.Format64bppArgb:
                case PixelFormat.Format64bppPArgb:
                    return 64;

                default:
                    throw new ArgumentOutOfRangeException(nameof(format), format, "Unknow or unsupported PixelFormat");
            } // switch format
        } // GetBitsPerPixel

        private void buttonClear_Click(object sender, EventArgs e)
        {
            listFiles.Items.Clear();
            listFiles.Items.Add("<Drop files here>").ForeColor = SystemColors.GrayText;
            buttonLoadImages.Enabled = false;
            buttonClear.Enabled = false;
            buttonSave.Enabled = false;
        }
    } // class IconBuilder
} // namespace
