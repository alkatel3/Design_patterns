using System.Diagnostics;

namespace ValueProxy
{
    [DebuggerDisplay("{value*100.0f}%")]
    public struct Percentage
    {
        private readonly decimal value;

        public Percentage(decimal value)
        {
            this.value = value;
        }

        public static decimal operator *(decimal a, Percentage b)
        {
            return a * b.value;
        }

        public static Percentage operator +(Percentage a, Percentage b)
        {
            return new Percentage(a.value + b.value);
        }

        public static implicit operator Percentage(int value)
        {
            return value.Percent();
        }

        public bool Equals(Percentage other)
        {
            return value.Equals(other.value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            return obj is Percentage other && Equals(other);
        }

        public override int GetHashCode()
        {
            return value.GetHashCode();
        }

        public override string ToString()
        {
            return $"{value * 100}%";
        }
    }

    public static class ExtensionMethods
    {
        public static Percentage Percent(this int value)
        {
            return new Percentage(value / 100.0m);
        }
        public static Percentage Percent(this decimal value)
        {
            return new Percentage(value / 100.0m);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine(10m * 5.Percent());
            Console.WriteLine(2.Percent() + 3m.Percent());
        }
    }
}