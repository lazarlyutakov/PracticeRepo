using RoguelikeGame.Core;
using RogueSharp.DiceNotation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RoguelikeGame.Monsters
{
   public class Dzvero : Monster
    {
        public static Dzvero Create(int level)
        {
            int health = Dice.Roll("2D5");
            return new Dzvero
            {
                Attack = Dice.Roll("1D3") + level / 4,
                AttackChance = Dice.Roll("25D3"),
                Awarness = 5,
                Color = Colors.DzveroColor,
                Defense = Dice.Roll("1D3") + level / 4,
                DefenseChance = Dice.Roll("10D4"),
                Gold = Dice.Roll("5D5"),
                Health = health,
                MaxHealth = health,
                Name = "Dzvero",
                Speed = 11,
                Symbol = 'D'
            };
        }
    }
}
