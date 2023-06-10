using JetBrains.Annotations;
using System.ComponentModel;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;

namespace PropertyDependencies
{
    public class PropertyNotificationSupport
        : INotifyPropertyChanged, INotifyPropertyChanging
    {
        private readonly Dictionary<string, HashSet<string>> affectedBy
            = new Dictionary<string, HashSet<string>>();

        public event PropertyChangingEventHandler? PropertyChanging;
        public event PropertyChangedEventHandler? PropertyChanged;

        protected Func<T> property<T>(string name, Expression<Func<T>> exception)
        {
            Console.WriteLine($"Creating computed property for expression {exception}");

            var visitor = new MemberAccessVisitor(GetType());
            visitor.Visit(exception);

            if (visitor.PropertyNames.Any())
            {
                if (!affectedBy.ContainsKey(name))
                    affectedBy.Add(name, new HashSet<string>());

                foreach (var propName in visitor.PropertyNames)
                    if (propName != name)
                        affectedBy[name].Add(propName);
            }


            return exception.Compile();
        }

        private class MemberAccessVisitor : ExpressionVisitor
        {
            private readonly Type declaringType;
            public readonly IList<string> PropertyNames = new List<string>();

            public MemberAccessVisitor(Type declaringType)
            {
                this.declaringType = declaringType;
            }

            public override Expression Visit(Expression expr)
            {
                if (expr != null && expr.NodeType == ExpressionType.MemberAccess)
                {
                    var memberExpr = (MemberExpression)expr;
                    if (memberExpr.Member.DeclaringType == declaringType)
                    {
                        PropertyNames.Add(memberExpr.Member.Name);
                    }
                }

                return base.Visit(expr);
            }
        }

        protected virtual void OnPropertyChanging(
            [CallerMemberName] string propertyName = null)
        {
            PropertyChanging?.Invoke(this,
              new PropertyChangingEventArgs(propertyName));
        }

        [NotifyPropertyChangedInvocator]
        protected virtual void OnPropertyChanged
            ([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

            foreach (var affected in affectedBy.Keys)
                if (affectedBy[affected].Contains(propertyName))
                    OnPropertyChanged(affected);
        }

        protected void setValue<T>(T value, ref T field,
            [CallerMemberName] string propertyName = null)
        {
            if (value.Equals(field)) return;
            OnPropertyChanging(propertyName);
            field = value;
            OnPropertyChanged(propertyName);
        }
    }

    public class Person
        : PropertyNotificationSupport
    {
        private int age;

        public int Age
        {
            get => age;
            set => setValue(value, ref age);
        }

        private bool citizen;
        public bool Citizen
        {
            get => citizen;
            set => setValue(value, ref citizen);
        }
        private readonly Func<bool> canVote;
        public bool CanVote => canVote();

        public Person()
        {
            canVote = property(nameof(CanVote),
                () => Age >= 16 && Citizen);
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var person = new Person { Age = 15, Citizen = true };

            person.PropertyChanged += PersonOnPropertyChanged;
            Console.WriteLine("Changing Age:");
            person.Age++;
            Console.WriteLine("Changing citizenship:");
            person.Citizen = false;
        }

        private static void PersonOnPropertyChanged(object? sender, PropertyChangedEventArgs e)
        {
            var p = (Person)sender;
            if(e.PropertyName == "CanVote")
                Console.WriteLine($"Voting status changed ({p.Age})");
        }
    }
}