using Microsoft.VisualStudio.TestTools.UnitTesting;
using MasterMind.Game;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind.Game.Tests
{
    [TestClass]
    public class GameTurnTests
    {
        [TestMethod]
        public void GameTurnTest()
        {
            var turn = new GameTurn(GameConfig.BeginnerGamePieceCount);

            for (int i = 0; i < turn.CodeBreakerGuesses.Length; i++)
            {
                Assert.AreEqual(turn.CodeBreakerGuesses[i], GamePieces.Empty);
                Assert.AreEqual(turn.GuessResults[i], GuessResult.Empty);
            }

        }
    }
}