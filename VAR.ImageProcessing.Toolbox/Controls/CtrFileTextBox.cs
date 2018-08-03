using System;
using System.IO;
using System.Windows.Forms;

namespace VAR.ImageProcessing.Toolbox.Controls
{
    public class CtrFileTextBox : TextBox
    {
        public bool DirectoryChooser { get; set; } = false;

        public CtrFileTextBox()
        {
            DoubleClick += CtrFileTextBox_DoubleClick;
            TextChanged += CtrFileTextBox_TextChanged;
        }

        private void CtrFileTextBox_TextChanged(object sender, EventArgs e)
        {
            if(Text.StartsWith("\"") && Text.EndsWith("\""))
            {
                Text = Text.Substring(1, Text.Length - 2);
            }
        }

        private void CtrFileTextBox_DoubleClick(object sender, EventArgs e)
        {
            if (DirectoryChooser)
            {
                ShowDirectorySelector();
            }
            else
            {
                ShowFileSelector();
            }
        }

        private void ShowFileSelector()
        {
            FileDialog fileDialog = new OpenFileDialog();
            if (string.IsNullOrEmpty(Text) == false)
            {
                string path = Path.GetDirectoryName(Text);
                fileDialog.InitialDirectory = path;
            }
            DialogResult result = fileDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Text = fileDialog.FileName;
            }
        }

        private void ShowDirectorySelector()
        {
            FolderBrowserDialog folderBrowserDialog = new FolderBrowserDialog();
            DialogResult result = folderBrowserDialog.ShowDialog();
            if (result == DialogResult.OK)
            {
                Text = folderBrowserDialog.SelectedPath;
            }
        }
    }
}
