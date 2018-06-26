using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Threading;

namespace Draw
{
   

    internal class Program
    {
        private static Size _canvasSize = new Size(0, 0);

        private static char[,] canvasPage;

        private static void Main(string[] args)
        {
            Console.WriteLine("****************WELCOME TO CONSOLE DRAWING PLATFORM****************");
            WriteHelp();

            Console.WriteLine("User input is prefixed with enter command.");

            Console.Write("enter command: ");
            var command = Console.ReadLine();
            var exit = false;
            while (true)
            {
                
                var splitCommand = command.Trim().RemoveExtraSpaces().Split(' ').ToList();

                switch (splitCommand[0].ToUpper())
                {
                    case "C":
                        MapCanvas(splitCommand);
                        DrawCanvas();
                        break;
                    case "L":
                        DrawLine(splitCommand, 'x');
                        break;
                    case "R":
                        DrawRectangle(splitCommand, 'x', 'x');
                        break;

                    case "B":
                        DrawFill(splitCommand, splitCommand[3].ElementAt(0));
                        break;

                    case "Q":
                        Console.WriteLine("Exiting program....");
                        Thread.Sleep(3000);
                        exit = true;
                        break;

                    case "HELP":
                        WriteHelp();
                        break;
                    default:
                        Console.WriteLine(
                            "Please enter valid option or use command Q to exit. For more information type help");
                        break;
                }

                if (exit) break;

                Console.Write("enter command: ");
                command = Console.ReadLine();
            }

            Environment.Exit(0);
        }

       
       
        private static void DrawFill(IList<string> splitCommand, char drawChar)
        {
            var pt = Utility.ParseCord(splitCommand[1], splitCommand[2]);
            if (!Utility.ValidCordinate(pt, _canvasSize))
                return;

            Shape.AddToCanvas(canvasPage, pt, drawChar);
            Shape.DrawCanvas(canvasPage);
        }

        private static void WriteHelp()
        {
            Console.WriteLine(
                @"The program work as follow:
1. Create a new canvas
2. Start drawing on the canvas by issuing various commands
3. Quit

Commands                            Description
C w h                   Should create a new canvas of width w and height h.

L x1 y1 x2 y2           Should create a new line from (x1,y1) to (x2,y2). Currently
                        only horizontal or vertical lines are supported. Horizontal
                        and vertical lines will be drawn using the 'x' character.

R x1 y1 x2 y2           Create a new rectangle, whose upper left corner is (x1,y1)
                        and lower right corner is (x2,y2). Horizontal and vertical 
                        lines will be drawn using the 'x' character.

B x y c                 Fill the entire area connected to (x,y) with colour 'c'. The
                        behaviour of this is the same as that of the 'bucket fill' tool
                        in paint programs.
                        
                        Command to select color.
                        DarkBlue = d,
                        DarkGreen = e,
                        DarkCyan = f,
                        DarkRed = h,
                        DarkMagenta = i,
                        DarkYellow = j,
                        Gray = a,
                        DarkGray = k,
                        Blue = b,
                        Green = g,
                        Cyan = c,
                        Red = r,
                        Magenta = m,
                        Yellow = y,
                        White = defualt

Q                       Quit the program.
");
        }

        private static void DrawRectangle(IList<string> splitCommand, char drawCharX, char drawCharY)
        {
            var pts = new Shape.Points();

            if (!Utility.ValidateCanvas(splitCommand, 5, _canvasSize, ref pts))
                return;

            var rectangleSize = new Size
            {
                Height = pts.toPoint.Y - pts.fromPoint.Y,
                Width = pts.toPoint.X - pts.fromPoint.X
            };

            var rect = new Rectangle(pts.fromPoint, rectangleSize);
            Shape.AddToCanvas(canvasPage, rect, drawCharX, drawCharY);
            Shape.DrawCanvas(canvasPage);
        }

       
         private static void DrawLine(IList<string> splitCommand, char drawChar)
        {
            var pts = new Shape.Points();

            if (!Utility.ValidateCanvas(splitCommand, 5, _canvasSize, ref pts, true))
                return;

            Shape.AddToCanvas(canvasPage, pts.fromPoint, pts.toPoint, drawChar);
            Shape.DrawCanvas(canvasPage);
        }

        private static void DrawCanvas()
        {
            if (_canvasSize.IsEmpty) return;
            var rect = new Rectangle(new Point(0, 0), _canvasSize);
            Shape.AddToCanvas(canvasPage, rect, '-', '|', true);
            Shape.DrawCanvas(canvasPage);
        }

       
         private static void MapCanvas(IList<string>  splitCommand)
        {
            var redrawChar = ' ';
            if (!_canvasSize.IsEmpty)
            {
                
                Console.WriteLine("Canvas is not empty --> width: {_canvasSize.Width} & height: {_canvasSize.Height}");
                Console.WriteLine("Do you wish to redraw your canvas? Answer in Y/N");
                Console.Write("Answer:");
                redrawChar = Console.ReadLine().FirstOrDefault();
            }

            if (redrawChar != ' ' && redrawChar != 'Y' && redrawChar != 'y') return;

            

            var pt = Utility.ParseCord(splitCommand[1], splitCommand[2]);

            _canvasSize.Width = pt.X + 1; 
            _canvasSize.Height = pt.Y + 1; 

            canvasPage = new char[_canvasSize.Height + 1, _canvasSize.Width + 1];
        }
    }
}