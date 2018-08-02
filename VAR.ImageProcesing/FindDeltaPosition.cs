using System;
using System.Drawing;
using System.Drawing.Imaging;

namespace VAR.ImageProcesing.Code
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

        public OffsetPosition SearchOnGrid(int xMin, int yMin, int xMax, int yMax, int skipChecks, int skipPixels)
        {
            int halfWidth = _rimgStart.Width / 2;
            int halfHeight = _rimgStart.Height / 2;
            OffsetPosition bestOffsetPosition = null;
            for(int y = yMin; y < yMax; y += skipChecks)
            {
                for (int x = xMin; x < xMax; x += skipChecks)
                {
                    int offsetX = x - halfWidth;
                    int offsetY = y - halfHeight;
                    double meanSquareError = CalculateMeanSquareError(offsetX, offsetY, skipPixels);
                    if (bestOffsetPosition == null || meanSquareError < bestOffsetPosition.MeanSquareError)
                    {
                        bestOffsetPosition = new OffsetPosition { OffsetX = offsetX, OffsetY = offsetY, MeanSquareError = meanSquareError, };
                    }
                }
            }
            return bestOffsetPosition;
        }

        public OffsetPosition SearchRecursive(int checkDensity, int skipPixels)
        {
            int halfWidth = _rimgStart.Width / 2;
            int halfHeight = _rimgStart.Height / 2;
            OffsetPosition bestOffsetPosition = null;
            int xMin = 0;
            int yMin = 0;
            int xMax = _rimgStart.Width;
            int yMax = _rimgStart.Height;
            int skipChecks = 1;
            do
            {
                skipChecks = Math.Min((xMax - xMin), (yMax - yMin)) / checkDensity;
                if (skipChecks <= 0) { skipChecks = 1; }
                bestOffsetPosition = SearchOnGrid(xMin, yMin, xMax, yMax, skipChecks, skipPixels);
                if (skipChecks > 1)
                {
                    xMin = (bestOffsetPosition.OffsetX + halfWidth) - skipChecks;
                    yMin = (bestOffsetPosition.OffsetY + halfHeight) - skipChecks;
                    xMax = (bestOffsetPosition.OffsetX + halfWidth) + skipChecks;
                    yMax = (bestOffsetPosition.OffsetY + halfHeight) + skipChecks;
                }
            } while (skipChecks > 1);
            return bestOffsetPosition;
        }
        
    }
}
