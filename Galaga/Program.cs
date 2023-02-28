using System;
using DIKUArcade.GUI;

namespace Galaga
{
    class Program
    {
        static void Main(string[] args) //hej 
        {
            var windowArgs = new WindowArgs() { Title = "Galaga v0.1" };
            var game = new Game(windowArgs);
            //something's inn the waaaaaaaaaaay
            game.Run();
            //Console.WriteLine("Hello World!"); // TODO: Delete this line!
            // Hej!!
        }
    }
}
