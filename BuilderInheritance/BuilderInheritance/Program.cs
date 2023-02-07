namespace BuilderInheritance
{
    public class Person
    {
        public string Name;
        public string Position;


        public class Builder : PersonJobBuilder<Builder>
        {
            internal Builder()
            {
            }
        }
        public static Builder New => new Builder();
    }

    public abstract class PersonBuilder
    {
        protected Person person;

        public Person Build()
        {
            return person;
        }
    }

    public class PersonInfoBuilder<SELF> : PersonBuilder
        where SELF :PersonInfoBuilder<SELF>
    {
        public SELF Called(string name)
        {
            person.Name = name;
            return (SELF) this;
        }
    }

    public class PersonJobBuilder<SELF>
        : PersonInfoBuilder<SELF>
        where SELF : PersonJobBuilder<SELF>
    {
        public SELF WorksAs(string position)
        {
            person.Position = position;
            return (SELF) this;
        }
    }

    internal class Program
    {
        static void Main(string[] args)
        {
            var me = Person.New
                .Called("Vitaliy")
                .WorksAs("Engineer")
                .Build();
        }
    }
}