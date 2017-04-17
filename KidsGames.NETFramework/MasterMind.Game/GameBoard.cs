using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind.Game
{
    public enum GameLevels { Beginner, Advanced }

    public class GameBoard
    {
        private GameLevels gameLevel;

        public int NumberOfPieces { get; private set; }
        public GameTurn[] GameTurns { get; private set; }
        public GamePieces[] SecretCode { get; private set; }
        

        public GameBoard(GameLevels gameLevel)
        {
            this.gameLevel = gameLevel;
            setNumberOfPieces();
            SetupGameTurns();
        }

        private void SetupGameTurns()
        {
            this.GameTurns = new GameTurn[GameConfig.MaxNumberOfTurns];
            for (int i = 0; i < GameTurns.Length; i++)
            {
                this.GameTurns[i] = new GameTurn(this.NumberOfPieces);
            }
        }

        private void setNumberOfPieces()
        {
            if (gameLevel == GameLevels.Beginner)
            {
                this.NumberOfPieces = 3;
            }
            else
            {
                this.NumberOfPieces = 4;
            }
        }
    }

    public enum GamePieces { Empty, Piece1, Piece2, Piece3, Piece4}
    public enum GuessResult { Empty, Incorrect, CorrectButWrongLocation, Correct}

    public class GameTurn
    {
        public GamePieces[] CodeBreakerGuesses { get; set; }
        public GuessResult[] GuessResults { get; set; }

        public GameTurn(int numPieces)
        {
            this.CodeBreakerGuesses = new GamePieces[numPieces];
            this.GuessResults = new GuessResult[numPieces];
        }
    }
}
