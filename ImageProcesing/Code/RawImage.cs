using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;

namespace ImageProcesing.Code
{
    public class RawImage
    {
        public const int BytesPerPixel = 3;

        private readonly int _width;
        private readonly int _height;
        private readonly byte[] _data;

        public int Width => _width;

        public int Height => _height;

        public byte[] Data => _data;

        public RawImage(int width, int height)
        {
            _width = width;
            _height = height;
            _data = new byte[width * height * BytesPerPixel];
        }

        public static RawImage FromBitmap(Bitmap bmp)
        {
            Rectangle _imageRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            RawImage newImage = new RawImage(bmp.Width, bmp.Height);

            BitmapData imageData = bmp.LockBits(
                _imageRect,
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);
            Marshal.Copy(imageData.Scan0, newImage._data, 0, newImage._data.Length);
            bmp.UnlockBits(imageData);

            return newImage;
        }

        public static RawImage FromImage(Image image)
        {
            Bitmap bmp = new Bitmap(image);
            return FromBitmap(bmp);
        }

        public bool IsInsideBounds(int x, int y)
        {
            if (x < 0 || x >= _width) { return false; }
            if (y < 0 || y >= _height) { return false; }
            return true;
        }

        public int GetOffset(int x, int y)
        {
            return (x + (y * _width)) * BytesPerPixel;
        }

        public int GetOffsetLooping(int x, int y)
        {
            x = (x % _width);
            if (x < 0) { x = x + _width; }
            y = (y % _height);
            if (y < 0) { y = y + _height; }
            return (x + (y * _width)) * BytesPerPixel;
        }
    }
}
