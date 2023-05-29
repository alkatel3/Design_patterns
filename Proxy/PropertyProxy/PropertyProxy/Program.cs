namespace PropertyProxy
{
    public class Property<T> where T : new()
    {
        private T value;
        private readonly string name;

        public T Value
        {
            get => value;
            set
            {
                if (Equals(this.value, value))
                    return;
                Console.WriteLine($"Assigning {value} to {name}");
                this.value = value;
            }
        }

        public Property() : this(default(T)) { }

        public Property(T value, string name="")
        {
            this.value = value;
            this.name = name;
        }

        public static implicit operator T(Property<T> p)
        {
            return p.Value;
        }

        public static implicit operator Property<T>(T value)
        {
            return new Property<T>(value);
        }

        protected bool Equals(Property<T> other)
        {
            return EqualityComparer<T>.Default.Equals(value, other.value);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Property<T>)obj);
        }

        public override int GetHashCode()
        {
            return EqualityComparer<T>.Default.GetHashCode(value);
        }
    }

    public class Creature
    {
        private readonly Property<int> agility =
            new Property<int>(10, nameof(Agility));

        public int Agility
        {
            get => agility.Value;
            set => agility.Value = value;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var c = new Creature();
            c.Agility = 12;
            int n = c.Agility;
        }
    }
}