using System;

namespace MasterMind
{
    public enum GameLevels { Beginner, Advanced}

    public class GameBoard
    {
        private GameLevels gameLevel;

        public int NumberOfPieces { get; private set; }

        public GameBoard(GameLevels beginner)
        {
            this.gameLevel = beginner;
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
}
