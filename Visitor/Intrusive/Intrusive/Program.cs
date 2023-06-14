using System.Text;

namespace Intrusive
{
    public abstract class Expressino
    {
        // adding a new operation
        public abstract void Print(StringBuilder sb);
    }

    public class DoubleExpression : Expressino
    {
        private double value;

        public DoubleExpression(double value)
        {
            this.value = value;
        }

        public override void Print(StringBuilder sb)
        {
            sb.Append(value);
        }
    }

    public class AdditionExpresion : Expressino
    {
        private Expressino left, right;

        public AdditionExpresion(Expressino left, Expressino right)
        {
            this.left = left;
            this.right = right;
        }

        public override void Print(StringBuilder sb)
        {
            sb.Append("(");
            left.Print(sb);
            sb.Append("+");
            right.Print(sb);
            sb.Append(")");
        }
    }

    //intrusive

    public class Program
    {
        static void Main(string[] args)
        {
            // 1+(2+3)

            var e = new AdditionExpresion(
                left: new DoubleExpression(1),
                right: new AdditionExpresion(
                    left: new DoubleExpression(2),
                    right: new DoubleExpression(3)));

            var sb = new StringBuilder();
            e.Print(sb);
            Console.WriteLine(sb);

            // what is more likely: new type or new operation
        }
    }
}