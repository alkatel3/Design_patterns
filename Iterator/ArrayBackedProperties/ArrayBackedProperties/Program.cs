using System.Collections;

namespace ArrayBackedProperties
{
    public class Creature :IEnumerable<int>
    {
        private int[] stats = new int[3];

        public IEnumerable<int> Stats => stats;

        private const int strength = 0;
        public int Strength
        {
            get => stats[strength];
            set => stats[strength] = value;
        }

        private const int agility = 1;
        public int Agility
        {
            get => stats[agility];
            set => stats[agility]=value;
        }

        private const int intelligence = 2;
        public int Intelligence
        {
            get => stats[intelligence];
            set => stats[intelligence] = value;
        }


        public double SumOfStats => stats.Sum();

        public double MaxStat => stats.Max();

        public double AverageStat => stats.Average();

        public IEnumerator<int> GetEnumerator()
        {
            return stats.AsEnumerable().GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public int this[int index]
        {
            get => stats[index];
            set => stats[index] = value;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var creature = new Creature();
            creature.Strength = 10;
            creature.Intelligence = 11;
            creature.Agility = 12;

            Console.WriteLine(
                $"Max stat = {creature.MaxStat}, " +
                $"average stat = {creature.AverageStat}, " +
                $"sum = {creature.SumOfStats}");

            
        }
    }
}