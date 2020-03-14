using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns
{
    /** 
     * The Open Closed principle states that a class should be open for extension.
     * In this case it should be possible to extend ProductFilter to make new filters,
     * but at the same time it should be closed for modification.
     * 
     * The idea is that once a class is running and working it is usually a bad idea
     * to come back and modify the class that you already know is working.
     * 
     * This means it should not be possible to go into the ProductFilter class and 
     * modify the code. Instead you should use inheritance to avoid the brute force approach.
     * 
     * This can be done by using the SPECIFICATION PATTERN. The idea is to combine business logic
     * together, so that it gives a boolean expression. 
     * 
     * A more in depth guide can be found at https://www.codeproject.com/Articles/670115/Specification-pattern-in-Csharp
     */
    public enum Color
    {
        Red, Green, Blue, Yellow
    }

    public enum Size
    {
        Small, Medium, Large, Huge
    }

    public class Product
    {
        public string Name;
        public Color Color;
        public Size Size;

        public Product(string name, Color color, Size size)
        {
            if(name == null)
            {
                throw new ArgumentNullException(paramName: nameof(name));
            }
            Name = name;
            Color = color;
            Size = size;
        }
    }

    /**
     * This class is able to filter by size and color.
     * Imagine your boss now wants to be able to filter by both at the same time.
     * 
     */ 
    public class ProductFilter
    {
        public IEnumerable<Product> FilterBySize(IEnumerable<Product> products, Size size)
        {
            foreach(var p in products)
                if (p.Size == size)
                    yield return p;
        }

        public IEnumerable<Product> FilterByColor(IEnumerable<Product> products, Color color)
        {
            foreach (var p in products)
                if (p.Color == color)
                    yield return p;
        }

        // This violates the Open Closed principle.
        public IEnumerable<Product> FilterBySizeAndColor(IEnumerable<Product> products, Size size, Color color)
        {
            foreach (var p in products)
                if (p.Size == size && p.Color == color)
                    yield return p;
        }
    }

    /**
     * The ISpecification checks whether or not some criteria is met.
     * By doing this, we follow the OpenClose Principle with the Specification Pattern
     */
    public interface ISpecification<T>
    {
        bool IsSatisfied(T t);
    }

    /**
     * The IFilter is specifically an interface that makes it possible to filter.
     * The interface also implements the ISpecification to check whether or not it
     * is satisfied with the filter criteria.
     */ 
    public interface IFilter<T>
    {
        IEnumerable<T> Filter(IEnumerable<T> items, ISpecification<T> spec);
    }

    /**
     * For each criteria you want to filter by, you can implement the specification
     * interface to check for the criteria. This is an example with ColorSpecification.
     */ 
    public class ColorSpecification : ISpecification<Product>
    {
        private Color color;

        public ColorSpecification(Color color)
        {
            this.color = color;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Color == color;
        }
    }

    /**
     * The same pattern is seen below with the size specification.
     */ 
    public class SizeSpecification : ISpecification<Product>
    {
        private Size size;

        public SizeSpecification(Size size)
        {
            this.size = size;
        }

        public bool IsSatisfied(Product t)
        {
            return t.Size == size;
        }
    }

    /**
     * Now imagine you were assigned the task to add a new specification.
     * In the new specification it is possible to combine size and color
     * so that you can filter by both criterias.
     * 
     * In that case there has been implemented an AndSpecification which takes
     * 2 specifications and checks whether or not BOTH of them is satisfied.
     */ 
    public class AndSpecification<T> : ISpecification<T>
    {
        private ISpecification<T> first, second;

        public AndSpecification(ISpecification<T> first, ISpecification<T> second) 
        {
            this.first = first ?? throw new ArgumentNullException(paramName: nameof(first));
            this.second = second ?? throw new ArgumentNullException(paramName: nameof(second));
        }


        public bool IsSatisfied(T t)
        {
            return first.IsSatisfied(t) && second.IsSatisfied(t);
        }
    }

    public class BetterFilter : IFilter<Product>
    {
        public IEnumerable<Product> Filter(IEnumerable<Product> items, ISpecification<Product> spec)
        {
            foreach (var i in items)
                if (spec.IsSatisfied(i))
                    yield return i;
        }
    }

    public class OpenClosed
    {
        //public static void Main(string[] args)
        //{
        //    var apple = new Product("Apple", Color.Red, Size.Medium);
        //    var grass = new Product("Grass", Color.Green, Size.Large);
        //    var melon = new Product("Melon", Color.Green, Size.Huge);
        //    var house = new Product("House", Color.Blue, Size.Large);

        //    Product[] products = { apple, grass, melon, house };

        //    // Bad way of doing it
        //    var pf = new ProductFilter();
        //    Console.WriteLine("Green products (old way): ");
        //    foreach (var p in pf.FilterByColor(products, Color.Green))
        //    {
        //        Console.WriteLine($" - {p.Name} is green");
        //    }

        //    // Good way of doing it
        //    var bf = new BetterFilter();
        //    Console.WriteLine("Green products (new way): ");
        //    foreach (var p in bf.Filter(products, new ColorSpecification(Color.Green)))
        //    {
        //        Console.WriteLine($" - {p.Name} is green");
        //    }

        //    // Good way of combining both (without modifying class)
        //    Console.WriteLine("Large blue items");
        //    foreach(var p in bf.Filter(
        //        products,
        //        new AndSpecification<Product>(
        //            new ColorSpecification(Color.Blue),
        //            new SizeSpecification(Size.Large)
        //            )))
        //    {
        //        Console.WriteLine($" - {p.Name} is large and blue");
        //    }

        //    Console.ReadLine();
        //}
    }
}
