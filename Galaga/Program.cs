using System;
using DIKUArcade.GUI;
using Galaga.GalagaStates;
namespace Galaga
{
    class Program
    {
        static void Main(string[] args)
        {
            var windowArgs = new WindowArgs() { Title = "Galaga v0.1" };
            var game = new StateMachine(windowArgs);
            game.Run();
        }
    }
}
