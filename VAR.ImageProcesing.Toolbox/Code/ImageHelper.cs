using System.Drawing;
using System.Drawing.Imaging;

namespace VAR.ImageProcesing.Toolbox.Code
{
    public class ImageHelper
    {
        public static Bitmap OffsetImage(int offsetX, int offsetY, Image img)
        {
            Bitmap bmpResult = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(bmpResult);
            g.DrawImage(img, 0, 0, img.Width, img.Height);
            g.DrawImage(img, offsetX, offsetY, img.Width, img.Height);
            return bmpResult;
        }
    }
}
