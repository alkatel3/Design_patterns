namespace TemplateMethod
{
    public abstract class Game
    {
        protected readonly int numberOfPlayers;
        protected int currentPlayer;

        protected abstract bool HaveWinner { get; }
        protected abstract int WinningPlayer { get; }

        public Game(int numberOfPlayers)
        {
            this.numberOfPlayers = numberOfPlayers;
        }

        public void Run()
        {
            Start();
            while (!HaveWinner)
                TakeTurn();
            Console.WriteLine($"Player {WinningPlayer} wins!");
        }

        protected abstract void TakeTurn();
        protected abstract void Start();
    }

    public class Chess : Game
    {
        private int turn = 1, maxTurn = 10;

        protected override bool HaveWinner => turn == maxTurn;

        protected override int WinningPlayer => currentPlayer;
        public Chess() 
            : base(2)
        {
        }



        protected override void Start()
        {
            Console.WriteLine($"Starting a game of chess with {numberOfPlayers} players");
        }

        protected override void TakeTurn()
        {
            Console.WriteLine($"Turn {turn++} taken by player {currentPlayer}.");
            currentPlayer = (currentPlayer + 1) % numberOfPlayers;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var chess = new Chess();
            chess.Run();
        }
    }
}