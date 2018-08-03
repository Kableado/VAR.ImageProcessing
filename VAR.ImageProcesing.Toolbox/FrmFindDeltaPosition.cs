using System;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VAR.ImageProcesing.Toolbox.Code;

namespace VAR.ImageProcesing.Toolbox
{
    public partial class FrmFindDeltaPosition : Form
    {
        public FrmFindDeltaPosition()
        {
            InitializeComponent();
        }

        private void btnGenMap_Click(object sender, EventArgs e)
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

                FindDeltaPosition findDeltaPosition = new FindDeltaPosition(imgStart, imgEnd);
                int skipPixels = Math.Min(imgStart.Width, imgStart.Height) / 100;
                int skipChecks = Math.Min(imgStart.Width, imgStart.Height) / 20;
                Bitmap bmpErrorMap = findDeltaPosition.GenerateErrorMap(skipChecks, skipPixels);
                ctrImageViewer.ImageShow = bmpErrorMap;
                Application.DoEvents();
            }
            catch (Exception ex)
            {
                ctrOutput.AddLine("!!!!!!!!!!!!!!!!!!!!!!!!");
                ctrOutput.AddLine(ex.Message);
                ctrOutput.AddLine("---------------------");
                ctrOutput.AddLine(ex.StackTrace);
            }
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
                Application.DoEvents();

                // Search offset
                int skipPixels = Math.Min(imgStart.Width, imgStart.Height) / 150;
                FindDeltaPosition findDeltaPosition = new FindDeltaPosition(imgStart, imgEnd);
                Stopwatch sw = Stopwatch.StartNew();
                FindDeltaPosition.OffsetPosition offsetPosition = findDeltaPosition.SearchRecursive(30, skipPixels);
                sw.Stop();
                ctrOutput.AddLine(string.Format("Best: [X: {0}, Y: {1}] (Error:{2})",
                    offsetPosition.OffsetX, offsetPosition.OffsetY, offsetPosition.MeanSquareError));
                ctrOutput.AddLine(string.Format("Time {0} MS", sw.ElapsedMilliseconds));
                Application.DoEvents();

                // Apply Offset
                Bitmap bmpResult = ImageHelper.OffsetImage(offsetPosition.OffsetX, offsetPosition.OffsetY, imgEnd);
                string strFileEndProcessed = PathHelper.AddSuffixToFilePath(txtPathImageEnd.Text, ".offset");
                bmpResult.Save(strFileEndProcessed);
                ctrOutput.AddLine(string.Format("Offset applied to \"{0}\"", strFileEndProcessed));
                Application.DoEvents();
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
