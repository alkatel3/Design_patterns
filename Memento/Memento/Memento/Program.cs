namespace Memento
{
    public class Memento
    {
        public int Balance { get; }

        public Memento(int balance)
        {
            Balance = balance;
        }
    }

    public class BankAccount
    {
        private int balance;

        public BankAccount(int balance)
        {
            this.balance = balance;
        }

        public Memento Deposit(int amount)
        {
            balance += amount;
            return new Memento(balance);
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }

        public void Restore(Memento m)
        {
            balance = m.Balance;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var ba = new BankAccount(100);

            var m1 = ba.Deposit(50);
            var m2 = ba.Deposit(20);
            Console.WriteLine(ba);

            //m1
            ba.Restore(m1);
            Console.WriteLine(ba);

            //m2
            ba.Restore(m2);
            Console.WriteLine(ba);
        }
    }
}