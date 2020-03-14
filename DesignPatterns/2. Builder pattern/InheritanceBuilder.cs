using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns._2._Builder_pattern
{
    /**
     * The Inheritance Builder uses recursion to build
     * the objects. In this case it builds a new Person.
     * 
     * The var in the static void Main method is equivalent to: PersonJobBuilder<Person.Builder> since 
     * it uses the recursive SELF to track it back to the builder class inside the 
     * person class.
     */ 
    public class Person
    {
        public string Name;
        public string Position;

        public class Builder : PersonJobBuilder<Builder>
        {
            // this Builder Class is exposed...
        }

        public static Builder New => new Builder();

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, {nameof(Position)}: {Position}";
        }
    }

    public abstract class PersonBuilder
    {
        protected Person person = new Person();

        public Person Build()
        {
            return person;
        }
    }

    public class PersonInfoBuilder<SELF>
        : PersonBuilder
        where SELF : PersonInfoBuilder<SELF> // class Foo : Bar<Foo>
    {
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF) this;
        }
    }

    public class PersonJobBuilder<SELF> 
        : PersonInfoBuilder<PersonJobBuilder<SELF>>
        where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorksAsA(string position)
        {
            person.Position = position;
            return (SELF) this;
        }
    }

    public class InheritanceBuilder
    {
        static void Main(string[] args)
        {
            var p = Person.New
                .Called("Rune")
                .WorksAsA("Developer")
                .Build();
            Console.WriteLine(p);


            Console.ReadLine();
        }
    }
}
