using System;
using System.Dynamic;
using ImpromptuInterface;
using static System.Console;

namespace NullObject
{
    public interface ILog
    {
        void Info(string msg);
        void Warn(string msg);
    }

    class ConsoleLog : ILog
    {
        public void Info(string msg)
        {
            WriteLine(msg);
        }

        public void Warn(string msg)
        {
            WriteLine("WARNING: " + msg);
        }
    }

    class OptionalLog : ILog
    {
        private readonly ILog impl;

        public OptionalLog(ILog impl)
        {
            this.impl = impl;
        }

        public void Info(string msg)
        {
            impl?.Info(msg);
        }

        public void Warn(string msg)
        {
            impl?.Warn(msg);
        }
    }

    public class BankAccount
    {
        private ILog log;
        private int balance;

        public BankAccount(ILog log)
        {
            this.log = new OptionalLog(log);
        }

        public void Deposit(int amount)
        {
            balance += amount;
            // check for null everywhere
            log?.Info($"Deposited ${amount}, balance is now {balance}");
        }

        public void Withdraw(int amount)
        {
            if (balance >= amount)
            {
                balance -= amount;
                log?.Info($"Withdrew ${amount}, we have ${balance} left");
            }
            else
            {
                log?.Warn($"Could not withdraw ${amount} because " +
                          $"balance is only ${balance}");
            }
        }
    }

    public sealed class NullLog : ILog
    {
        public void Info(string msg) { }
        public void Warn(string msg) { }
    }

    public class Program
    {
        static void Main()
        {
            //var log = new ConsoleLog();
            //ILog log = null;
            var log = new NullLog();
            var ba = new BankAccount(log);
            ba.Deposit(100);
            ba.Withdraw(200);
        }
    }
}