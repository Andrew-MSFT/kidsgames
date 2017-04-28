using System;
using MasterMind.Logic;
using Xunit;


namespace MasterMind.Tests
{
    public class GameInitializationTests
    {
		[Fact]
		public void CreateBeginnerGame()
		{
			var gb = new GameBoard(GameLevels.Beginner);
			Assert.Equal(gb.NumberOfPieces, GameConfig.BeginnerGamePieceCount);
		}

		[Fact]
		public void CheckInitialGameBoard()
		{
			var gb = new GameBoard(GameLevels.Beginner);
			foreach (var turn in gb.GameTurns)
			{
				for (int i = 0; i < gb.NumberOfPieces; i++)
				{
					Assert.Equal(turn.CodeBreakerGuesses[i], GamePieces.Empty);
					Assert.Equal(turn.GuessResults[i], GuessResult.Empty);
				}
			}
		}

		[Fact]
		public void SetValidCode()
		{
			GameBoard gb = new GameBoard(GameLevels.Beginner);
			var code = new GamePieces[] { GamePieces.Piece1, GamePieces.Piece2, GamePieces.Piece3 };
			gb.SetCode(code);
			for (int i = 0; i < code.Length; i++)
			{
				Assert.Equal(gb.SecretCode[i], code[i]);
			}
		}

		[Fact]
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
			Assert.True(false);
		}

		[Fact]
		public void CreateAdvancedGame()
		{
			//var gb = new GameBoard(GameLevels.Advanced);
			//Assert.Equal(gb.NumberOfPieces, GameConfig.AdvancedGamePieceCount);
		}
    }
}
