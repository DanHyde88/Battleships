using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Battleships
{
    // The board will be a rectangular array 10 x 10.
    public class Board
    {
        // Properties. Change size of board by changing rectangular array size here
        private static int boardSize = 10;
        public int[,] UserDisplay = new int[boardSize, boardSize];
        public int[,] CompDisplay = new int[boardSize, boardSize];

        // Constructor
        public Board()
        {
            UserPopulateDisplay();
            CompPopulateDisplay();
        }

        // Populates the user's display. To change the amount of each type of ship change the value of amountOfShips for each ship type
        public void UserPopulateDisplay()
        {
            int amountOfShips;
            foreach (Ship ship in Enum.GetValues(typeof(Ship)))
            {
                switch (ship)
                {
                    case Ship.Destroyer:
                        amountOfShips = 4;
                        SolicitPlayer(ship, amountOfShips);
                        break;

                    case Ship.Submarine:
                        amountOfShips = 3;
                        SolicitPlayer(ship, amountOfShips);
                        break;

                    case Ship.Battleship:
                        amountOfShips = 2;
                        SolicitPlayer(ship, amountOfShips);
                        break;

                    case Ship.Carrier:
                        amountOfShips = 1;
                        SolicitPlayer(ship, amountOfShips);
                        break;
                }
            }
        }

        // Populates the user's display. To change the amount of each type of ship change the value of amountOfShips for each ship type
        public void CompPopulateDisplay()
        {
            int amountOfShips;
            foreach (Ship ship in Enum.GetValues(typeof(Ship)))
            {
                switch (ship)
                {
                    case Ship.Destroyer:
                        amountOfShips = 4;
                        CompPositionGenerator(ship, amountOfShips);
                        break;

                    case Ship.Submarine:
                        amountOfShips = 3;
                        CompPositionGenerator(ship, amountOfShips);
                        break;

                    case Ship.Battleship:
                        amountOfShips = 2;
                        CompPositionGenerator(ship, amountOfShips);
                        break;

                    case Ship.Carrier:
                        amountOfShips = 1;
                        CompPositionGenerator(ship, amountOfShips);
                        break;
                }
            }
        }

        // Takes the initial position and direction for each ship
        public void SolicitPlayer(Ship ship, int amountOfShips)
        {
            for (int i = 0; i < amountOfShips; i++)
            {
                ShowDisplay();
                Console.WriteLine($"Please pick initialising position for {ship}, number {i + 1} of {amountOfShips}. {ship}s are {(int)ship} long.");
                int[] initialPosition = PlacePieceWhere();

                Console.WriteLine("Which direction do you want the rest of the ship to extend in from starting position: Up(0), Right(1), Down(2), Left(3)?");
                int direction = Convert.ToInt32(Console.ReadLine());
                if (ShipPositionCheck(ship, initialPosition[0], initialPosition[1], direction, false))
                {
                    ShipPositionLoop(ship, initialPosition[0], initialPosition[1], direction, false);
                }
                else
                {
                    Console.WriteLine("THE SHIP CANNOT BE PLACED. PLEASE TRY AGAIN!"); //Need to improve gameplay here so it doesn't just end game.
                    i -= 1; //Have to redo this ship as it failed if code reaches here.
                }
            }
        }

        // Takes the initial position and direction for each ship
        public void CompPositionGenerator(Ship ship, int amountOfShips)
        {
            for (int i = 0; i < amountOfShips; i++)
            {
                Random rnd = new Random();
                int randomNumberX = rnd.Next(0, 10);
                int randomNumberY = rnd.Next(0, 10);
                int randomNumberDirection = rnd.Next(0, 10);
                if (ShipPositionCheck(ship, randomNumberX, randomNumberY, randomNumberDirection, true))
                {
                    ShipPositionLoop(ship, randomNumberX, randomNumberY, randomNumberDirection, true);
                }
                else
                {
                    i -= 1;
                }
            }
        }

        // Solicits x y coordinates to place a piece of a ship and then returns a position array
        public int[] PlacePieceWhere()
        {
            Console.WriteLine("Enter the x coordinate");
            int x = Convert.ToInt32(Console.ReadLine());
            Console.WriteLine("Enter the y coordinate");
            int y = Convert.ToInt32(Console.ReadLine());
            // x and y flipped on purpose so reads like cartesian coordinates
            return new[] { x, y };
        }

        // Checks all the pieces of the ships move to check it's legal, i.e. on the board and not already taken
        public bool ShipPositionCheck(Ship ship, int x, int y, int direction, bool comp)
        {
            int[,] display;
            if (comp)
            {
                display = CompDisplay;
            }
            else
            {
                display = UserDisplay;
            }
            int pieces = (int)ship;
            switch (direction)
            {
                case 0: // Up
                    for (int j = y; j > y - pieces; j--)
                    {
                         if (x > boardSize - 1 || j > boardSize - 1 || x < 0 || j < 0 || display[j, x] == 1) // Long winded way of checking that the possible pieces don't go off the board or over another ship
                        {
                            return false;
                        }
                    }
                    return true;

                case 1: // Right
                    for (int i = x; i < x + pieces; i++)
                    {
                        if (i > boardSize - 1 || y > boardSize - 1 || i < 0 || y < 0 || display[y, i] == 1)
                        {
                            return false;
                        }
                    }
                    return true;

                case 2: // Down
                    for (int j = y; j < y + pieces; j++)
                    {
                        if (x > boardSize - 1 || j > boardSize - 1 || x < 0 || j < 0 || display[j, x] == 1)
                        {
                            return false;
                        }
                    }
                    return true;

                case 3: // Left
                    for (int i = x; i > x - pieces; i--)
                    {
                        if (i > boardSize - 1 || y > boardSize - 1 || i < 0 || y < 0 || display[y, i] == 1)
                        {
                            return false;
                        }
                    }
                    return true;
                default:
                    return false;
            }
        }

        // Places the ship piece in the display
        public void ShipPositionLoop(Ship ship, int x, int y, int direction, bool comp)
        {
            int[,] display;
            if (comp)
            {
                display = CompDisplay;
            }
            else
            {
                display = UserDisplay;
            }
            int pieces = (int)ship;
            switch (direction)
            {
                case 0: // Up
                    for (int j = y; j > y - pieces; j--)
                    {
                        display[j, x] = 1;
                    }
                    break;

                case 1: // Right
                    for (int i = x; i < x + pieces; i++)
                    {
                        display[y, i] = 1;
                    }
                    break;

                case 2: // Down
                    for (int j = y; j < y + pieces; j++)
                    {
                        display[j, x] = 1;
                    }
                    break;

                case 3: // Left
                    for (int i = x; i > x - pieces; i--)
                    {
                        display[y, i] = 1;
                    }
                    break;
            }
        }

        // Shows the display on the console by looping through Display
        public void ShowDisplay()
        {
            Console.WriteLine("  0123456789");
            Console.WriteLine("  ||||||||||");
            for (int i=0; i < UserDisplay.GetLength(0); i++)
            {
                Console.Write(i + "-");
                for (int j = 0; j < UserDisplay.GetLength(1); j++)
                {
                    if (UserDisplay[i,j] == 1)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write("0");
                    }
                }
                Console.WriteLine("");
            }
        }

        // Shows the display on the console by looping through Display
        public void ShowCompDisplay()
        {
            Console.WriteLine("  0123456789");
            Console.WriteLine("  ||||||||||");
            for (int i = 0; i < CompDisplay.GetLength(0); i++)
            {
                Console.Write(i + "-");
                for (int j = 0; j < CompDisplay.GetLength(1); j++)
                {
                    if (CompDisplay[i, j] == 1)
                    {
                        Console.Write("X");
                    }
                    else
                    {
                        Console.Write("0");
                    }
                }
                Console.WriteLine("");
            }
        }
    }

    // Enumerator with the length of each type of ship
    public enum Ship
    {
        Destroyer = 2,
        Submarine,
        Battleship,
        Carrier
    }
}
