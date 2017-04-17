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

        public GameBoard(GameLevels gameLevel)
        {
            this.gameLevel = gameLevel;
            setNumberOfPieces();
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
