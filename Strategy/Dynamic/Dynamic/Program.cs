using System.Text;

namespace Dynamic
{
    public enum OutputFormat
    {
        Markdown,
        Html
    }

    public interface IListStrategy
    {
        void Start(StringBuilder sb);
        void End(StringBuilder sb);
        void AddListItem(StringBuilder sb, string item);
    }

    public class MarkdownLisrStrategy : IListStrategy
    {
        public void Start(StringBuilder sb)
        {
        }

        public void End(StringBuilder sb)
        {
        }

        public void AddListItem(StringBuilder sb, string item)
        {
            sb.AppendLine($" * {item}");
        }
    }

    public class HtmlListStrategy : IListStrategy
    {
        public void Start(StringBuilder sb)
        {
            sb.AppendLine("<ul>");
        }

        public void End(StringBuilder sb)
        {
            sb.AppendLine("</ul>");
        }

        public void AddListItem(StringBuilder sb, string item)
        {
            sb.AppendLine($"  <li>{item}</li>");
        }
    }

    public class TextProcessor
    {
        private StringBuilder sb = new StringBuilder();
        private IListStrategy listStrategy;

        public void SetOutpitFormat(OutputFormat format)
        {
            switch (format)
            {
                case OutputFormat.Markdown:
                    listStrategy = new MarkdownLisrStrategy();
                    break;
                case OutputFormat.Html:
                    listStrategy = new HtmlListStrategy();
                    break;
            }
        }

        public StringBuilder Clear()
        {
            return sb.Clear();
        }

        public override string ToString() => sb.ToString();


        public void AppendList(IEnumerable<string> items)
        {
            listStrategy.Start(sb);
            foreach(var item in items)
            {
                listStrategy.AddListItem(sb, item);
            }
            listStrategy.End(sb);
        }

    }

    public class Program
    {
        static void Main(string[] args)
        {
            var tp = new TextProcessor();
            tp.SetOutpitFormat(OutputFormat.Markdown);
            tp.AppendList(new[] { "foo", "bar", "bax" });
            Console.WriteLine(tp);

            tp.Clear();
            tp.SetOutpitFormat(OutputFormat.Html);
            tp.AppendList(new[] { "foo", "bar", "bax" });
            Console.WriteLine(tp);
        }
    }
}
