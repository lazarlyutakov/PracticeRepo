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

        private static readonly int messageWidt = 80;
        private static readonly int messageHeight = 11;
        private static RLConsole messageConsole;

        private static readonly int statWidth = 20;
        private static readonly int statHeight = 70;
        private static RLConsole statConsole;

        private static readonly int inventoryWidth = 80;
        private static readonly int inventoryHeioght = 11;
        private static RLConsole inventoryConsole;

        public static DungeonMap DungeonMap { get; private set; }

        public static Player Player { get; private set; }

        public static void Main(string[] args)
        {
            string fontFileName = "terminal8x8.png";
            string consoleTitle = "Rugeulike Game - Level 1";

            // initialize consoles
            rootConsole = new RLRootConsole(fontFileName, screenWidth, screenHeight, 8, 8, 1f, consoleTitle);
            mapConsole = new RLConsole(mapWidth, mapHeight);
            messageConsole = new RLConsole(messageWidt, messageHeight);
            statConsole = new RLConsole(statWidth, statHeight);
            inventoryConsole = new RLConsole(inventoryWidth, inventoryHeioght);

            Player = new Player();

            MapGenerator mapGenerator = new MapGenerator(mapWidth, mapHeight);
            DungeonMap = mapGenerator.CreateMap();
            DungeonMap.UpdatePlayerFieldOfView();
            
            rootConsole.Update += OnRootConsoleUpdate;
            rootConsole.Render += OnRootConsoleRender;
            rootConsole.Run();
        }

        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            mapConsole.SetBackColor(0, 0, mapWidth, mapHeight, Colors.FloorBackground);
            mapConsole.Print(1, 1, "Map", Colors.TextHeading);

            messageConsole.SetBackColor(0, 0, messageWidt, messageHeight, Swatch.DbDeepWater);
            messageConsole.Print(1, 1, "Messages", Colors.TextHeading);

            statConsole.SetBackColor(0, 0, statWidth, statHeight, Swatch.DbOldStone);
            statConsole.Print(1, 1, "Stats", Colors.TextHeading);

            inventoryConsole.SetBackColor(0, 0, inventoryWidth, inventoryHeioght, Swatch.DbWood);
            inventoryConsole.Print(1, 1, "Inventory", Colors.TextHeading);
        }

        private static void OnRootConsoleRender(object sender, UpdateEventArgs e)
        {
            DungeonMap.Draw(mapConsole);
            Player.Draw(mapConsole, DungeonMap);

            RLConsole.Blit(mapConsole, 0, 0, mapWidth, mapHeight, rootConsole, 0, inventoryHeioght);
            RLConsole.Blit(statConsole, 0, 0, statWidth, statHeight, rootConsole, mapWidth, 0);
            RLConsole.Blit(messageConsole, 0, 0, messageWidt, messageHeight, rootConsole,0, screenHeight - messageHeight);
            RLConsole.Blit(inventoryConsole, 0, 0, inventoryWidth, inventoryHeioght, rootConsole, 0, 0);

            rootConsole.Draw();
        }
    }
}
