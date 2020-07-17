using System;
using BotInterface.Game;
using NUnit.Framework;

namespace BotTester
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            KilseicBott.KilseicBott pleaseWork = new KilseicBott.KilseicBott();
            Move hi = pleaseWork.MakeMove(new Gamestate());
            Console.WriteLine(hi.ToString());
            Assert.Pass();
        }
    }
}