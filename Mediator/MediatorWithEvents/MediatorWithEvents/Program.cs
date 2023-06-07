namespace MediatorWithEvents
{

    public abstract class GameEventArgs: EventArgs
    {
        public abstract void Print();
        
    }

    public class Game
    {
        public event EventHandler<GameEventArgs> Events;

        public void Fire(GameEventArgs args)
        {
            Events?.Invoke(this, args);
        }
    }

    public class PlayerScoredEventArgs : GameEventArgs
    {
        public string PlayerName;
        public int GoalsScoredSoFar;

        public PlayerScoredEventArgs(string playerName, int goalsScoredSoFar)
        {
            PlayerName = playerName;
            GoalsScoredSoFar = goalsScoredSoFar;
        }

        public override void Print()
        {
            Console.WriteLine($"{PlayerName} has scored! " +
                                $"(their {GoalsScoredSoFar} goal)");
        }
    }

    public class Player
    {
        private string name;
        private Game game;
        private int goalsScored = 0;

        public Player(string name, Game game)
        {
            this.name = name;
            this.game = game;

            game.Events += (sender, args) =>
            {
                if (args is PlayerScoredEventArgs scored &&
                scored.PlayerName == name)
                {
                    Console.WriteLine($"{name} says: yes, I did this!");
                }
            };
        }

        public void Score()
        {
            goalsScored++;
            var args = new PlayerScoredEventArgs(name, goalsScored);
            game.Fire(args);
        }
    }

    public class Coach
    {
        private Game game;

        public Coach(Game game)
        {
            this.game = game;

            game.Events += (sender, args) =>
            {
                if (args is PlayerScoredEventArgs scored &&
                scored.GoalsScoredSoFar < 3)
                {
                    Console.WriteLine($"Coach says: well done, {scored.PlayerName}");
                }
            };
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var game = new Game();
            var player = new Player("Sam", game);
            var player2 = new Player("Dan", game);
            var coach = new Coach(game);

            player.Score();
            player2.Score();
            player.Score();

            player2.Score();
            player.Score();
        }
    }
}