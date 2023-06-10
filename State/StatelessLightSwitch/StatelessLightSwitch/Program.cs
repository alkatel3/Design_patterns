using System.Runtime.CompilerServices;
using Stateless;
using Stateless.Graph;

namespace StatelessLightSwitch
{
    public enum Trigger
    {
        On, Off
    }

    public class Program
    {
        static void Main(string[] args)
        {
            // false = off, true = on

            var light = new StateMachine<bool, Trigger>(false);

            light.Configure(false)
                .Permit(Trigger.On, true)
                .OnEntry(transition =>
                {
                    if (transition.IsReentry)
                        Console.WriteLine("Light is already off!");
                    else
                        Console.WriteLine("Switching light off");
                })
                .PermitReentry(Trigger.Off);
            // .Ignore(Trigger.Off) // but if it's already off we do nothing

            // same for when the light is on
            light.Configure(true)
                .Permit(Trigger.Off, false)
                 .OnEntry(transition =>
                 {
                     Console.WriteLine("Switching light on");
                 })
                 .Ignore(Trigger.On);

            light.Fire(Trigger.On);
            light.Fire(Trigger.Off);
            light.Fire(Trigger.Off);
        }
    }
}