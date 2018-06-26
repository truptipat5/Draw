using System;
using System.Collections.Generic;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Collections;

namespace Draw
{
    internal static class Utility
    {
        internal static string RemoveExtraSpaces(this string input)
        {
            var pattern = @"\s+";

            var exp = new Regex(pattern);

            return exp.Replace(input, " ");
        }

  internal static bool ValidateCanvas(IList<string> splitCommand, int countCommand, Size canvasSize,
      ref Shape.Points pts, bool isLine = false)
        {
            var ret = true;

            if (splitCommand.Count != countCommand)
            {
                Console.WriteLine("Please enter correct cordincates");
                ret = false;
            }

            if (ValidateCanvas(canvasSize)) ret = false;

            pts.fromPoint = ParseCord(splitCommand[1], splitCommand[2]);
            pts.toPoint = ParseCord(splitCommand[3], splitCommand[4]);

            if ((pts.fromPoint.X >= pts.toPoint.X || pts.fromPoint.Y >= pts.toPoint.Y) && !isLine)
            {
                Console.WriteLine("Please enter valid cordinates");
                ret = false;
            }

            if (!ValidCordinate(pts.fromPoint, canvasSize) || !ValidCordinate(pts.toPoint, canvasSize))
                ret = false;

            return ret;
        }

        internal static bool ValidateCanvas(Size canvasSize)
        {
            if (canvasSize.IsEmpty)
               Console.WriteLine("Please first draw a valid canvas --> width: {canvasSize.Width} & height: {canvasSize.Height}");
            return canvasSize.IsEmpty;
        }

        internal static bool ValidCordinate(Point pt, Size canvasSize)
        {
            var val = true;

            if (pt.X >= canvasSize.Width + 1 || pt.X <= 0)
            {
                Console.WriteLine("Please Enter valid X co-ordinates");
                val = false;
            }

            if (pt.Y >= canvasSize.Height + 1 || pt.Y <= 0)
            {
                Console.WriteLine("Please Enter valid Y co-ordinates");
                val = false;
            }

            return val;
        }

        internal static bool ValidCordinate(Rectangle rect, Point pt)
        {
            return rect.Contains(pt);
        }

        internal static Point ParseCord(string x, string y)
        {
            var tempX = 0;
            var tempY = 0;
            int.TryParse(x, out tempX);
            int.TryParse(y, out tempY);

            return new Point(tempX, tempY);
        }

        internal static ConsoleColor GetColor(char color)
        {
            switch (color)
            {
                case 'b': return ConsoleColor.Blue;
                case 'g': return ConsoleColor.Green;
                case 'c': return ConsoleColor.Cyan;
                case 'r': return ConsoleColor.Red;
                case 'm': return ConsoleColor.Magenta;
                case 'y': return ConsoleColor.Yellow;

                case 'a': return ConsoleColor.Gray;
                case 'd': return ConsoleColor.DarkBlue;
                case 'e': return ConsoleColor.DarkGreen;
                case 'f': return ConsoleColor.DarkCyan;
                case 'h': return ConsoleColor.DarkRed;
                case 'i': return ConsoleColor.DarkMagenta;
                case 'j': return ConsoleColor.DarkYellow;
                case 'k': return ConsoleColor.DarkGray;
                default:
                    return ConsoleColor.White;
            }
        }
    }
}