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
        private const int BytesPerPixel = 3;

        private readonly Image _imgStart;
        private readonly int _widthStart;
        private readonly int _heightStart;
        private readonly byte[] _bytesStart;

        private readonly Image _imgEnd;
        private readonly int _widthEnd;
        private readonly int _heightEnd;
        private readonly byte[] _bytesEnd;

        public FindDeltaPosition(Image imgStart, Image imgEnd)
        {
            _imgStart = imgStart;
            _widthStart = _imgStart.Width;
            _heightStart = _imgStart.Height;
            _bytesStart = Image_GetPixelsBytesArray(_imgStart);

            _imgEnd = imgEnd;
            _widthEnd = _imgEnd.Width;
            _heightEnd = _imgEnd.Height;
            _bytesEnd = Image_GetPixelsBytesArray(_imgEnd);
        }

        private static byte[] Bitmap_GetPixelsBytesArray(Bitmap bmp)
        {
            Rectangle _imageRect = new Rectangle(0, 0, bmp.Width, bmp.Height);
            byte[] pixelData = new byte[bmp.Width * bmp.Height * BytesPerPixel];

            BitmapData imageData = bmp.LockBits(
                _imageRect,
                ImageLockMode.ReadOnly,
                PixelFormat.Format24bppRgb);
            Marshal.Copy(imageData.Scan0, pixelData, 0, pixelData.Length);
            bmp.UnlockBits(imageData);

            return pixelData;
        }

        private static byte[] Image_GetPixelsBytesArray(Image img)
        {
            Bitmap bmp = new Bitmap(img);
            return Bitmap_GetPixelsBytesArray(bmp);
        }

        public double CalculateMeanSquareError(int offsetX = 0, int offsetY = 0, int skipPixels = 1)
        {
            long sum = 0;
            int estimatedCount = ((_widthStart - offsetX) * (_heightStart - offsetY)) * 3;
            for (int y = 0; y < _heightStart; y++)
            {
                for (int x = 0; x < _widthStart; x++)
                {
                    if ((x % skipPixels != 0) || (y % skipPixels != 0)) { continue; }

                    int x2 = x - offsetX;
                    if (x2 < 0 || x2 >= _widthEnd) { continue; }
                    int y2 = y - offsetY;
                    if (y2 < 0 || y2 >= _heightEnd) { continue; }

                    long offset1 = (x + (y * _widthStart)) * BytesPerPixel;
                    long offset2 = (x2 + (y2 * _widthEnd)) * BytesPerPixel;

                    for (int c = 0; c < BytesPerPixel; c++)
                    {
                        int diff = _bytesStart[offset1 + c] - _bytesEnd[offset2 + c];
                        sum += (diff * diff);
                    }
                }
            }
            double meanSquareRoot = (sum / (double)estimatedCount);
            return meanSquareRoot;
        }

        public class OffsetPosition
        {
            public int OffsetX { get; set; }
            public int OffsetY { get; set; }
            public double MeanSquareError { get; set; }
        }

        public OffsetPosition SearchOnSpiral(float percentage = 1.0f, int skipPixels = 1)
        {
            int skip = 0;
            int halfWidth = _widthStart / 2;
            int halfHeight = _heightStart / 2;
            int max = (int)(Math.Max(_widthStart, _heightStart) * percentage);
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
                    if (skip > 8 && (i % (skip/8)) != 0)
                    {
                        process = false;
                    }
                }

                if (process)
                {
                    double meanSquareError = CalculateMeanSquareError(x, y, skipPixels);
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
                    skip++;
                }
                x = x + dx;
                y = y + dy;
            }
            return bestOffsetPosition;
        }


    }
}
