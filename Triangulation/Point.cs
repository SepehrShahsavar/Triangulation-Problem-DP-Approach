using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Triangulation
{
    public class Point
    {
        public int X { get; set; }
        public int Y { get; set; }

        public Point(int x , int y)
        {
            X = x;
            Y = y;
        }

        public static double CalculateDistance(Point p1 , Point p2)
        {
            return Math.Sqrt(Math.Pow((p1.X - p2.X),2) + Math.Pow((p1.Y - p2.Y), 2));
        }
    }
}
