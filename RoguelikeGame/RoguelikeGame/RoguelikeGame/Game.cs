using RLNET;
using RoguelikeGame.Core;
using RoguelikeGame.Systrems;

namespace RoguelikeGame
{
    public static class Game
    {
        private static readonly int screenWidth = 100;
        private static readonly int screenHeight = 70;
        private static RLRootConsole rootConsole;

        private static readonly int mapWidth = 80;
        private static readonly int mapHeight = 48;
        private static RLConsole mapConsole;

        private static readonly int messageWidth = 80;
        private static readonly int messageHeight = 11;
        private static RLConsole messageConsole;

        private static readonly int statWidth = 20;
        private static readonly int statHeight = 70;
        private static RLConsole statConsole;

        private static readonly int inventoryWidth = 80;
        private static readonly int inventoryHeight = 11;
        private static RLConsole inventoryConsole;

        private static bool renderRequired = true;

        public static DungeonMap DungeonMap { get; private set; }

        public static Player Player { get; private set; }

        public static CommandSystem CommandSystem { get; private set; }

        public static void Main(string[] args)
        {
            string fontFileName = "terminal8x8.png";
            string consoleTitle = "Rugeulike Game - Level 1";

            // initialize consoles
            rootConsole = new RLRootConsole(fontFileName, screenWidth, screenHeight, 8, 8, 1f, consoleTitle);
            mapConsole = new RLConsole(mapWidth, mapHeight);
            messageConsole = new RLConsole(messageWidth, messageHeight);
            statConsole = new RLConsole(statWidth, statHeight);
            inventoryConsole = new RLConsole(inventoryWidth, inventoryHeight);

            Player = new Player();

            MapGenerator mapGenerator = new MapGenerator(mapWidth, mapHeight);
            DungeonMap = mapGenerator.CreateMap();
            DungeonMap.UpdatePlayerFieldOfView();

            CommandSystem = new CommandSystem();

            rootConsole.Update += OnRootConsoleUpdate;
            rootConsole.Render += OnRootConsoleRender;

            messageConsole.SetBackColor(0, 0, messageWidth, messageHeight, Swatch.DbDeepWater);
            messageConsole.Print(1, 1, "Messages", Colors.TextHeading);

            statConsole.SetBackColor(0, 0, statWidth, statHeight, Swatch.DbOldStone);
            statConsole.Print(1, 1, "Stats", Colors.TextHeading);

            inventoryConsole.SetBackColor(0, 0, inventoryWidth, inventoryHeight, Swatch.DbWood);
            inventoryConsole.Print(1, 1, "Inventory", Colors.TextHeading);

            rootConsole.Run();
        }

        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            bool didPlayerAct = false;
            RLKeyPress keyPress = rootConsole.Keyboard.GetKeyPress();

            if (keyPress != null)
            {
                if (keyPress.Key == RLKey.Up)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Up);
                }
                else if (keyPress.Key == RLKey.Down)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Down);
                }
                else if (keyPress.Key == RLKey.Left)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Left);
                }
                else if (keyPress.Key == RLKey.Right)
                {
                    didPlayerAct = CommandSystem.MovePlayer(Direction.Right);
                }
                else if (keyPress.Key == RLKey.Escape)
                {
                    rootConsole.Close();
                }
            }

            if (didPlayerAct)
            {
                renderRequired = true;
            }
        }

        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            if (renderRequired)
            {
                mapConsole.Clear();
                statConsole.Clear();
                messageConsole.Clear();

                // DungeonMap.Draw(mapConsole, statConsole);
                Player.Draw(mapConsole, DungeonMap);
                
                

                DungeonMap.Draw(mapConsole);
                Player.Draw(mapConsole, DungeonMap);
                RLConsole.Blit(mapConsole, 0, 0, mapWidth, mapHeight, rootConsole, 0, inventoryHeight);
                RLConsole.Blit(messageConsole, 0, 0, messageWidth, messageHeight, rootConsole, 0, screenHeight - messageHeight);
                RLConsole.Blit(statConsole, 0, 0, statWidth, statHeight, rootConsole, mapWidth, 0);
                RLConsole.Blit(inventoryConsole, 0, 0, inventoryWidth, inventoryHeight, rootConsole, 0, 0);
                rootConsole.Draw();

                renderRequired = false;
            }
        }
    }
}
