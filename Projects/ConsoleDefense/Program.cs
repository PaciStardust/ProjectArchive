using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ProjectArchive.Projects.ConsoleDefense
{
    [Project("Console Defense", 1631656800, 1631829600, "Prototype for a console based TD game - WASD to move, ENTER to build, ESC to play round")]
    internal class Program : IProject
    {
        public void Run()
        {
            //Graphics settings
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.CursorVisible = false;
            Console.SetWindowSize(110, 41);
            Console.Title = "ConsoleDefense - Console-Based Tower Defense"; //TODO SET NAME DURING GAME
            Graphics.Start(40, 110);

            Game test = new Game(ListMaps.TestMap);
            test.Start();
        }
    }

    //TODO:
    //
    //TOWERS - generators? Actual gameplay towers and integration, upgrades? 
    //ENEMIES - Basic, Splitters, Slow but strong, Fast but weak, bosses?
    //DAMAGE - Damage Types
    //MONEY - Money System
    //WAVES - PROGRESSION
    //TILES - Visual different tiles, splitters
}
