using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RLNET;

namespace Roguelike
{
    class Game
    {
        // The screen height and width are in number of tiles
        private static readonly int screenWidth = 100;
        private static readonly int screenHeight = 70;
        private static RLRootConsole rootConsole;

        //The map console takes up most of the screen and is where the map will be drawn
        private static readonly int mapWidth = 80;
        private static readonly int mapHeight = 48;
        private static RLConsole mapConsole;

        //below map console is message console
        private static readonly int messageWidth = 80;
        private static readonly int messageHeight = 11;
        private static RLConsole messageConsole;

        //Stat console is to the right of map
        private static readonly int statWidth = 20;
        private static readonly int statHeight = 70;
        private static RLConsole statConsole;

        //above the map is the inventory console which shows the player's equipment, abilities, and items
        private static readonly int inventoryWidth = 80;
        private static readonly int inventoryHeight = 11;
        private static RLConsole inventoryConsole;

        public static void Main()
        {
            // This must be the exact name of the bitmap font file we are using or it will error.
            string fontFileName = "terminal8x8.png";
            // The title will appear at the top of the console window
            string consoleTitle = "RougeSharp V3 Tutorial - Level 1";
            // Tell RLNet to use the bitmap font that we specified and that each tile is 8 x 8 pixels
            rootConsole = new RLRootConsole(fontFileName, screenWidth, screenHeight,
              8, 8, 1f, consoleTitle);
            
            //initialize the sub consoles that we will Blit to the root console
            mapConsole = new RLConsole(mapWidth, mapHeight);
            messageConsole = new RLConsole(messageWidth, messageHeight);
            statConsole = new RLConsole(statWidth, statHeight);
            inventoryConsole = new RLConsole(inventoryWidth, inventoryHeight);

            // Set up a handler for RLNET's Update event
            rootConsole.Update += OnRootConsoleUpdate;
            // Set up a handler for RLNET's Render event
            rootConsole.Render += OnRootConsoleRender;
            // Begin RLNET's game loop
            rootConsole.Run();
        }

        // Event handler for RLNET's Update event
        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            //set background color and text for each console
            //so that we can verify that they are in the correct position
            mapConsole.SetBackColor(0, 0, mapWidth, mapHeight, RLColor.Black);
            mapConsole.Print(1, 1, "Map", RLColor.White);

            messageConsole.SetBackColor(0, 0, messageWidth, messageHeight, RLColor.Gray);
            messageConsole.Print(1, 1, "Messages", RLColor.White);

            statConsole.SetBackColor(0, 0, statWidth, statHeight, RLColor.Brown);
            statConsole.Print(1, 1, "Stats", RLColor.White);

            inventoryConsole.SetBackColor(0, 0, inventoryWidth, inventoryHeight, RLColor.Cyan);
            inventoryConsole.Print(1, 1, "Inventory", RLColor.White);
        }

        // Event handler for RLNET's Render event
        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            //Blit the sub consoles to the root console in the correct locations
            RLConsole.Blit(mapConsole, 0, 0, mapWidth, mapHeight, rootConsole, 0, inventoryHeight);
            RLConsole.Blit(statConsole, 0, 0, statWidth, statHeight, rootConsole, mapWidth, 0);
            RLConsole.Blit(messageConsole, 0, 0, messageWidth, messageHeight, rootConsole, 0, screenHeight - messageHeight);
            RLConsole.Blit(inventoryConsole, 0, 0, inventoryWidth, inventoryHeight, rootConsole, 0, 0);

            // Tell RLNET to draw the console that we set
            rootConsole.Draw();
        }
    }
}
