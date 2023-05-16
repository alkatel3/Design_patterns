using Autofac;
using Bridge;

namespace Bridge
{
    // shape: square, circle, triangle
    // renderer: raaster, vector

    public interface IRenderer
    {
        void RenderCircle(float radius);
    }

    public class VectorRenderer : IRenderer
    {

        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing circle of radius {radius}");
        }
    }

    public class RasterRenderer : IRenderer
    {
        public void RenderCircle(float radius)
        {
            Console.WriteLine($"Drawing pixels for circle of radius {radius}");
        }
    }

    public abstract class Shape
    {
        protected IRenderer renderer;

        // a bridge between the shape that's being drawn an
        // the component which actually draws it
        protected Shape(IRenderer renderer)
        {
            this.renderer = renderer;
        }

        public abstract void Draw();
        public abstract void Resize(float factor);
    }

    public class Circle : Shape
    {
        private float radius;

        public Circle(IRenderer renderer, float radius) : base(renderer)
        {
            this.radius = radius;
        }

        public override void Draw()
        {
            renderer.RenderCircle(radius);
        }

        public override void Resize(float factor)
        {
            radius *= factor;
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            //var raster = new RasterRenderer();
            //var vector = new VectorRenderer();
            //var circle = new Circle(vector, 5);
            //circle.Draw();
            //circle.Resize(2);
            //circle.Draw();

            var cb = new ContainerBuilder();
            // IRenderer -> VectorRenderer
            cb.RegisterType<VectorRenderer>().As<IRenderer>();

            cb.Register((c, p) => new Circle(
                c.Resolve<IRenderer>(),
                p.Positional<float>(0)
                ));

            // todo: delegate factories

            using (var c = cb.Build())
            {
                var circle = c.Resolve<Circle>(
                    new PositionalParameter(0, 5.0f)
                    );
                circle.Draw();
                circle.Resize(3);
                circle.Draw();
            }
        }
    }
}


//Home Task

//using System;

//  namespace Coding.Exercise
//{
//    public interface IRenderer
//    {
//        string WhatToRenderAs { get; }
//    }

//    public class VectorRenderer : IRenderer
//    {
//        public string WhatToRenderAs => "lines";
//    }

//    public class RasterRenderer : IRenderer
//    {
//        public string WhatToRenderAs => "pixels";
//    }

//    public abstract class Shape
//    {
//        protected IRenderer renderer;

//        protected Shape(IRenderer renderer)
//        {
//            this.renderer = renderer;
//        }

//        public string Name { get; set; }

//        public override string ToString()
//        {
//            return $"Drawing {Name} as {renderer.WhatToRenderAs}";
//        }
//    }


//    public class Triangle : Shape
//    {
//        public Triangle(IRenderer renderer) : base(renderer) => Name = "Triangle";
//    }

//    public class Square : Shape
//    {
//        public Square(IRenderer renderer) : base(renderer) => Name = "Square";
//    }

//    public class Program
//    {
//        static void Main(string[] args)
//        {
//            Console.WriteLine(new Triangle(new RasterRenderer()).ToString());
//        }
//    }
//}

