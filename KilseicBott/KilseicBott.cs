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
        private Draw DrawTools; 
        private int SticksODynamite; 
        private int EnemyDynSticks;
        private RandomMoves RandomInstance;
        private int N;
        private List<Move> LastNMoves;
        
        public KilseicBott()
        {
            DrawTools = new Draw();
            SticksODynamite = 97; 
            EnemyDynSticks = 100;
            RandomInstance = new RandomMoves();
            LastNMoves = new List<Move>();
            N = 4;
        }
        public Move MakeMove(Gamestate gamestate)
        {
            if (gamestate.GetRounds().Length == 0)
                return Move.R;
            List<Round> allRounds = gamestate.GetRounds().ToList();
            UpdateLastNMoves(allRounds);
            if (allRounds.Last().GetP2() == Move.D)
                EnemyDynSticks--;
            if (LastNMoves.All(o => o == LastNMoves[0]) & LastNMoves.Count >= N)
                return CounterMove(LastNMoves[0]);
            if (RandomInstance.RandomInstance.Next(100) == 0)
                N = RandomInstance.RandomInstance.Next(3, 6);
            if (allRounds.Count > 1 & !DrawTools.FirstDraw)
                if (allRounds[allRounds.Count - 2].GetP1() == allRounds[allRounds.Count - 2].GetP2())
                    DrawTools.UpdateDrawCounter(allRounds, EnemyDynSticks);
            Move nextMove;
            if (allRounds.Last().GetP1() == allRounds.Last().GetP2())
            {
                nextMove = DrawTools.DrawScenario(SticksODynamite,EnemyDynSticks);
            }
            else
            {
                DrawTools.DrawStreak = 0;
                DrawTools.FirstDraw = true;
                if (SticksODynamite > 0)
                    nextMove = RandomInstance.DynamiteChanceMove(RandomInstance.RandomMove(),1,100);
                else
                    nextMove = RandomInstance.RandomMove();
            }
            if (nextMove == Move.D)
                SticksODynamite--;
            return nextMove;
        }

        private void UpdateLastNMoves(List<Round> allRounds)
        {
            while (LastNMoves.Count+1 > N)
                LastNMoves.RemoveAt(0);
            LastNMoves.Add(allRounds.Last().GetP2());
        }

        private Move CounterMove(Move inputMove)
        {
            switch (inputMove)
            {
                case Move.D:
                    return Move.W;
                case Move.P:
                    return Move.S;
                case Move.R:
                    return Move.P;
                case Move.S:
                    return Move.R;
                default:
                    return RandomInstance.RandomMove();
            }
        }
    }
}
