﻿using Newtonsoft.Json;
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

        public int MaxNumberOfTurns { get { return GameConfig.MaxNumberOfTurns; } }
        public int NumberOfPieces { get; private set; }
        public GameTurn[] GameTurns { get; private set; }
        public GamePieces[] SecretCode { get; private set; }

        

        public GameBoard(GameLevels gameLevel)
        {
            this.gameLevel = gameLevel;
            setNumberOfPieces();
            //InitializeTurns(GameConfig.MaxNumberOfTurns);
        }

        public void SetCode(GamePieces[] secretCode)
        {
            if (secretCode.Length != NumberOfPieces)
            {
                throw new InvalidOperationException("Incorrect number of pieces in code");
            }
            this.SecretCode = secretCode;
        }

        private void InitializeTurns(int maxNumberOfTurns)
        {
            this.GameTurns = new GameTurn[maxNumberOfTurns];
            for (int i = 0; i < GameTurns.Length; i++)
            {
                this.GameTurns[i] = new GameTurn(this.NumberOfPieces);
            }
            
        }

        private void setNumberOfPieces()
        {
            var a = "Hello " + this.NumberOfPieces + "something";
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
}
