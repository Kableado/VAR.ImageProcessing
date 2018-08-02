using ImageProcesing.Code;
using System;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Imaging;
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
                int skipPixels = Math.Min(imgStart.Width, imgStart.Height) / 100;
                FindDeltaPosition findDeltaPosition = new FindDeltaPosition(imgStart, imgEnd);
                Stopwatch sw = Stopwatch.StartNew();
                FindDeltaPosition.OffsetPosition offsetPosition = findDeltaPosition.SearchOnSpiral(0.15f, skipPixels);
                sw.Stop();
                ctrOutput.AddLine(string.Format("Best: [X: {0}, Y: {1}] (Error:{2})",
                    offsetPosition.OffsetX, offsetPosition.OffsetY, offsetPosition.MeanSquareError));
                ctrOutput.AddLine(string.Format("Time {0} MS", sw.ElapsedMilliseconds));
                Application.DoEvents();
                
                // Apply Offset
                Bitmap bmpResult = new Bitmap(imgStart.Width, imgStart.Height, PixelFormat.Format24bppRgb);
                Graphics g = Graphics.FromImage(bmpResult);
                g.DrawImage(imgEnd, 0, 0, imgEnd.Width, imgEnd.Height);
                g.DrawImage(imgEnd, offsetPosition.OffsetX, offsetPosition.OffsetY, imgEnd.Width, imgEnd.Height);
                string strFileEndProcessed = string.Format("{0}/{1}.offset{2}",
                    Path.GetDirectoryName(txtPathImageEnd.Text),
                    Path.GetFileNameWithoutExtension(txtPathImageEnd.Text),
                    Path.GetExtension(txtPathImageEnd.Text));
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
