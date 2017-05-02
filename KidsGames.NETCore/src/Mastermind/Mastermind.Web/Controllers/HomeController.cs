using System;
using System.Collections.Generic;
using System.Linq;
using Mastermind.Web.Models;
using Microsoft.AspNetCore.Mvc;
using MasterMind.Logic;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;

namespace Mastermind.Web
{
    /// <summary>
    /// A controller intercepts the incoming browser request and returns
    /// an HTML view (.cshtml file) or any other type of data.
    /// </summary>
    public class HomeController : Controller
    {
        //static string m_pendingGame = null;
        static Dictionary<Guid, GameBoard> m_games = new Dictionary<Guid, GameBoard>() { /*{ Guid.NewGuid(), new GameBoard(DifficultyLevels.Beginner)}, { Guid.NewGuid(), new GameBoard(DifficultyLevels.Beginner) }*/ };

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetGames()
        {
            //PlayerRole role;
            //string gameId;

            //if (m_pendingGame == null)
            //{
            //    m_pendingGame = Guid.NewGuid().ToString();
            //    gameId = m_pendingGame;
            //    role = PlayerRole.CodeSetter;
            //}
            //else
            //{
            //    role = PlayerRole.CodeBreaker;
            //    gameId = m_pendingGame;
            //    m_pendingGame = null;
            //}
            var games = new List<Guid>();
            foreach(var gameId in m_games.Keys)
            {
                games.Add(gameId);
            }

            var json = this.Json(games);
            return json;
        }

        [HttpPost]
        public JsonResult CreateNewGame([FromBody] DifficultyLevels difficulty)
        {
            var newGame = new GameBoard(difficulty);
            m_games.Add(newGame.GameId, newGame);
            var json = this.Json(new GameSession(newGame.GameId, PlayerRole.CodeSetter));
            return json;
        }

        [HttpPost]
        public HttpResponseMessage SetSecretCode([FromBody] SetCodeContainer container)
        {
            var game = m_games[container.SessionInfo.SessionId];
            game.SetCode(container.Code);

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            return response;
        }

        public JsonResult MakeGuess([FromBody] GuessContainer guess)
        {
            var game = m_games[guess.SessionInfo.SessionId];
            var result = game.MakeGuess(guess.Guess);
            var json = this.Json(result);
            return json;
        }

    }

    public class SetCodeContainer
    {
        public DifficultyLevels DifficultyLevel { get; set; }
        public List<GamePieces> Code { get; set; }
        public GameSession SessionInfo { get; set; }
    }

    public class GuessContainer
    {
        public List<GamePieces> Guess { get; set; }
        public GameSession SessionInfo { get; set; }
    }

    public class GameSession
    {
        public Guid SessionId { get; set; }
        public PlayerRole Role { get; set; }

        public GameSession(Guid id, PlayerRole role)
        {
            this.SessionId = id;
            this.Role = role;
        }
    }
}
