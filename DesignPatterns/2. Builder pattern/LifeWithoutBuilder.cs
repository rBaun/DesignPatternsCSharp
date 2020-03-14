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

        public HtmlElement()
        {

        }


    }

    public class LifeWithoutBuilder
    {
        static void Main(string[] args)
        {
            // simple example
            var hello = "hello";
            var sb = new StringBuilder();
            sb.Append("<p>");
            sb.Append(hello);
            sb.Append("</p>");
            Console.WriteLine(sb);

            // example with 2 words
            var words = new[] { "hello", "world" };
            sb.Clear();
            sb.Append("<ul>");
            foreach (var word in words)
                sb.AppendFormat("<li>{0}</li>", word);
            sb.Append("</ul>");
            Console.WriteLine(sb);  // needs to iterate through words, where you can use an HTML builder instead.

            Console.ReadLine();
        }
    }
}
