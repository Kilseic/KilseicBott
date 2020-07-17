using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading;
using BotInterface.Bot;
using BotInterface.Game;

namespace KilseicBott
{
    public class KilseicBott : IBot

    {
        //private int MyScore;
        //private int RoundCounter; 
        //private int MatchLength;
        private Draw DrawTools; 
        private int SticksODynamite; 
        private int EnemyDynSticks;
        private RandomMoves RandomInstance;
        
        public KilseicBott()
        {
            //MyScore = 0;
            //RoundCounter = 0; 
            //MatchLength = 2000; 
            DrawTools = new Draw();
            SticksODynamite = 97; 
            EnemyDynSticks = 100;
            RandomInstance = new RandomMoves();
        }
        public Move MakeMove(Gamestate gamestate) 
        { 
            /*RoundCounter++;
            if (RoundCounter % 100 == 0) 
                UpdateMatchLength();*/
            if (gamestate.GetRounds().Length == 0)
                return Move.R;
            List<Round> allRounds = gamestate.GetRounds().ToList();
            if (allRounds[allRounds.Count - 1].GetP2() == Move.D)
                EnemyDynSticks--;
            if (allRounds.Count > 1 & !DrawTools.FirstDraw)
                if (allRounds[allRounds.Count - 2].GetP1() == allRounds[allRounds.Count - 2].GetP2())
                    DrawTools.UpdateDrawCounter(allRounds, EnemyDynSticks);
            if (allRounds[allRounds.Count - 1].GetP1() == allRounds[allRounds.Count - 1].GetP2())
            {
                Move nextMove = DrawTools.DrawScenario(SticksODynamite,EnemyDynSticks);
                if (nextMove == Move.D)
                    SticksODynamite--;
                return nextMove;
            }
            DrawTools.DrawStreak = 0;
            DrawTools.FirstDraw = true;
            return RandomInstance.RandomMove();
        }
    }
}
