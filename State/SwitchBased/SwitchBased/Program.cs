using System.Text;

namespace SwitchBased
{
    public enum State
    {
        Locked,
        Failed,
        UnLocked
    }

    public class Program
    {
        static void Main(string[] args)
        {
            string code = "1234";
            var state = State.Locked;
            var entry = new StringBuilder();
            var data = new Queue<int>(new[] { 1, 2, 2, 4 });

            while (true)
            {
                switch (state)
                {
                    case State.Locked:
                        var value = data.Dequeue();
                        Console.WriteLine(value);
                        entry.Append(value);

                        if (entry.ToString() == code)
                        {
                            state = State.UnLocked;
                            break;
                        }

                        if (!code.StartsWith(entry.ToString()))
                            state = State.Failed; 

                        break;
                    case State.Failed:
                        Console.WriteLine("FAILED");
                        return;
                    case State.UnLocked:
                        Console.WriteLine("UNLOCKED");
                        return;
                }
            }
        }
    }
}