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
        static Dictionary<Guid, GameBoard> m_games = new Dictionary<Guid, GameBoard>();

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

            var json = this.Json(new NewGameContainer(gameId, role));
            return json;
        }

        [HttpPost]
        public HttpResponseMessage SetSecretCode([FromBody] SetCodeContainer container)
        {
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            return response;
        }

    }

    public class SetCodeContainer
    {
        public DifficultyLevels DifficultyLevel { get; set; }
        public List<GamePieces> Code { get; set; }
    }

    public class NewGameContainer
    {
        public string SessionId { get; set; }
        public PlayerRole Role { get; set; }

        public NewGameContainer(string id, PlayerRole role)
        {
            this.SessionId = id;
            this.Role = role;
        }
    }
}
