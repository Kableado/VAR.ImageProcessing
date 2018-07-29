using System;
using System.Windows.Forms;

namespace ImageProcesing.Controls
{
    public class CtrFileTextBox : TextBox
    {
        public CtrFileTextBox()
        {
            DoubleClick += CtrFileTextBox_DoubleClick;
        }

        private void CtrFileTextBox_DoubleClick(object sender, EventArgs e)
        {
            FileDialog fileDialog = new OpenFileDialog();
            DialogResult result = fileDialog.ShowDialog();
            if(result == DialogResult.OK)
            {
                Text = fileDialog.FileName;
            }
        }
    }
}
