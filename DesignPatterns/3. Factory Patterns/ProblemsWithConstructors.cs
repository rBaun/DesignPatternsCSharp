using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns._3._Factory_Patterns
{
    public enum CoordinateSystem
    {
        Cartesian,
        Polar
    }

    public class Point
    {
        private double x, y;

        // You are not able to have both these constructors:

        // Cartesian system
        //public Point(double x, double y)
        //{
        //    this.x = x;
        //    this.y = y;
        //}

        // Polar system
        //public Point(double rho, double theta)
        //{
        //    // not able to have both constructors..
        //}

        // A workaround could be as follows
        // However, this would require documentation to let people know
        // How you intend to use the a and b parameters and also how you initialize the object..
        public Point(double a, double b,
            CoordinateSystem system = CoordinateSystem.Cartesian)
        {
            switch(system)
            {
                case CoordinateSystem.Cartesian:
                    x = a;
                    y = b;
                    break;
                case CoordinateSystem.Polar:
                    x = a * Math.Cos(b);
                    y = a * Math.Sin(b);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(system), system, null);
            }
        }
    }

    public class ProblemsWithConstructors
    {
        //static void Main(string[] args)
        //{

        //}
    }
}
