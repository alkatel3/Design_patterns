using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Xml.Serialization;

namespace PrototypeFactory
{
    public static class ExtensionMeethods
    {
        public static T DeepCopy<T>(this T self)
        {
            using (var stream = new MemoryStream())
            {
                BinaryFormatter formatter = new BinaryFormatter();
                formatter.Serialize(stream, self);
                stream.Seek(0, SeekOrigin.Begin);
                object copy = formatter.Deserialize(stream);
                return (T)copy;
            }
        }

        public static T DeepCopyXML<T>(this T self)
        {
            using (var ms = new MemoryStream())
            {
                XmlSerializer s = new XmlSerializer(typeof(T));
                s.Serialize(ms, self);
                ms.Position = 0;
                return (T)s.Deserialize(ms);
            }
        }
    }

    [Serializable]
    public class Employee
    {
        public string Name;
        public Address Address;

        public Employee(string name, Address address)
        {
            Name = name;
            Address = address;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}," +
                $" {nameof(Address)}: {Address}";
        }
    }

    [Serializable]
    public class Address
    {
        public string StreetAddress, City;
        public int Suite;

        public Address(string streetAddress, string city, int suite)
        {
            StreetAddress = streetAddress;
            City = city;
            Suite = suite;
        }

        public override string ToString()
        {
            return $"{nameof(StreetAddress)}: {StreetAddress}," +
                $" {nameof(City)}: {City}, {nameof(Suite)}: {Suite}";
        }
    }

    public class EmployeeFactory
    {
        private static Employee main =
            new Employee(null, new Address("123 East Dr", "London", 0));
        private static Employee aux =
            new Employee(null, new Address("321 East Dr", "Chicago", 0));

        private static Employee NewEmployee(Employee proto, string name, int suite)
        {
            var copy = proto.DeepCopy();
            copy.Name = name;
            copy.Address.Suite = suite;
            return copy;
        }

        public static Employee NewMainOfficeEmployee
            (string name, int suite) => NewEmployee(main, name, suite);

        public static Employee NewAuxOfficeEmployee
            (string name, int suite) => NewEmployee(aux, name, suite);
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var john = EmployeeFactory.NewMainOfficeEmployee("John", 123);
            Console.WriteLine(john);
        }
    }
}