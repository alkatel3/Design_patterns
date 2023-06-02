using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;

namespace Command
{
    public class BankAccount
    {
        private int balance;
        private int overdraftLimit = -500;

        public BankAccount(int balance)
        {
            this.balance = balance;
        }

        public void Deposit(int amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited ${amount}, balance = ${balance}");
        }

        public bool Withdraw(int amount)
        {
            if(balance - amount >= overdraftLimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew ${amount}, balance = ${balance}");
                return true;
            }
            return false;
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: ${balance}";
        }
    }

    public interface ICommand
    {
        void Call();
        void Undo();
        bool Success { get; set; }
    }

    public class BankAccountCommand : ICommand
    {
        private BankAccount account;

        public enum Action
        {
            Deposit, Withdraw
        }

        private Action action;
        private int amount;

        public bool Success { get; set; }

        public BankAccountCommand(BankAccount account, Action action, int amount)
        {
            this.account = account;
            this.action = action;
            this.amount = amount;
        }

        public void Call()
        {
            switch (action)
            {
                case Action.Deposit:
                    account.Deposit(amount);
                    Success = true;
                    break;
                case Action.Withdraw:
                    Success = account.Withdraw(amount);
                    break;
            }
        }

        public void Undo()
        {
            if (!Success) return;

            switch (action)
            {
                case Action.Deposit:
                    account.Withdraw(amount);
                    break;
                case Action.Withdraw:
                    account.Deposit(amount);
                    break;
            }
        }

        public class CompositeBankAccountCommand : List<BankAccountCommand>, ICommand
        {
            public bool Success { get; set; }

            public CompositeBankAccountCommand()
            {
                
            }

            public CompositeBankAccountCommand
                ([NotNull] IEnumerable<BankAccountCommand> collection)
                : base(collection)
            {
                
            }

            public void Call()
            {
                Success = true;
                ForEach(cmd =>
                {
                    cmd.Call();
                    Success &= cmd.Success;
                });
            }

            public void Undo()
            {
                foreach(var cmd in ((IEnumerable<BankAccountCommand>)this).Reverse())
                {
                    cmd.Undo();
                }
            }
        }

        public class Program
        {
            static void Main(string[] args)
            {
                var ba = new BankAccount(0);
                var cmdDeposit = new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100);
                var cmdWithdraw = new BankAccountCommand(ba, BankAccountCommand.Action.Withdraw, 10);

                var comppsit = new CompositeBankAccountCommand(
                    new[] { cmdDeposit, cmdWithdraw }
                    );

                comppsit.Call();

                Console.WriteLine(ba);

                comppsit.Undo();
                Console.WriteLine(ba);
            }
        }
    }
}