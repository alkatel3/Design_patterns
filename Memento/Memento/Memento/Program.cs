﻿namespace Memento
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
        private List<Memento> changes
            = new List<Memento>();
        private int current;

        public BankAccount(int balance)
        {
            this.balance = balance;
            changes.Add(new Memento(balance));
        }

        public Memento Deposit(int amount)
        {
            balance += amount;
            var m = new Memento(balance);
            changes.Add(m);
            ++current;
            return m;
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }

        public void Restore(Memento m)
        {
            if (m != null)
            {

                balance = m.Balance;
                changes.Add(m);
                current = changes.Count - 1; 
            }
        }

        public Memento Undo()
        {
            if (current > 0)
            {
                var m = changes[--current];
                balance = m.Balance;
                return m;
            }
            return null;
        }

        public Memento Redo()
        {
            if (current + 1 < changes.Count)
            {
                var m = changes[++current];
                balance = m.Balance;
                return m;
            }
            return null;
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

            ba.Undo();
            Console.WriteLine("Undo 1: " + ba);

            ba.Undo();
            Console.WriteLine("Undo 2: " + ba);

            ba.Redo();
            Console.WriteLine("Redo 1: " + ba);
        }
    }
}