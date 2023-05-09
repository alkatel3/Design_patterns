namespace CopyConstructors
{
    public class Address
    {
        public readonly string StreetName;
        public int HouseNumber;

        public Address(Address address)
        {
            StreetName = address.StreetName;
            HouseNumber = address.HouseNumber;
        }

        public Address(string streetName, int houseNumber)
        {
            StreetName = streetName;
            HouseNumber = houseNumber;
        }

        public override string ToString()
        {
            return $"{nameof(StreetName)}: {StreetName}, {nameof(HouseNumber)}: {HouseNumber}";
        }
    }

    public class Person 
    {
        public readonly string[] Names;
        public readonly Address Address;
        
        public Person(Person other)
        {
            Names = new string[other.Names.Length];
            Array.Copy(other.Names, Names, other.Names.Length);
            Address = new Address(other.Address);
        }

        public Person(string[] names, Address address)
        {
            Names = names;
            Address = address;
        }

        public override string ToString()
        {
            return $"{nameof(Names)}: {string.Join(" ", Names)}, {nameof(Address)}: {Address}";
        }
    }

    public static class Demo
    {
        static void Main()
        {
            var john = new Person(
              new[] { "John", "Smith" },
              new Address("London Road", 123));

            var jane = new Person(john);
            jane.Address.HouseNumber = 321;
            jane.Names[0] = "Jane";

            Console.WriteLine(john);
            Console.WriteLine(jane);
        }
    }

}