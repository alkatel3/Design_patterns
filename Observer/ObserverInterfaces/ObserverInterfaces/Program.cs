using System.Reactive.Linq;

namespace ObserverInterfaces
{
    public class Event
    {

    }

    public class FallsIllEvent : Event
    {
        public string Address;
    }

    public class Person : IObservable<Event>
    {
        private readonly HashSet<Subscription> subscriptions
            = new HashSet<Subscription>();

        public void CatchACold()
        {
            foreach(var sub in subscriptions)
            {
                sub.Observer.OnNext(
                    new FallsIllEvent { Address ="123 London Road"});
            }
        }

        public IDisposable Subscribe(IObserver<Event> observer)
        {
            var sub = new Subscription(this, observer);
            subscriptions.Add(sub);
            return sub; //flyweight
        }


        private class Subscription : IDisposable
        {
            private Person person;
            public IObserver<Event> Observer;

            public Subscription(Person person, IObserver<Event> observer)
            {
                this.person = person;
                Observer = observer;
            }

            public void Dispose()
            {
                person.subscriptions.Remove(this);
            }
        }
    }

    public class Program //:IObserver<Event>
    {
        public Program()
        {
            Person person = new Person();
            //var sub = person.Subscribe(this);

            person.OfType<FallsIllEvent>()
                .Subscribe(args => // OnNext
                Console.WriteLine(
                    $"We need a doctor to {args.Address}"));

            person.CatchACold();
        }

        static void Main(string[] args)
        {
            new Program();
        }

        //public void OnCompleted()
        //{
        //}

        //public void OnError(Exception error)
        //{
        //}

        //public void OnNext(Event value)
        //{
        //    if(value is FallsIllEvent args)
        //    {
        //        Console.WriteLine(
        //            $"Call doctor to {args.Address}");
        //    }
        //}
    }
}