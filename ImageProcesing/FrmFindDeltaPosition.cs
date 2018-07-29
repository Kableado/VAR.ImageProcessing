using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace ImageProcesing
{
    public partial class FrmFindDeltaPosition : Form
    {
        public FrmFindDeltaPosition()
        {
            InitializeComponent();
        }

        private void btnClean_Click(object sender, EventArgs e)
        {
            ctrImageViewer.ImageShow = null;
            ctrOutput.Clean();
        }

        private void btnCalculate_Click(object sender, EventArgs e)
        {
            ctrOutput.Clean();
            if (File.Exists(txtPathImageStart.Text) == false)
            {
                ctrOutput.AddLine(string.Format("Start image not found: \"{0}\"", txtPathImageStart.Text));
                return;
            }
            if (File.Exists(txtPathImageEnd.Text) == false)
            {
                ctrOutput.AddLine(string.Format("End image not found: \"{0}\"", txtPathImageEnd.Text));
                return;
            }
            try
            {
                Image imgStart = Image.FromFile(txtPathImageStart.Text);
                Image imgEnd = Image.FromFile(txtPathImageEnd.Text);
                ctrImageViewer.ImageShow = imgStart;

            }
            catch (Exception ex)
            {
                ctrOutput.AddLine("!!!!!!!!!!!!!!!!!!!!!!!!");
                ctrOutput.AddLine(ex.Message);
                ctrOutput.AddLine("---------------------");
                ctrOutput.AddLine(ex.StackTrace);
            }
        }
    }
}
