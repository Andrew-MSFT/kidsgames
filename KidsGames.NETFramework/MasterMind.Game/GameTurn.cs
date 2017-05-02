namespace MasterMind.Game
{
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
