using System;
using System.Collections.Generic;
using System.Linq;
using BotInterface.Game;

namespace KilseicBott
{
    public class RandomMoves
    {
        public Random RandomInstance;

        public RandomMoves()
        {
            RandomInstance = new Random();
        }
        public Move RandomMove(bool includeWater = false)
        {
            int number = RandomInstance.Next(3);
            if (number == 0 & includeWater)
                return Move.W;
            if (number == 0)
                return Move.S;
            if (number == 1)
                return Move.R;
            return Move.P;
        }
        public Move DynamiteChanceMove(Move inputMove, int chanceNumerator, int chanceDenominator)
        {
            int randNumber = RandomInstance.Next(chanceDenominator);
            List<int> numberList = Enumerable.Range(0, chanceNumerator).ToList();
            if (numberList.Contains(randNumber))
            {
                return Move.D;
            }
            return inputMove;
        }
    }
}