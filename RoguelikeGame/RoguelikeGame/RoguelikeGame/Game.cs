﻿using RLNET;
using RoguelikeGame.Core;
using RoguelikeGame.Systrems;
using RogueSharp.Random;
using System;

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

        private static int _mapLevel = 1;

        private static bool renderRequired = true;

        public static DungeonMap DungeonMap { get; private set; }

        public static Player Player { get; set; }

        public static CommandSystem CommandSystem { get; private set; }

        public static MessageLog MessageLog { get; private set; }

        public static IRandom Random { get; private set; }
        public static SchedulingSystem SchedulingSystem { get; private set; }

        public static void Main(string[] args)
        {
            int seed = (int)DateTime.UtcNow.Ticks;
            Random = new DotNetRandom(seed);

            string consoleTitle = $"RogueSharp V3 Tutorial - Level 1 - Seed {seed}";

            string fontFileName = "terminal8x8.png";

            MessageLog = new MessageLog();
            MessageLog.Add("The rogue arrives at level 1");
            MessageLog.Add($"Level created with seed {seed}");

            // initialize consoles
            rootConsole = new RLRootConsole(fontFileName, screenWidth, screenHeight, 8, 8, 1f, consoleTitle);
            mapConsole = new RLConsole(mapWidth, mapHeight);
            messageConsole = new RLConsole(messageWidth, messageHeight);
            statConsole = new RLConsole(statWidth, statHeight);
            inventoryConsole = new RLConsole(inventoryWidth, inventoryHeight);

            string consoleTitile = $"RougeSharp V3 Tutorial - Level {_mapLevel} - Seed {seed}";

            SchedulingSystem = new SchedulingSystem();

            MapGenerator mapGenerator = new MapGenerator(mapWidth, mapHeight, 20, 13, 7, _mapLevel);
            DungeonMap = mapGenerator.CreateMap();
            DungeonMap.UpdatePlayerFieldOfView();

            CommandSystem = new CommandSystem();

            rootConsole.Update += OnRootConsoleUpdate;
            rootConsole.Render += OnRootConsoleRender;

            inventoryConsole.SetBackColor(0, 0, inventoryWidth, inventoryHeight, Swatch.DbWood);
            inventoryConsole.Print(1, 1, "Inventory", Colors.TextHeading);

            rootConsole.Run();
        }

        private static void OnRootConsoleUpdate(object sender, UpdateEventArgs e)
        {
            bool didPlayerAct = false;
            RLKeyPress keyPress = rootConsole.Keyboard.GetKeyPress();

            if (CommandSystem.IsPlayerTurn)
            {
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
                    else if (keyPress.Key == RLKey.Period)
                    {
                        if (DungeonMap.CanMoveDownToNextLevel())
                        {
                            MapGenerator mapGenerator = new MapGenerator(mapWidth, mapHeight, 20, 13, 7, _mapLevel);
                            DungeonMap = mapGenerator.CreateMap();
                            MessageLog = new MessageLog();
                            CommandSystem = new CommandSystem();
                            rootConsole.Title = $"RogueSharp RLNet Tutorial - Level {_mapLevel}";
                            didPlayerAct = true;
                        }
                    }
                }
                if (didPlayerAct)
                {
                    renderRequired = true;
                    CommandSystem.EndPLayerTurn();
                }
            }
            else
            {
                CommandSystem.ActivateMonsters();
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

                DungeonMap.Draw(mapConsole, statConsole);               
                Player.Draw(mapConsole, DungeonMap);
                MessageLog.Draw(messageConsole);
                Player.Draw(mapConsole, DungeonMap);
                Player.DrawStats(statConsole);

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
