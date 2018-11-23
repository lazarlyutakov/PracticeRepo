using RoguelikeGame.Core;
using RoguelikeGame.Systrems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeGame.Interfaces
{
    public interface IBehaviour
    {
        bool Act(Monster monster, CommandSystem commandSystem);
    }
}
