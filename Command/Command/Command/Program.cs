namespace Command
{
    public class BankAccount
    {
        private int balance;
        private int overdraftLimit = -500;

        public void Deposit(int amount)
        {
            balance += amount;
            Console.WriteLine($"Deposited ${amount}, balance = ${balance}");
        }

        public void Withdraw(int amount)
        {
            if(balance - amount >= overdraftLimit)
            {
                balance -= amount;
                Console.WriteLine($"Withdrew ${amount}, balance = ${balance}");
            }
        }

        public override string ToString()
        {
            return $"{nameof(balance)}: {balance}";
        }
    }

    public class BankAccountCommand
    {
        private BankAccount account;

        public enum Action
        {
            Deposit, Withdraw
        }

        private Action action;
        private int amount;

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
                    break;
                case Action.Withdraw:
                    account.Withdraw(amount);
                    break;
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var ba = new BankAccount();
            var cmd = new BankAccountCommand(ba, BankAccountCommand.Action.Deposit, 100);
            cmd.Call();
            Console.WriteLine(ba); ;
        }
    }
}