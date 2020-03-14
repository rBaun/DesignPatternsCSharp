using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace DesignPatterns.SOLID_Principles
{
    /**
     * Dependency Inversion principle is where a high level module should not depend
     * upon low level model, but instead you should use abstractions.
     * 
     * In this example the Research class should not depend on the Relationships class.
     * It should be able to modify the relationship as needed.
     * This is done by making interfaces, that exposes the relationships class.
     * 
     * An example can be seen down below...
     */ 
    public enum Relationship
    {
        Parent,
        Child,
        Sibling
    }

    public class Person
    {
        public string Name { get; set; }

    }

    public interface IRelationshipBrowser
    {
        IEnumerable<Person> FindAllChildrenOf(string name);
    }

    // low-level parts of the system
    public class Relationships : IRelationshipBrowser
    {
        private List<(Person, Relationship, Person)> relations 
            = new List<(Person, Relationship, Person)>(); // using C# tuples (A person and the relationship to a different person)

        public void AddParentAndChild(Person parent, Person child)
        {
            relations.Add((parent, Relationship.Parent, child)); // adding the parents relation to the child
            relations.Add((child, Relationship.Child, parent)); // adding the childs relation to the parent
        }

        public IEnumerable<Person> FindAllChildrenOf(string name)
        {
            return relations.Where(
                x => x.Item1.Name == name &&
                     x.Item2 == Relationship.Parent
            ).Select(r => r.Item3);
        }
    }

    public class Research
    {
        // Depending on the interface and not the class itself
        public Research(IRelationshipBrowser browser)
        {
            foreach (var p in browser.FindAllChildrenOf("John"))
                Console.WriteLine($"John has a child called {p.Name}");
        }

        //static void Main(string[] args)
        //{
        //    var parent = new Person { Name = "John" };
        //    var child1 = new Person { Name = "Chris" };
        //    var child2 = new Person { Name = "Mary" };

        //    var relationships = new Relationships();
        //    relationships.AddParentAndChild(parent, child1);
        //    relationships.AddParentAndChild(parent, child2);

        //    new Research(relationships);

        //    Console.ReadLine();
        //}
    }
}
