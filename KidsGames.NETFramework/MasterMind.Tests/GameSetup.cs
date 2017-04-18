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

        [TestMethod]
        public void CheckInitialGameBoard()
        {
            var gb = new GameBoard(GameLevels.Beginner);
            foreach(var turn in gb.GameTurns)
            {
                for (int i = 0; i < gb.NumberOfPieces; i++)
                {
                    Assert.AreEqual(turn.CodeBreakerGuesses[i], GamePieces.Empty);
                    Assert.AreEqual(turn.GuessResults[i], GuessResult.Empty);
                }
            }
        }

        [TestMethod]
        public void SetValidCode()
        {
            GameBoard gb = new GameBoard(GameLevels.Beginner);
            var code = new GamePieces[] {GamePieces.Piece1, GamePieces.Piece2, GamePieces.Piece3 };
            gb.SetCode(code);
            for (int i = 0; i < code.Length; i++)
            {
                Assert.AreEqual(gb.SecretCode[i], code[i]);
            }
        }

        [TestMethod]
        public void SetInvalidCode()
        {
            var gb = new GameBoard(GameLevels.Beginner);
            var code = new GamePieces[] { GamePieces.Piece1, GamePieces.Piece2, GamePieces.Piece3, GamePieces.Piece4 };
            try
            {
                gb.SetCode(code);
                
            }
            catch (InvalidOperationException)
            {
                return;
            }
            Assert.Fail();
        }

        [TestMethod]
        public void CreateAdvancedGame()
        {
            //var gb = new GameBoard(GameLevels.Advanced);
            //Assert.AreEqual(gb.NumberOfPieces, GameConfig.AdvancedGamePieceCount);
        }
    }
}
