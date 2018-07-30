using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace ImageProcesing.Code
{
    public class FindDeltaPosition
    {
        private readonly Image _imgStart;
        private readonly Image _imgEnd;
        private readonly int _width;
        private readonly int _height;
        private readonly byte[] _bytesStart;
        private readonly byte[] _bytesEnd;

        public FindDeltaPosition(Image imgStart, Image imgEnd)
        {
            _imgStart = imgStart;
            _imgEnd = imgEnd;
            _width = _imgStart.Width;
            _height = _imgStart.Height;
            _bytesStart = Image_GetPixelsBytesArray(_imgStart);
            _bytesEnd = Image_GetPixelsBytesArray(_imgEnd);
        }

        private static byte[] Bitmap_GetPixelsBytesArray(Bitmap bmp)
        {
            Rectangle _imageRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            int channels = Bitmap.GetPixelFormatSize(bmp.PixelFormat) / 8;
            byte[] pixelData = new byte[bmp.Width * bmp.Height * channels];

            BitmapData imageData = bmp.LockBits(
                _imageRect,
                ImageLockMode.ReadOnly,
                bmp.PixelFormat);
            Marshal.Copy(imageData.Scan0, pixelData, 0, pixelData.Length);
            bmp.UnlockBits(imageData);

            return pixelData;
        }

        private static byte[] Image_GetPixelsBytesArray(Image img)
        {
            Bitmap bmp = new Bitmap(img);
            return Bitmap_GetPixelsBytesArray(bmp);
        }

        public double CalculateMeanSquareError(int offsetX = 0, int offsetY = 0)
        {
            long meanSquareRoot = 0;
            int estimatedCount = ((_width - offsetX) * (_height - offsetY)) * 3;
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    int x2 = x + offsetX;
                    if (x2 < 0 || x2 >= _width) { continue; }
                    int y2 = y + offsetY;
                    if (y2 < 0 || y2 >= _height) { continue; }

                    long offset1 = (x + (y * _width)) * 4;
                    long offset2 = ((x + offsetX) + ((y + offsetY) * _width)) * 4;

                    for (int c = 0; c < 3; c++)
                    {
                        int diff = _bytesStart[offset1 + c] - _bytesEnd[offset2 + c];
                        meanSquareRoot += (diff * diff);
                    }
                }
            }
            return (meanSquareRoot / (double)estimatedCount);
        }

        public class OffsetPosition
        {
            public int OffsetX { get; set; }
            public int OffsetY { get; set; }
            public double MeanSquareError { get; set; }
        }

        public OffsetPosition SearchOnSpiral(float percentage = 1.0f, int skip = 0, int offset = 0)
        {
            int halfWidth = _width / 2;
            int halfHeight = _height / 2;
            int max = (int)(Math.Max(_width, _height) * percentage);
            max *= max;
            int x = 0;
            int y = 0;
            int dx = 0;
            int dy = -1;
            OffsetPosition bestOffsetPosition = null;
            List<OffsetPosition> listBestOffsetPositions = new List<OffsetPosition>();

            for (int i = 0; i < max; i++)
            {
                Application.DoEvents();
                bool process = false;
                if ((-halfWidth < x) && (x <= halfWidth) && (-halfHeight < y) && (y <= halfHeight))
                {
                    process = true;
                    if (skip > 0 && ((i + offset) % skip) != 0)
                    {
                        process = false;
                    }
                }

                if (process)
                {
                    double meanSquareError = CalculateMeanSquareError(x, y);
                    if (bestOffsetPosition == null || meanSquareError < bestOffsetPosition.MeanSquareError)
                    {
                        bestOffsetPosition = new OffsetPosition { OffsetX = x, OffsetY = y, MeanSquareError = meanSquareError, };
                        listBestOffsetPositions.Add(bestOffsetPosition);
                    }
                }

                if (x == y || (x < 0 && x == -y) || (x > 0 && x == (1 - y)))
                {
                    int temp;
                    temp = dx;
                    dx = -dy;
                    dy = temp;
                }
                x = x + dx;
                y = y + dy;
            }
            return bestOffsetPosition;
        }
    }
}
