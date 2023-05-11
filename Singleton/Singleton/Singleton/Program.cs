using MoreLinq;

namespace Singleton
{
    public class SingletoDatabase
    {
        private Dictionary<string, int> capitals;
        private static int instanceCount;
        public static int Count => instanceCount;

        private SingletoDatabase()
        {
            capitals = File.ReadAllLines(
                Path.Combine(
                    new FileInfo(typeof(SingletoDatabase).Assembly.Location).DirectoryName,
                    "capitals.txt"
                    )
                ).Batch(2).ToDictionary(
                list => list.ElementAt(0).Trim(),
                list => int.Parse(list.ElementAt(1)));
        }

        public int GetPopulation(string ciry)
        {
            return capitals[ciry];
        }

        private static Lazy<SingletoDatabase> instance =
            new Lazy<SingletoDatabase>(() =>
            {
                instanceCount++;
                return new SingletoDatabase();
            });

        public static SingletoDatabase Instance = instance.Value;
    }

    public class SingletonRecordFinder
    {
        public int TotalPopulation(IEnumerable<string> names)
        {
            int result = 0;
            foreach(var name in names)
            {
                result += SingletoDatabase.Instance.GetPopulation(name);
            }
            return result;
        }
    }

    public class Program
    {
        public static void Main(string[] args)
        {
            var db = SingletoDatabase.Instance;

            var city = "Tokyo";

            Console.WriteLine($"{city} has population {db.GetPopulation(city)}");
        }
    }
}