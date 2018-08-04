using System.Drawing;
using System.Drawing.Imaging;

namespace VAR.ImageProcessing.Toolbox.Code
{
    public class ImageHelper
    {
        public static Bitmap OffsetImage(int offsetX, int offsetY, Image img)
        {
            offsetX = offsetX % img.Width;
            if (offsetX < 0)
            {
                offsetX += img.Width;
            }
            offsetY = offsetY % img.Height;
            if (offsetY < 0)
            {
                offsetY += img.Height;
            }
            Bitmap bmpResult = new Bitmap(img.Width, img.Height, PixelFormat.Format24bppRgb);
            Graphics g = Graphics.FromImage(bmpResult);
            g.DrawImage(img, offsetX, offsetY, img.Width, img.Height);
            if (offsetX > 0)
            {
                g.DrawImage(img, offsetX - img.Width, offsetY, img.Width, img.Height);
            }
            if (offsetY > 0)
            {
                g.DrawImage(img, offsetX, offsetY - img.Height, img.Width, img.Height);
            }
            if (offsetX > 0 && offsetY > 0)
            {
                g.DrawImage(img, offsetX - img.Width, offsetY - img.Height, img.Width, img.Height);
            }
            return bmpResult;
        }
    }
}
