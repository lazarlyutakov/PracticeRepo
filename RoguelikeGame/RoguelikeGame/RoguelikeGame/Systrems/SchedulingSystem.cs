using RoguelikeGame.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeGame.Systrems
{
    public class SchedulingSystem
    {
        private int _time;
        private readonly SortedDictionary<int, List<ISchedulable>> _schedulables;

        public SchedulingSystem()
        {
            _time = 0;
            _schedulables = new SortedDictionary<int, List<ISchedulable>>();
        }

        public void Add(ISchedulable schedulable)
        {
            int key = _time + schedulable.Time;

            if (!_schedulables.ContainsKey(key))
            {
                _schedulables.Add(key, new List<ISchedulable>());
            }

            _schedulables[key].Add(schedulable);
        }

        public void Remove(ISchedulable schedulable)
        {
            KeyValuePair<int, List<ISchedulable>> schedulableListFound = new KeyValuePair<int, List<ISchedulable>>(-1, null);

            foreach (var schedulableList in _schedulables)
            {
                if (schedulableList.Value.Contains(schedulable))
                {
                    schedulableListFound = schedulableList;
                    break;
                }
            }

            if (schedulableListFound.Value != null)
            {
                schedulableListFound.Value.Remove(schedulable);

                if (schedulableListFound.Value.Count <= 0)
                {
                    _schedulables.Remove(schedulableListFound.Key);
                }
            }
        }

        public ISchedulable Get()
        {
            var firstSchedulableGroup = _schedulables.First();
            var firstScheduleable = firstSchedulableGroup.Value.First();
            Remove(firstScheduleable);
            _time = firstSchedulableGroup.Key;

            return firstScheduleable;
        }

        public int GetTime()
        {
            return _time;
        }

        public void Clear()
        {
            _time = 0;
            _schedulables.Clear();
        }
    }
}
