using RoguelikeGame.Core;
using RoguelikeGame.Monsters;
using RogueSharp;
using RogueSharp.DiceNotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeGame.Systrems
{
    public class MapGenerator
    {
        private readonly int _width;
        private readonly int _heght;
        private readonly int _maxRooms;
        private readonly int _roomMaxSize;
        private readonly int _roomMinSize;

        private readonly DungeonMap _map;

        public MapGenerator(int width, int height, int maxRooms, int roomMaxSize, int roomMinSize, int mapLevel)
        {
            _width = width;
            _heght = height;
            _maxRooms = maxRooms;
            _roomMaxSize = roomMaxSize;
            _roomMinSize = roomMinSize;
            _map = new DungeonMap();
        }

        public DungeonMap CreateMap()
        {
            _map.Initialize(_width, _heght);

            for (int r = _maxRooms; r > 0; r--)
            {
                int roomWidth = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomHeight = Game.Random.Next(_roomMinSize, _roomMaxSize);
                int roomXPosition = Game.Random.Next(0, _width - roomWidth - 1);
                int roomYPosition = Game.Random.Next(0, _heght - roomHeight - 1);

                var newRoom = new Rectangle(roomXPosition, roomYPosition, roomWidth, roomHeight);

                bool newRoomIntersects = _map.Rooms.Any(room => newRoom.Intersects(room));

                if (!newRoomIntersects)
                {
                    _map.Rooms.Add(newRoom);
                }
            }

            for (int r = 1; r < _map.Rooms.Count; r++)
            {
                int previousRoomCenterX = _map.Rooms[r - 1].Center.X;
                int previousRoomCenterY = _map.Rooms[r - 1].Center.Y;
                int currentRoomCenterX = _map.Rooms[r].Center.X;
                int currentRoomCenterY = _map.Rooms[r].Center.Y;

                if (Game.Random.Next(1,2) == 1)
                {
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                }
                else
                {
                    CreateVerticalTunnel(previousRoomCenterY, currentRoomCenterY, previousRoomCenterX);
                    CreateHorizontalTunnel(previousRoomCenterX, currentRoomCenterX, previousRoomCenterY);
                }
            }

            foreach (var room in _map.Rooms)
            {
                CreateRoom(room);
                CreateDoors(room);
            }

            CreateStairs();

            PlacePlayer();
            PlaceMonster();

            return _map;
        }

        public void CreateRoom(Rectangle room)
        {
            for (int x = room.Left + 1; x < room.Right; x++)
            {
                for (int y = room.Top + 1; y < room.Bottom; y++)
                {
                    _map.SetCellProperties(x, y, true, true);
                }
            }
        }

        private void PlacePlayer()
        {
            Player player = Game.Player;

            if (player == null)
            {
                player = new Player();
            }

            player.X = _map.Rooms[0].Center.X;
            player.Y = _map.Rooms[0].Center.Y;

            _map.AddPlayer(player);
        }

        private void PlaceMonster()
        {
            foreach (var room in _map.Rooms)
            {
                if (Dice.Roll("1D10") < 7)
                {
                    var numberOfMonsters = Dice.Roll("1D4");

                    for (int i = 0; i < numberOfMonsters; i++)
                    {
                        Point randomRoomLocation = (Point)_map.GetRandomWalkableLocationInRoom(room);
                        Point randomRoomLocation2 = (Point)_map.GetRandomWalkableLocationInRoom(room);

                        if (randomRoomLocation != null)
                        {
                            var monster = Kobold.Create(1);
                            var dzveroMonster = Dzvero.Create(2);

                            monster.X = randomRoomLocation.X;
                            monster.Y = randomRoomLocation.Y;

                             dzveroMonster.X = randomRoomLocation2.X;
                             dzveroMonster.Y = randomRoomLocation2.Y;

                            _map.AddMonster(monster);
                            _map.AddMonster(dzveroMonster);

                        }
                    }
                }
            }
        }

        private void CreateHorizontalTunnel(int xStart, int xEnd, int yPosition)
        {
            for (int x = Math.Min(xStart, xEnd); x <= Math.Max(xStart, xEnd); x++)
            {
                _map.SetCellProperties(x, yPosition, true, true);
            }
        }

        private void CreateVerticalTunnel(int yStart, int yEnd, int xPosition)
        {
            for (int y = Math.Min(yStart, yEnd); y <= Math.Max(yStart, yEnd); y++)
            {
                _map.SetCellProperties(xPosition, y, true, true);
            }
        }

        private void CreateDoors(Rectangle room)
        {
            int xMin = room.Left;
            int xMax = room.Right;
            int yMin = room.Top;
            int yMax = room.Bottom;

            List<ICell> borderCell = _map.GetCellsAlongLine(xMin, yMin, xMax, yMax).ToList();
            borderCell.AddRange(_map.GetCellsAlongLine(xMin, yMin, xMin, yMax));
            borderCell.AddRange(_map.GetCellsAlongLine(xMin, yMax, xMax, yMax));
            borderCell.AddRange(_map.GetCellsAlongLine(xMax, yMin, xMax, yMax));

            foreach (var cell in borderCell)
            {
                if (IsPotentialDoor(cell))
                {
                    _map.SetCellProperties(cell.X, cell.Y, false, true);
                    _map.Doors.Add(new Door { X = cell.X, Y = cell.Y, IsOpen = false });
                }
            }
        }

        private void CreateStairs()
        {
            _map.StairsUp = new Stairs
            {
                X = _map.Rooms.First().Center.X + 1,
                Y = _map.Rooms.First().Center.Y,
                IsUp = true
            };

            _map.StairsDown = new Stairs
            {
                X = _map.Rooms.Last().Center.X,
                Y = _map.Rooms.Last().Center.Y,
                IsUp = false
            };
        }
        private bool IsPotentialDoor(ICell cell)
        {
            if (!cell.IsWalkable)
            {
                return false;
            }

            var right = _map.GetCell(cell.X + 1, cell.Y);
            var left = _map.GetCell(cell.X - 1, cell.Y);
            var top = _map.GetCell(cell.X, cell.Y - 1);
            var bottom = _map.GetCell(cell.X, cell.Y + 1);

            if (_map.GetDoor(cell.X, cell.Y) !=  null || _map.GetDoor(right.X, right.Y) != null || _map.GetDoor(left.X, left.Y) != null || _map.GetDoor(top.X, top.Y) != null || _map.GetDoor(bottom.X, bottom.Y) != null)
            {
                return false;
            }

            if (right.IsWalkable && left.IsWalkable && !top.IsWalkable && !bottom.IsWalkable)
            {
                return true;
            }

            if (!right.IsWalkable && !left.IsWalkable && top.IsWalkable && bottom.IsWalkable)
            {
                return true;
            }

            return false;
        }
    }
}
