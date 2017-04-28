using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MasterMind.Logic
{
	public enum DifficultyLevels { Beginner, Advanced }

    public class GameConfig
    {
        public const int BeginnerGamePieceCount = 3;
        public const int AdvancedGamePieceCount = 4;
        public const int MaxNumberOfTurns = 10;

        public static int GetPieceCount(DifficultyLevels difficulty)
        {
            if (difficulty == DifficultyLevels.Beginner)
            {
                return BeginnerGamePieceCount;
            }
            else
            {
                return AdvancedGamePieceCount;
            }
        }
    }
}
