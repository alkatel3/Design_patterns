namespace MethodChain
{
    public class Creature
    {
        public string Name;
        public int Attack, Defense;

        public Creature(string name, int attack, int defense)
        {
            Name = name;
            Attack = attack;
            Defense = defense;
        }

        public override string ToString()
        {
            return $"{nameof(Name)}: {Name}, " +
                $"{nameof(Attack)}: {Attack}, " +
                $"{nameof(Defense)}: {Defense}";
        }
    }

    public class CreatureModifier
    {
        protected Creature creature;

        //singly linked list
        private CreatureModifier next;

        public CreatureModifier(Creature creature)
        {
            this.creature = creature;
        }

        public void Add(CreatureModifier m)
        {
            if (next != null)
            {
                next.Add(m);
            }
            else
            {
                next = m;
            }
        }

        public virtual void Handle() => next?.Handle();
    }

    public class DoubleAttackModifier : CreatureModifier
    {
        public DoubleAttackModifier(Creature creature) 
            : base(creature)
        {
        }

        public override void Handle()
        {
            Console.WriteLine($"Doubling {creature.Name}'s attack");
            creature.Attack *= 2;
            base.Handle();
        }
    }

    public class IncreaseDefenseModifier : CreatureModifier
    {
        public IncreaseDefenseModifier(Creature creature) 
            : base(creature)
        {
        }

        public override void Handle()
        {
            if (creature.Attack <= 2)
            {
                Console.WriteLine($"Increasing {creature.Name}'s defense");
                creature.Defense++;
            }
            base.Handle();
        }
    }

    public class NoBobuaeaModifier : CreatureModifier
    {
        public NoBobuaeaModifier(Creature creature) 
            : base(creature)
        {
        }
        public override void Handle()
        {
            
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var goblin = new Creature("Goblin", 1, 1);
            Console.WriteLine(goblin);

            var root = new CreatureModifier(goblin);

            //root.Add(new NoBobuaeaModifier(goblin));

            root.Add(new DoubleAttackModifier(goblin));
            root.Add(new DoubleAttackModifier(goblin));
            root.Add(new IncreaseDefenseModifier(goblin));

            root.Handle();
            Console.WriteLine(goblin);
        }
    }
}