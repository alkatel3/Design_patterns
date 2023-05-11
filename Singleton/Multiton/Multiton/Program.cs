namespace Multiton
{
    public enum Subsystem
    {
        Main, 
        Backup
    }

    public class Printer
    {
        private Printer()
        {

        }

        public static Printer Get(Subsystem ss)
        {
            if (instances.ContainsKey(ss))
            {
                return instances[ss];
            }

            var instance = new Printer();
            instances[ss] = instance;
            return instance;
        }

        private static readonly Dictionary<Subsystem, Printer> instances
            = new();
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var primary = Printer.Get(Subsystem.Main);
            var backup = Printer.Get(Subsystem.Backup);

            var primary2 = Printer.Get(Subsystem.Main);

            Console.WriteLine(ReferenceEquals(primary, primary2));
        }
    }
}