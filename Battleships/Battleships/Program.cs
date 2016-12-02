using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    class Program
    {
        static void Main(string[] args)
        {
            Board first = new Battleships.Board();
            first.ShowDisplay();
            // This is how to get the int value from an enum. Use a cast from enum to int.
            //Console.WriteLine((int)Ship.Destroyer);
            first.ShowCompDisplay();
        }
    }
}
