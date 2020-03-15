using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns._2._Builder_pattern
{
    /**
     * Functional builders is usually intended to be used within 
     * functional programming. This is just to illustrate how it 
     * would be done, without using inheritance and still use
     * the open close principle.
     */
     
    public class PersonFunctional
    {
        public string Name, Position;
    }

    public class PersonBuilderFunctional
    {
        public readonly List<Action<PersonFunctional>> Actions =
            new List<Action<PersonFunctional>>();   // this has to be public, which can be a downside

        public PersonBuilderFunctional Called(string name)
        {
            Actions.Add(p => { p.Name = name; });
            return this;
        }

        public PersonFunctional Build()
        {
            var p = new PersonFunctional();
            Actions.ForEach(a => a(p));
            return p;
        }
    }

    /**
     * Add the extension methods in this class and access
     * the Actions from the PersonBuilderFunctional.
     */ 
    public static class PersonBuilderExtension
    {
        public static PersonBuilderFunctional WorksAsA
            (this PersonBuilderFunctional builder, string position)
        {
            builder.Actions.Add(p => { p.Position = position; });
            return builder;
        }
    }

    /**
     * The result is the same, but this is done without using 
     * a OOP technique, but instead a functional programming technique.
     */ 
    public class FunctionalBuilder
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilderFunctional();
            pb.Called("Rune")
                .WorksAsA("Developer")
                .Build();

            Console.ReadLine();
        }
    }
}
