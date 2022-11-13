using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PolygonArea
{

    public class Point
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Point(double x, double y)
        {
            X = x;
            Y = y;
        }

        public override string ToString() => $"({X}, {Y})";
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var points = new List<Point>();
            points.Add(new Point(10, 0));
            points.Add(new Point(20, 10));

            points.Add(new Point(10, 20));
            points.Add(new Point(0, 10));

            Console.WriteLine("Area = " + PolygonArea(points));
            Console.WriteLine("Centroid = " + PolygonCentroid(points));

            Console.ReadLine();
        }

        /// <summary>
        /// The positions of the geometric centroid of a planar non-self-intersecting polygon with vertices (x1,y1), ..., (xn,yn)
        /// </summary>
        /// <param name="points"></param>
        /// <returns></returns>
        public static Point PolygonCentroid(List<Point> points)
        {
            double area = PolygonArea(points);
            double x = 0;
            double y = 0;
            double x1, x2, y1, y2;
            for (int i = 1; i < points.Count; i++)
            {
                x1 = points[i - 1].X;
                x2 = points[i].X;
                y1 = points[i - 1].Y;
                y2 = points[i].Y;
                x += (x1 + x2) * Determinant(x1, y1, x2, y2);
                y += (y1 + y2) * Determinant(x1, y1, x2, y2);
            }
            x /= 6 * area;
            y /= 6 * area;
            return new Point(x, y);
        }

        static double Determinant(double x1, double y1, double x2, double y2)
        {
            return x1 * y2 - x2 * y1;
        }

        /// <summary>
        /// The (signed) area of a planar non-self-intersecting polygon with vertices (x1,y1), ..., (xn,yn)
        /// </summary>
        /// <param name="vertices"></param>
        /// <returns>Note that the area of a convex polygon is defined to be positive if the points are arranged in a counterclockwise order and negative if they are in clockwise order (Beyer 1987).</returns>
        static double PolygonArea(List<Point> vertices)
        {
            if (vertices.Count < 3)
            {
                return 0;
            }
            double area = Determinant(vertices[vertices.Count - 1].X, vertices[vertices.Count - 1].Y, vertices[0].X, vertices[0].Y);
            for (int i = 1; i < vertices.Count; i++)
            {
                area += Determinant(vertices[i - 1].X, vertices[i - 1].Y, vertices[i].X, vertices[i].Y);
            }
            return area / 2;
        }
    }
}
