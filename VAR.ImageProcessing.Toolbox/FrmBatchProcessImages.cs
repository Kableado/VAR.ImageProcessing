using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using VAR.ImageProcessing.Toolbox.Code;

namespace VAR.ImageProcessing.Toolbox
{
    public partial class FrmBatchProcessImages : Form
    {
        public FrmBatchProcessImages()
        {
            InitializeComponent();
        }

        private void btnProcess_Click(object sender, EventArgs e)
        {
            try
            {
                ctrOutput.Clean();
                if (Directory.Exists(ctrFileTextBox.Text) == false)
                {
                    ctrOutput.AddLine(string.Format("Directory not found: \"{0}\"", ctrFileTextBox.Text));
                    return;
                }

                List<string> imageFiles = FileHelper.GetImageFilesOnDirectory(ctrFileTextBox.Text);
                string previousFile = null;
                foreach (string file in imageFiles)
                {
                    if (previousFile == null)
                    {
                        previousFile = file;
                        continue;
                    }

                    Image imgStart = Image.FromFile(previousFile);
                    Image imgEnd = Image.FromFile(file);

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
                    string strFileEndProcessed = PathHelper.AddDirectoryToFilePath(file, txtDestDirectory.Text, true);
                    bmpResult.Save(strFileEndProcessed);
                    ctrOutput.AddLine(string.Format("Offset applied to \"{0}\"", strFileEndProcessed));
                    Application.DoEvents();

                    previousFile = strFileEndProcessed;
                }
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
