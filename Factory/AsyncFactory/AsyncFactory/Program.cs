namespace AsyncFactory
{
    public interface IAsyncItit<T>
    {
        Task<T> InitAsync();
    }

    public class Foo:IAsyncItit<Foo>
    {
        /*public*/ public Foo()
        {
            //await Task.Delay(1000);
        }

        // ↓↓
        public async Task<Foo> InitAsync()
        {
            // some work here
            await Task.Delay(1000);
            return this; // fluent
        }

        public static async Task<Foo> CreateAsync()
        {
            var result = new Foo();
            return await result.InitAsync();
        }
    }

    public static class AsyncFactory
    {
        public static async Task<T> Create<T>()
            where T : IAsyncItit<T>, new()
        {
            return await new T().InitAsync();
        }
    }

    public class Program
    {
        public static async void Main(string[] args)
        {
            // var foo = new Foo();
            // await foo.InitAsync();

            // instead
            //var foo = await Foo.CreateAsync();

            // or
            var foo2 = AsyncFactory.Create<Foo>();
        }
    }
}