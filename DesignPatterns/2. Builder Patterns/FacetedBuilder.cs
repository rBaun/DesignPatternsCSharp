using System;
using System.Collections.Generic;
using System.Text;

namespace DesignPatterns._2._Builder_pattern
{
    /**
     * A Faceted Builder is used to make multiple fluent builders in one object.
     * You might want a builder for the address and the employment info, which is
     * done in this example using the PersonFaceted class.
     */ 
    public class FacetedBuilder
    {
        public class PersonFaceted
        {
            // address
            public string StreetAddress, Postcode, City;

            // employment
            public string CompanyName, Position;
            public int AnnualIncome;

            public override string ToString()
            {
                return $"Address Info: \n{nameof(StreetAddress)}: {StreetAddress},\n{nameof(Postcode)}: {Postcode},\n{nameof(City)}: {City} \n\n" +
                       $"Employment Info: \n{nameof(CompanyName)}: {CompanyName},\n{nameof(Position)}: {Position},\n{nameof(AnnualIncome)}: {AnnualIncome}\n";
            }
        }

        /**
         * This class works as a facacde and does NOT build the object,
         * but it keeps a reference. Instead it uses the reference of a person
         * in the builder classes below.
         */ 
        public class PersonFacadeBuilder
        {
            // reference
            protected PersonFaceted person = new PersonFaceted();

            // expose other builders
            public PersonFacetedJobBuilder Works => new PersonFacetedJobBuilder(person);
            public PersonFacetedAddressBuilder Lives => new PersonFacetedAddressBuilder(person);

            // only used to show person object in console.writeline instead of the actual object name
            public static implicit operator PersonFaceted(PersonFacadeBuilder pb)
            {
                return pb.person;
            }
        }

        /**
         * This is the builder for the address info.
         * It inherites from the PersonFacadeBuilder, which holds the reference to person.
         */
        public class PersonFacetedAddressBuilder : PersonFacadeBuilder
        {
            public PersonFacetedAddressBuilder(PersonFaceted person)
            {
                this.person = person; // this.person is the reference from PersonFacadeBuilder
            }

            public PersonFacetedAddressBuilder At(string streetAddress)
            {
                person.StreetAddress = streetAddress;
                return this;
            }

            public PersonFacetedAddressBuilder WithPostcode(string postcode)
            {
                person.Postcode = postcode;
                return this;
            }

            public PersonFacetedAddressBuilder In(string city)
            {
                person.City = city;
                return this;
            }
        }

        /**
         * This is the builder for the employment info.
         * It inherites from the PersonFacadeBuilder, which holds the reference to person.
         */
        public class PersonFacetedJobBuilder : PersonFacadeBuilder
        {
            public PersonFacetedJobBuilder(PersonFaceted person)
            {
                this.person = person; // this.person is the reference from PersonFacadeBuilder
            }

            public PersonFacetedJobBuilder At(string companyName)
            {
                person.CompanyName = companyName;
                return this;
            }

            public PersonFacetedJobBuilder AsA(string position)
            {
                person.Position = position;
                return this;
            }

            public PersonFacetedJobBuilder Earning(int amount)
            {
                person.AnnualIncome = amount;
                return this;
            }
        }

        //static void Main(string[] args)
        //{
        //    var pb = new PersonFacadeBuilder();
        //    PersonFaceted person = pb
        //        .Lives.At("Jomfru Ane Gade 21A")
        //              .In("Aalborg")
        //              .WithPostcode("9000")
        //        .Works.At("Google")
        //              .AsA("Developer")
        //              .Earning(50000);

        //    Console.WriteLine(person);
        //    Console.ReadLine();
        //}
    }
}
