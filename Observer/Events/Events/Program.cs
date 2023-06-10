namespace Events
{
    public class FallsIllEventArgs : EventArgs
    {
        public string Address;
    }

    public class Person
    {
        public event EventHandler<FallsIllEventArgs> FallsIll;

        public void CatchCold()
        {
            FallsIll?.Invoke(
                this, new FallsIllEventArgs
                {
                    Address = "123 London Road"
                });
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var person = new Person();

            person.FallsIll += PersonFallsIll;

            person.CatchCold();

            person.FallsIll -= PersonFallsIll;
        }

        private static void PersonFallsIll(object? sender, FallsIllEventArgs e)
        {
            Console.WriteLine($"Call a doctor to {e.Address}");
        }
    }
}