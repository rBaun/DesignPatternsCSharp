using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /**
     * The idea is that you should be able to substitute a base type for a 
     * subtype, meaning you should be able to cast a descendent to its base
     * and still be able to operate as expected.
     * 
     * Basically use virtual and override to avoid problems with overrides.
     * In this example a square being overwrited to a rectangle would make 
     * it have 0 height and therefor 0 area, instead of 4 height, 4 width and 16 area.
     * 
     * Change rectangle to not be virtual and square to be new instead of override
     * to see the difference.
     */ 

    public class Rectangle
    {
        // make these not virtual to see difference
        public virtual int Width { get; set; }
        public virtual int Height { get; set; }

        public Rectangle()
        {

        }

        public Rectangle(int width, int height)
        {
            Width = width;
            Height = height;
        }

        public override string ToString()
        {
            return $"{nameof(Width)}: {Width}, {nameof(Height)}: {Height}";
        }
    }

    public class Square : Rectangle
    {
        // change from override => new to see the difference
        public override int Width
        {
            set { base.Width = base.Height = value; }
        }

        public override int Height
        {
            set { base.Width = base.Height = value; }
        }
    }

    public class LiskovSubstitution
    {
        //static public int Area(Rectangle r) => r.Width * r.Height;

        //static void Main(string[] args)
        //{
        //    Rectangle rc = new Rectangle(2, 3);
        //    Console.WriteLine($"{rc} has area {Area(rc)}");

        //    Rectangle sq = new Square();
        //    sq.Width = 4;
        //    Console.WriteLine($"{sq} has area {Area(sq)}");

        //    Console.ReadLine();
        //}
    }
}
