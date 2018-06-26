using System;
using System.Drawing;

namespace Draw
{
    internal class Shape
    {
        
        internal static char[,] AddToCanvas(char[,] canvas, Rectangle rect, char xCordChar, char yCordChar,
            bool initialize = false)
        {
            for (var i = rect.X; i < canvas.GetLength(1); i++)
            for (var j = rect.Y; j < canvas.GetLength(0); j++)
                if ((j == rect.Y || j == rect.Bottom) && i <= rect.Right && canvas[j, i] == '\0'
                ) 
                    canvas[j, i] = xCordChar;
                else if ((i == rect.X || i == rect.Right) && j <= rect.Bottom && canvas[j, i] == '\0'
                )
                    canvas[j, i] = yCordChar;
           

            return canvas;
        }

        internal static char[,] AddToCanvas(char[,] canvas, Point fromPoint, Point toPoint, char cordChar)
        {
            if (fromPoint.X == toPoint.X)
            {
                var tempY1 = fromPoint.Y;
                var tempY2 = toPoint.Y;

                if (toPoint.Y < fromPoint.Y)
                {
                    tempY1 = toPoint.Y;
                    tempY2 = fromPoint.Y;
                }

                while (tempY1 <= tempY2)
                {
                    canvas[tempY1, fromPoint.X] = cordChar;
                    tempY1++;
                }
            }
            else if (fromPoint.Y == toPoint.Y)
            {
                var tempX1 = fromPoint.X;
                var tempX2 = toPoint.X;

                if (toPoint.X < fromPoint.X)
                {
                    tempX1 = toPoint.X;
                    tempX2 = fromPoint.X;
                }

               
                while (tempX1 <= tempX2)
                {
                    canvas[fromPoint.Y, tempX1] = cordChar;
                    tempX1++;
                }
            }

            return canvas;
        }

        internal static char[,] AddToCanvas(char[,] canvas, Point pt, char color)
        {
            if (canvas[pt.Y, pt.X] == '\0')
                canvas[pt.Y, pt.X] = color;

            if (canvas[pt.Y + 1, pt.X] == '\0')
                AddToCanvas(canvas, new Point(pt.X, pt.Y + 1), color);

            if (canvas[pt.Y - 1, pt.X] == '\0')
                AddToCanvas(canvas, new Point(pt.X, pt.Y - 1), color);

            if (canvas[pt.Y, pt.X + 1] == '\0')
                AddToCanvas(canvas, new Point(pt.X + 1, pt.Y), color);

            if (canvas[pt.Y, pt.X - 1] == '\0')
                AddToCanvas(canvas, new Point(pt.X - 1, pt.Y), color);

            return canvas;
        }


        internal static void DrawCanvas(char[,] canvas)
        {
            for (var i = 0; i < canvas.GetLength(0); i++)
            {
                for (var x = 0; x < canvas.GetLength(1); x++)
                {
                    Console.ForegroundColor = Utility.GetColor((char) canvas.GetValue(i, x));
                    Console.Write(canvas.GetValue(i, x));
                    Console.ResetColor();
                }

                Console.WriteLine();
            }
        }

        internal struct Points
        {
            public Point fromPoint;
            public Point toPoint;
        }
    }
}