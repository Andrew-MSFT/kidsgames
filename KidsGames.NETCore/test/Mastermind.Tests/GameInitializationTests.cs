using System;
using MasterMind.Logic;
using Xunit;
using System.Collections.Generic;

namespace MasterMind.Tests
{
    public class GameInitializationTests
    {
        [Fact]
        public void CreateBeginnerGame()
        {
            var gb = new GameBoard(DifficultyLevels.Beginner);
            Assert.Equal(gb.NumberOfPieces, GameConfig.BeginnerGamePieceCount);
        }

        [Fact]
        public void CheckInitialGameBoard()
        {
            var gb = new GameBoard(DifficultyLevels.Beginner);
            //foreach (var turn in gb.GameTurns)
            //{
            //	for (int i = 0; i < gb.NumberOfPieces; i++)
            //	{
            //		Assert.Equal(turn.CodeBreakerGuesses[i], GamePieces.Empty);
            //		Assert.Equal(turn.GuessResults[i], GuessResult.Empty);
            //	}
            //}
        }

        [Fact]
        public void SetValidCode()
        {
            GameBoard gb = new GameBoard(DifficultyLevels.Beginner);
            var code = new List<GamePieces>() { GamePieces.Piece1, GamePieces.Piece2, GamePieces.Piece3 };
            gb.SetCode(code);
            for (int i = 0; i < code.Count; i++)
            {
                Assert.Equal(gb.SecretCode[i], code[i]);
            }
        }

        [Fact]
        public void SetInvalidCode()
        {
            var gb = new GameBoard(DifficultyLevels.Beginner);
            var code = new List<GamePieces>() { GamePieces.Piece1, GamePieces.Piece2, GamePieces.Piece3, GamePieces.Piece4 };
            try
            {
                gb.SetCode(code);

            }
            catch (InvalidOperationException)
            {
                return;
            }
            Assert.True(false);
        }

        [Fact]
        public void CreateAdvancedGame()
        {
            var gb = new GameBoard(DifficultyLevels.Advanced);
            Assert.Equal(gb.NumberOfPieces, GameConfig.AdvancedGamePieceCount);
        }

        [Fact]
        public void GuessCorrect()
        {
            var gb = new GameBoard(DifficultyLevels.Beginner);
            gb.SetCode(new List<GamePieces>() { GamePieces.Piece1, GamePieces.Piece2, GamePieces.Piece3 });
            var result = gb.MakeGuess(new List<GamePieces> { GamePieces.Piece1, GamePieces.Piece2, GamePieces.Piece3 });
            foreach (var r in result.GuessResults)
            {
                Assert.Equal(GuessResult.Correct, r);
            }
        }

        [Fact]
        public void GuessIncorrectLocations()
        {
            var gb = new GameBoard(DifficultyLevels.Beginner);
            gb.SetCode(new List<GamePieces>() { GamePieces.Piece1, GamePieces.Piece2, GamePieces.Piece3 });
            var result = gb.MakeGuess(new List<GamePieces> { GamePieces.Piece2, GamePieces.Piece3, GamePieces.Piece1 });
            foreach (var r in result.GuessResults)
            {
                Assert.Equal(GuessResult.CorrectButWrongLocation, r);
            }
        }

        [Fact]
        public void GuessIncorrect()
        {
            var gb = new GameBoard(DifficultyLevels.Beginner);
            gb.SetCode(new List<GamePieces>() { GamePieces.Piece1, GamePieces.Piece2, GamePieces.Piece3 });
            var result = gb.MakeGuess(new List<GamePieces> { GamePieces.Piece4, GamePieces.Piece4, GamePieces.Piece4 });
            foreach (var r in result.GuessResults)
            {
                Assert.Equal(GuessResult.Incorrect, r);
            }
        }

        [Fact]
        public void GuessPartiallyCorrect()
        {
            var gb = new GameBoard(DifficultyLevels.Beginner);
            gb.SetCode(new List<GamePieces>() { GamePieces.Piece1, GamePieces.Piece2, GamePieces.Piece3 });
            var result = gb.MakeGuess(new List<GamePieces> { GamePieces.Piece1, GamePieces.Piece3, GamePieces.Piece4 });

            Assert.Equal(GuessResult.Correct, result.GuessResults[0]);
            Assert.Equal(GuessResult.CorrectButWrongLocation, result.GuessResults[1]);
            Assert.Equal(GuessResult.Incorrect, result.GuessResults[2]);

        }
    }
}
