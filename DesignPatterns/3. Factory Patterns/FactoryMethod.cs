using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns._3._Factory_Patterns
{
    public enum CoordSystem
    {
        Cartesian,
        Polar
    }

    public class CoordPoint
    {
        // factory method for cartesian point
        public static CoordPoint NewCartesianPoint(double x, double y)
        {
            return new CoordPoint(x, y);
        }

        // factory method for polar point
        public static CoordPoint NewPolarPoint(double rho, double theta)
        {
            return new CoordPoint(rho * Math.Cos(theta), rho * Math.Sin(theta));
        }

        private double x, y;

        // private constructor
        private CoordPoint(double x, double y)
        {
            this.x = x;
            this.y = y;
        }

        public override string ToString()
        {
            return $"{nameof(x)}: {x}, {nameof(y)}: {y}";
        }
    }

    public class FactoryMethod
    {
        static void Main(string[] args)
        {
            var point = CoordPoint.NewPolarPoint(1.0, Math.PI / 2);
            Console.WriteLine(point);
            var point2 = CoordPoint.NewCartesianPoint(3, 5);
            Console.WriteLine(point2);

            Console.ReadLine();
        }
    }
}
