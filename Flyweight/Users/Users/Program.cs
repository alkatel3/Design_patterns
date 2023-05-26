namespace Users
{
    public class User
    {
        public string FullName { get; }

        public User(string fullName)
        {
            FullName = fullName;
        }
    }

    public class User2
    {
        static List<string> strings = new();
        private int[] names;

        public static List<string> Strings => strings;

        public User2(string fullName)
        {
            int getOrAdd (string s)
            {
                int idx = strings.IndexOf(s);
                if (idx != -1) return idx;
                else
                {
                    strings.Add(s);
                    return strings.Count - 1;
                }
            }

            names = fullName.Split(' ').Select(getOrAdd).ToArray();
        }

        public string FullName() 
            => string.Join(" ", names.Select(i => strings[i]));
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var user = new User2("Sam Smith");
            var user2 = new User2("Jame Smith");

            Console.WriteLine(User2.Strings.Count);
            Console.WriteLine(user.FullName());
            Console.WriteLine(user2.FullName());
        }
    }
}