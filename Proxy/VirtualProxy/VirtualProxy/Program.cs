namespace VirtualProxy
{
    public interface IBitmap
    {
        void Draw();
    }

    public class Bitmap : IBitmap
    {
        private readonly string filename;

        public Bitmap(string filename)
        {
            this.filename = filename;
            Console.WriteLine($"Loading image from {filename}");
        }

        public void Draw()
        {
            Console.WriteLine($"Drawing image {filename}");
        }
    }

    public class LazyBitmap : IBitmap
    {
        private readonly string filename;
        private Bitmap bitMap;

        public LazyBitmap(string filename)
        {
            this.filename = filename;
        }

        public void Draw()
        {
            if (bitMap == null)
            {
                bitMap = new Bitmap(filename);
            }
            bitMap.Draw();
        }
    }

    public class Program
    {
        public static void DrawImage(IBitmap img)
        {
            Console.WriteLine("About to draw image");
            img.Draw();
            Console.WriteLine("Done drawing image");
        }

        static void Main(string[] args)
        {
            var img = new LazyBitmap("any.png");
            DrawImage(img);
        }
    }
}