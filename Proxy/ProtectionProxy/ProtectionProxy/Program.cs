namespace ProtectionProxy
{
    public interface ICar
    {
        void Drive();
    }

    public class Car
    {
        public void Drive()
        {
            Console.WriteLine("Car being driven");
        }
    }

    public class Driver
    {
        public int Age;

        public Driver(int age)
        {
            Age = age;
        }
    }

    public class CarProxy : ICar
    {
        private Car car =new();
        private Driver driver;

        public CarProxy(Driver driver)
        {
            this.driver = driver;
        }

        public void Drive()
        {
            if (driver.Age >= 16)
                car.Drive();
            else
                Console.WriteLine("Driver is too young");
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var car = new CarProxy(new Driver(18));
            car.Drive();
        }
    }
}