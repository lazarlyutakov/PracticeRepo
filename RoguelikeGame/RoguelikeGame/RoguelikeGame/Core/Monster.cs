using RLNET;
using RoguelikeGame.Behaviours;
using RoguelikeGame.Systrems;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeGame.Core
{
    public class Monster : Actor
    {
        public int? TurnsAlerted { get; set; }

        public void DrawStats(RLConsole statConsole, int position)
        {
            int yPosition = 13 + (position * 2);
            statConsole.Print(1, yPosition, Symbol.ToString(), Color);

            int width = Convert.ToInt32(((double)Health / (double)MaxHealth) * 16.0);
            int remainingWidth = 16 - width;

            statConsole.SetBackColor(3, yPosition, width, 1, Swatch.Primary);
            statConsole.SetBackColor(3 + width, yPosition, remainingWidth, 1, Swatch.PrimaryDarkest);

            statConsole.Print(2, yPosition, $": {Name}", Swatch.DbLight);
        }

        public virtual void PerformAction(CommandSystem commandSystem)
        {
            var behaviour = new StandartMoveAndAttack();
            behaviour.Act(this, commandSystem);
        }
    }
}
