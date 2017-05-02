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
        static string m_pendingGame = null;
        static Dictionary<string, GameBoard> m_games = new Dictionary<string, GameBoard>();

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetGameInfo()
        {
            PlayerRole role;
            string gameId;

            if (m_pendingGame == null)
            {
                m_pendingGame = Guid.NewGuid().ToString();
                gameId = m_pendingGame;
                role = PlayerRole.CodeSetter;
            }
            else
            {
                role = PlayerRole.CodeBreaker;
                gameId = m_pendingGame;
                m_pendingGame = null;
            }

            var json = this.Json(new GameSession(gameId, role));
            return json;
        }

        [HttpPost]
        public HttpResponseMessage SetSecretCode([FromBody] SetCodeContainer container)
        {
            var game = new GameBoard(container.DifficultyLevel);
            game.SetCode(container.Code);
            m_games.Add(container.SessionInfo.SessionId, game);
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
        public string SessionId { get; set; }
        public PlayerRole Role { get; set; }

        public GameSession(string id, PlayerRole role)
        {
            this.SessionId = id;
            this.Role = role;
        }
    }
}
