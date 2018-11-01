using RoguelikeGame.Core;
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

        private readonly DungeonMap _map;

        public MapGenerator(int width, int height)
        {
            _width = width;
            _heght = height;
            _map = new DungeonMap();
        }

        public DungeonMap CreateMap()
        {
            _map.Initialize(_width, _heght);

            foreach (var cell in _map.GetAllCells())
            {
                _map.SetCellProperties(cell.X, cell.Y, true, true, true);
            }

            foreach (var cell in _map.GetCellsInRows(0, _heght - 1))
            {
                _map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            foreach (var cell in _map.GetCellsInColumns(0, _width - 1))
            {
                _map.SetCellProperties(cell.X, cell.Y, false, false, true);
            }

            return _map;
        }
    }
}
