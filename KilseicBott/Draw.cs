using System.Collections.Generic;
using System.Linq;
using BotInterface.Game;

namespace KilseicBott
{
    public class Draw
    {
        public bool FirstDraw;
        public Dictionary<string,int> DrawCounter;
        public int DrawStreak;
        public int DrawUpdate;
        public RandomMoves RandomInstance;

        public Draw()
        {
            FirstDraw = true; 
            DrawCounter = new Dictionary<string,int>(){{"RPS", 0},{"Dynamite", 0},{"Water", 0}}; 
            DrawStreak = 0; 
            DrawUpdate = 0; 
            RandomInstance = new RandomMoves();
        }
        public void UpdateDrawCounter(List<Round> allRounds, int enemyDynSticks)
        {
            DrawUpdate++;
            if (DrawUpdate == 15)
            {
                DrawCounter = new Dictionary<string,int>(){{"RPS", 0},{"Dynamite", 0},{"Water", 0}};
                DrawUpdate = 0;
            }
            switch (allRounds.Last().GetP2())
            {
                case Move.D:
                    DrawCounter["Dynamite"]++;
                    break;
                case Move.W:
                    DrawCounter["Water"]++;
                    break;
                default:
                    DrawCounter["RPS"]++;
                    break;
            }
            if (enemyDynSticks < 5)
                DrawCounter["Dynamite"] = 0;
            FirstDraw = false;
        }
        public Move DrawScenario(int sticksODynamite, int enemyDynSticks)
            {
                DrawStreak++;
                if (FirstDraw)
                    FirstDraw = false;
                string highestChanceMove = DrawCounter.Aggregate((x,
                    y) => x.Value > y.Value ? x : y).Key;
                bool counterValid = (DrawCounter.Values.Count(x => x.Equals
                    ( DrawCounter.Values.Max())) != 5);
                if (sticksODynamite == 0)
                {
                    return NoDynamiteLeft(highestChanceMove, counterValid, enemyDynSticks);
                }
                if (DrawStreak == 4)
                {
                    return Move.D;
                }
                if (!counterValid | enemyDynSticks == 0)
                    return RandomInstance.DynamiteChanceMove(RandomInstance.RandomMove(),1,4);
                if ("Water" == highestChanceMove)
                    return RandomInstance.DynamiteChanceMove(RandomInstance.RandomMove(),1,10);
                if ("Dynamite" == highestChanceMove)
                    return RandomInstance.DynamiteChanceMove(RandomInstance.RandomMove(includeWater: true),1,4);
                return RandomInstance.DynamiteChanceMove(RandomInstance.RandomMove(),1,5);
            }
            
            private Move NoDynamiteLeft(string highestChanceMove, bool counterValid, int enemyDynSticks)
            {
                if (enemyDynSticks > 5 & "Dynamite" == highestChanceMove & counterValid)
                {
                    return RandomInstance.RandomMove(includeWater: true);
                }
                return RandomInstance.RandomMove();
            }
    }
}