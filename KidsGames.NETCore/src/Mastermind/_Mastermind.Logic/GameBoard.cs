using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind.Logic
{
    public class GameBoard
    {
        private DifficultyLevels gameLevel;

        public int NumberOfPieces { get; private set; }
        public List<GamePieces[]> CodeBreakerGuesses { get; private set; }
        public List<GuessResult[]> GuessResults { get; private set; }
        public GamePieces[] SecretCode { get; private set; }
        private int m_currentTurn = 0;


        public GameBoard(DifficultyLevels difficulty)
        {
            this.gameLevel = difficulty;
            this.NumberOfPieces = GameConfig.GetPieceCount(difficulty);
            this.CodeBreakerGuesses = new List<GamePieces[]>();
            this.GuessResults = new List<GuessResult[]>();
        }

        public void SetCode(GamePieces[] secretCode)
        {
            if (secretCode.Length != NumberOfPieces)
            {
                throw new InvalidOperationException("Incorrect number of pieces in code");
            }
            this.SecretCode = secretCode;
        }

        public List<GuessResult> MakeGuess(GamePieces[] guess)
        {
            var result = new List<GuessResult>();
            this.CodeBreakerGuesses.Add(guess);

            for (int i = 0; i < guess.Length; i++)
            {
                bool guessFound = false;
                for (int x = 0; x < guess.Length; x++)
                {
                    if (x == i && this.SecretCode[x] == guess[i])
                    {
                        result.Add(GuessResult.Correct);
                        guessFound = true;
                        break;
                    }
                    else if (this.SecretCode[x] == guess[i])
                    {
                        result.Add(GuessResult.CorrectButWrongLocation);
                        guessFound = true;
                        break;
                    }
                }
                if (!guessFound)
                {
                    result.Add(GuessResult.Incorrect);
                }
            }

            m_currentTurn++;
            return result;
        }

    }

    public enum GamePieces { Empty, Piece1, Piece2, Piece3, Piece4 }
    public enum GuessResult { Empty, Incorrect, CorrectButWrongLocation, Correct }

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
