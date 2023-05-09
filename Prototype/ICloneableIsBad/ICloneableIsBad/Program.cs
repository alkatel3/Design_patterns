namespace ICloneableIsBad
{
    public class Person: ICloneable
    {
        public string[] Names;
        public Address Address;

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public object Clone()
        {
            return MemberwiseClone();
            //return new Person(Names, (Address)Address.Clone());
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(' ', Names)}," +
                $"{nameof(Address)}: {Address}";
        }
    }

    public class Address: ICloneable
    {
        public string StreetName;
        public int HouseNumber;

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public object Clone()
        {
            return new Address(StreetName, HouseNumber);
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}," +
                $" {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var john = new Person(
              new[] { "John", "Smith" },
              new Address("London Road", 123));

            var jane = (Person)john.Clone();
            jane.Address.HouseNumber = 321; // oops, John is now at 321

            // this doesn't work
            //var jane = john;

            // but clone is typically shallow copy
            jane.Names[0] = "Jane";

            Console.WriteLine(john);
            Console.WriteLine(jane);


        }
    }
}