using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Windows.Forms;

namespace ImageProcesing.Code
{
    public class FindDeltaPosition
    {
        private readonly Image _imgStart;
        private readonly RawImage _rimgStart;

        private readonly Image _imgEnd;
        private readonly RawImage _rimgEnd;

        public FindDeltaPosition(Image imgStart, Image imgEnd)
        {
            _imgStart = imgStart;
            _rimgStart = RawImage.FromImage(imgStart);
            _imgEnd = imgEnd;
            _rimgEnd = RawImage.FromImage(imgEnd);
        }

        public double CalculateMeanSquareError(int offsetX = 0, int offsetY = 0, int skipPixels = 1)
        {
            long sum = 0;
            int cnt = 0;
            for (int y = 0; y < _rimgStart.Height; y += skipPixels)
            {
                for (int x = 0; x < _rimgStart.Width; x += skipPixels)
                {
                    int x2 = x - offsetX;
                    int y2 = y - offsetY;

                    long offset1 = _rimgStart.GetOffset(x, y);
                    long offset2 = _rimgEnd.GetOffsetLooping(x2, y2);

                    for (int c = 0; c < RawImage.BytesPerPixel; c++)
                    {
                        int diff = _rimgStart.Data[offset1 + c] - _rimgEnd.Data[offset2 + c];
                        sum += (diff * diff);
                        cnt++;
                    }
                }
            }
            double meanSquareRoot = (sum / (double)cnt);
            return meanSquareRoot;
        }

        public Bitmap GenerateErrorMap(int skipChecks, int skipPixels)
        {
            int halfWidth = _rimgStart.Width / 2;
            int halfHeight = _rimgStart.Height / 2;
            int mapWidth = (int)Math.Ceiling(_rimgStart.Width / (double)skipChecks) + 1;
            int mapHeight = (int)Math.Ceiling(_rimgStart.Height / (double)skipChecks) + 1;
            double[,] map = new double[mapWidth, mapHeight];

            // Calculate map
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    int offsetX = (x * skipChecks) - halfWidth;
                    int offsetY = (y * skipChecks) - halfHeight;
                    double meanSquareError = CalculateMeanSquareError(offsetX, offsetY, skipPixels);
                    map[x, y] = meanSquareError;
                }
            }

            // Normalize Map
            double maxMeanSquareError = -1;
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    double meanSquareError = map[x, y];
                    if (maxMeanSquareError < meanSquareError)
                    {
                        maxMeanSquareError = meanSquareError;
                    }
                }
            }
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    double meanSquareError = map[x, y];
                    meanSquareError = 1.0f - (meanSquareError / maxMeanSquareError);
                    map[x, y] = meanSquareError;
                }
            }
            
            // Draw map
            int medX = halfWidth / skipChecks;
            int medY = halfHeight / skipChecks;
            Bitmap bmpError = new Bitmap(mapWidth, mapHeight, PixelFormat.Format24bppRgb);
            for (int y = 0; y < mapHeight; y++)
            {
                for (int x = 0; x < mapWidth; x++)
                {
                    double meanSquareError = map[x, y];

                    byte bMeanSquareError = (byte)(255 * meanSquareError);
                    if (x == medX && y == medY)
                    {
                        Color color = Color.FromArgb(255, bMeanSquareError, bMeanSquareError);
                        bmpError.SetPixel(x, y, color);
                    }
                    else
                    {
                        Color color = Color.FromArgb(bMeanSquareError, bMeanSquareError, bMeanSquareError);
                        bmpError.SetPixel(x, y, color);
                    }
                }
            }
            
            return bmpError;
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
            int halfWidth = _rimgStart.Width / 2;
            int halfHeight = _rimgStart.Width / 2;
            int max = (int)(Math.Max(_rimgStart.Width, _rimgStart.Height) * percentage);
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
