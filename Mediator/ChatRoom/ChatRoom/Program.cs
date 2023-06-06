namespace ChatRoom
{
    public class Person
    {
        public string Name;
        public ChatRoom Room;
        private List<string> chatLog = new List<string>();

        public Person(string name)
        {
            Name = name;
        }

        public void Receive(string sender, string message)
        {
            string s = $"{sender}: '{message}'";
            Console.WriteLine($"[{Name}]'s chat session {s}");
            chatLog.Add(s);
        }

        public void Say(string message)
            => Room.Broadcast(Name, message);

        public void PrivateMessage(string who, string message)
        {
            Room.Message(Name, who, message);
        }
    }

    public class ChatRoom
    {
        private List<Person> people = new List<Person>();

        internal void Broadcast(string source, string message)
        {
            foreach(var person in people)
            {
                if (person.Name != source)
                    person.Receive(source, message);
            }
        }

        public void Join(Person p)
        {
            string jounMsg = $"{p.Name} joins the chat";

            Broadcast("room", jounMsg);

            p.Room = this;
            people.Add(p);
        }

        internal void Message(string source, string destination, string message)
        {
            people.FirstOrDefault(p => p.Name == destination)
                ?.Receive(source, message);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var room = new ChatRoom();

            Person joun = new Person("Joun");
            Person jane = new Person("Jane");

            room.Join(joun);
            room.Join(jane);

            joun.Say("hi room!");
            jane.Say("oh, hey joun");

            var simon = new Person("Simon");
            room.Join(simon);
            simon.Say("hi everyone!");
            jane.PrivateMessage("Simon", "glad you could join us!;");
        }
    }
}