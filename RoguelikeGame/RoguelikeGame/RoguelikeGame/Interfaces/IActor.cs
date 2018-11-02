using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeGame.Interfaces
{
    public interface IActor
    {
        string Name { get; set; }
        int Awarness { get; set; }
    }
}
