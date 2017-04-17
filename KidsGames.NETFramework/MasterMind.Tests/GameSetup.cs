using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MasterMind.Game;

namespace MasterMind.Tests
{
    [TestClass]
    public class GameSetup
    {
        [TestMethod]
        public void CreateBeginnerGame()
        {
            var gb = new GameBoard(GameLevels.Beginner);
            Assert.AreEqual(gb.NumberOfPieces, GameConfig.BeginnerGamePieceCount);
        }
    }
}
