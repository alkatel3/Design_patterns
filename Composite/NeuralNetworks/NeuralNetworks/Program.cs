﻿using Microsoft.VisualBasic;
using System.Collections;
using System.Collections.ObjectModel;

namespace NeuralNetworks
{
    public static class ExtensionMethods
    {
        public static void ConnectTo(this IEnumerable<Neuron> self, IEnumerable<Neuron> other)
        {
            if (ReferenceEquals(self, other))
                return;

            foreach(var from in self)
                foreach(var to in other)
                {
                    from.Out.Add(to);
                    to.In.Add(from);
                }
        }
    }

    public class Neuron : IEnumerable<Neuron>
    {
        public List<Neuron> In = new List<Neuron>(),
            Out =new List<Neuron>();

        //public void ConnectTo(Neuron other)
        //{
        //    Out.Add(other);
        //    other.In.Add(this);
        //}

        public IEnumerator<Neuron> GetEnumerator()
        {
            yield return this;
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }

    public class NeuronLayer : Collection<Neuron>
    {
        public NeuronLayer(int count)
        {
            while(count-- > 0)
            {
                Add(new Neuron());
            }
        }
    }

    public class Program
    {
        static void Main(string[] args)
        {
            var neuron1 = new Neuron();
            var neuron2 = new Neuron();

            var layer1 = new NeuronLayer(3);
            var layer2 = new NeuronLayer(4);

            neuron1.ConnectTo(neuron2);
            neuron1.ConnectTo(layer1);
            layer2.ConnectTo(neuron2);
            layer1.ConnectTo(layer2);
        }
    }
}