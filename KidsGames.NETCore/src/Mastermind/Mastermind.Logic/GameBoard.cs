using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind.Logic
{
    public enum GamePieces { Empty, Piece1, Piece2, Piece3, Piece4 }
    public enum GuessResult { Empty, Incorrect, CorrectButWrongLocation, Correct }
    public enum PlayerRole { CodeSetter, CodeBreaker}

    public class GameBoard
    {
        private DifficultyLevels gameLevel;

        public Guid GameId { get; private set; }
        public int NumberOfPieces { get; private set; }
        public List<GamePieces[]> CodeBreakerGuesses { get; private set; }
        public List<GuessResult[]> GuessResults { get; private set; }
        public List<GamePieces> SecretCode { get; private set; }
        private int m_currentTurn = 0;


        public GameBoard(DifficultyLevels difficulty)
        {
            this.GameId = Guid.NewGuid();
            this.gameLevel = difficulty;
            this.NumberOfPieces = GameConfig.GetPieceCount(difficulty);
            this.CodeBreakerGuesses = new List<GamePieces[]>();
            this.GuessResults = new List<GuessResult[]>();
        }

        public void SetCode(List<GamePieces> secretCode)
        {
            if (secretCode.Count != NumberOfPieces)
            {
                throw new InvalidOperationException("Incorrect number of pieces in code");
            }
            this.SecretCode = secretCode;
        }

        public GameTurn MakeGuess(List<GamePieces> guess)
        {
            GameTurn turn;
            var result = new List<GuessResult>();
            this.CodeBreakerGuesses.Add(guess.ToArray());

            for (int i = 0; i < guess.Count; i++)
            {
                bool guessFound = false;
                for (int x = 0; x < guess.Count; x++)
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

            turn = new GameTurn(m_currentTurn, guess, result);
            m_currentTurn++;
            return turn;
        }

    }

    public class GameTurn
    {
        public int TurnNumber { get; set; }
        public List<GamePieces> Guesses { get; set; }
        public List<GuessResult> GuessResults { get; set; }

        public GameTurn(int number, List<GamePieces> guess, List<GuessResult> result)
        {
            this.TurnNumber = number;
            this.Guesses = guess;
            this.GuessResults = result;
        }
    }
}
