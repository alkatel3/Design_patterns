namespace Builder_Facats
{
    public class Person
    {
        //address
        public string StreetAddress, Postcode, City;

        //employment info
        public string CompanyName, Position;
        public int AnnualIncome;

        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}, {nameof(Postcode)}: {Postcode}," +
                $" {nameof(City)}: {City}, {nameof(CompanyName)}: {CompanyName}," +
                $"{nameof(Position)}: {Position}, {nameof(AnnualIncome)}: {AnnualIncome}";
        }

        
    }

    public class PersonBuilder //Facade 
    {
        protected Person person;

        public PersonBuilder()
        {
            person = new Person();
        }

        public PersonBuilder(Person person)
        {
            this.person = person;
        }

        public PersonAddressBuilder Lives => new PersonAddressBuilder(person);
        public PersonJobBuilder Job => new PersonJobBuilder(person);

        public static implicit operator Person(PersonBuilder pb)
        {
            return pb.person;
        }
    }

    public class PersonAddressBuilder : PersonBuilder
    {
        public PersonAddressBuilder(Person person) : base(person)
        {
        }

        public PersonAddressBuilder At(string streetAddress)
        {
            person.StreetAddress = streetAddress;
            return this;
        }

        public PersonAddressBuilder WithPostcode(string postcode)
        {
            person.Postcode=postcode;
            return this;
        }

        public PersonAddressBuilder In(string city)
        {
            person.City = city;
            return this;
        }
    }

    public class PersonJobBuilder : PersonBuilder
    {
        public PersonJobBuilder(Person person) : base(person)
        {
        }

        public PersonJobBuilder At(string companyName)
        {
            person.CompanyName = companyName;
            return this;
        }

        public PersonJobBuilder AsA(string position)
        {
            person.Position = position;
            return this;
        }

        public PersonJobBuilder Earning(int annualIncome)
        {
            person.AnnualIncome = annualIncome;
            return this;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var pb = new PersonBuilder();
            Person person = pb
                .Lives
                    .At("123 Londot Road")
                    .In("London")
                    .WithPostcode("SQ12BC")
                .Job
                    .At("Fabrikam")
                    .AsA("Engineer")
                    .Earning(123000);
            Console.WriteLine(person);


        }
    }
}