using Autofac;
using Autofac.Features.Metadata;

namespace AdapterDI
{
    public interface ICommand
    {
        void Execute();
    }

    public class SaveCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Saving current file");
        }
    }

    public class OpenCommand : ICommand
    {
        public void Execute()
        {
            Console.WriteLine("Opening file");
        }
    }

    public class Button
    {
        private ICommand command;
        private string name;

        public Button(ICommand command, string name)
        {
            this.command = command;
            this.name = name;
        }

        public void Click()
        {
            command.Execute();
        }

        public void PrintMe()
        {
            Console.WriteLine($"I am a button called {name}");
        }
    }

    public class Editor
    {
        public IEnumerable<Button> Buttons { get; }

        public Editor(IEnumerable<Button> buttons)
        {
            this.Buttons = buttons;
        }

        public void ClickAll()
        {
            foreach(var btn in Buttons)
            {
                btn.Click();
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var b = new ContainerBuilder();
            b.RegisterType<OpenCommand>()
                .As<ICommand>()
                .WithMetadata("Name","Open");
            b.RegisterType<SaveCommand>()
                .As<ICommand>()
                .WithMetadata("Name", "Save"); ;

            //b.RegisterType<Button>();
            b.RegisterAdapter<Meta<ICommand>, Button>(
                cmd => new Button(cmd.Value, (string)cmd.Metadata["Name"])
                );

            b.RegisterType<Editor>();

            using var c = b.Build();
            var editor = c.Resolve<Editor>();
            editor.ClickAll();

            foreach(var btn in editor.Buttons)
            {
                btn.PrintMe();
            }
        }
    }
}