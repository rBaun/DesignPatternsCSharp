using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns._2._Builder_pattern
{
    public class HtmlElement
    {
        public string Name, Text;
        public List<HtmlElement> Elements = new List<HtmlElement>();
        private const int indentSize = 2;

        public HtmlElement() {}

        public HtmlElement(string name, string text)
        {
            this.Name = name ?? throw new ArgumentNullException(paramName: nameof(name));
            this.Text = text ?? throw new ArgumentNullException(paramName: nameof(text));
        }

        private string ToStringImpl(int indent)
        {
            var sb = new StringBuilder();
            var i = new string(' ', indentSize * indent);
            sb.AppendLine($"{i}<{Name}>");

            if(!string.IsNullOrWhiteSpace(Text))
            {
                sb.Append(new string(' ', indentSize * (indent + 1)));
                sb.AppendLine(Text);
            }

            foreach (var e in Elements)
                sb.Append(e.ToStringImpl(indent + 1));
            sb.AppendLine($"{i}</{Name}>");

            return sb.ToString();
        }

        public override string ToString()
        {
            return ToStringImpl(0);
        }
    }

    /**
     * Not a scalable Builder, but it is intended to show the potential
     * with a builder.
     */ 
    public class HtmlBuilder
    {
        private readonly string rootName;
        HtmlElement root = new HtmlElement();

        public HtmlBuilder(string rootName)
        {
            root.Name = rootName;
        }

        public HtmlBuilder AddChild(string childName, string childText)
        {
            var e = new HtmlElement(childName, childText);
            root.Elements.Add(e);
            return this;
        }

        public override string ToString()
        {
            return root.ToString();
        }

        public void Clear()
        {
            root = new HtmlElement { Name = rootName };
        }
    }

    public class BuilderWithHtml
    {
        static void Main(string[] args)
        {
            /**
             * The two example below is without any custom builder
             * It only uses StringBuilder to append strings.
             */ 

            //single parameter
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");
            Console.WriteLine(sb);

            //multi parameter
            var words = new[] { "hello", "world" };
            sb.Clear();
            sb.Append("<ul>");
            foreach (var word in words)
                sb.AppendFormat("<li>{0}</li>", word);
            sb.Append("</ul>");
            Console.WriteLine(sb);  // needs to iterate through words, where you can use an HTML builder instead.

            /**
             * Using the actual HTML Builder
             */
            var builder = new HtmlBuilder("ul");
            builder.AddChild("li", "hello").AddChild("li", "world");
            Console.WriteLine(builder);

            Console.ReadLine();
        }
    }
}
