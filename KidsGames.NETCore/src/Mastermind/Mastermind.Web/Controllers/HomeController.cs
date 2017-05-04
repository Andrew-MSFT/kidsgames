using System;
using System.Collections.Generic;
using System.Linq;
using Mastermind.Web.Models;
using Microsoft.AspNetCore.Mvc;
using MasterMind.Logic;
using System.Net.Http;
using System.IO;
using Newtonsoft.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using System.Net.WebSockets;
using System.Threading;

namespace Mastermind.Web
{
    /// <summary>
    /// A controller intercepts the incoming browser request and returns
    /// an HTML view (.cshtml file) or any other type of data.
    /// </summary>
    public class HomeController : Controller
    {
        //static string m_pendingGame = null;
        static Dictionary<Guid, GameBoard> m_openGames = new Dictionary<Guid, GameBoard>() { /*{ Guid.NewGuid(), new GameBoard(DifficultyLevels.Beginner)}, { Guid.NewGuid(), new GameBoard(DifficultyLevels.Beginner) }*/ };
        static Dictionary<Guid, GameBoard> m_existingGames = new Dictionary<Guid, GameBoard>();

        public IActionResult Index()
        {
            return View();
        }

        public JsonResult GetGames()
        {
            var games = new List<Guid>();
            foreach(var gameId in m_openGames.Keys)
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
            m_openGames.Add(newGame.GameId, newGame);
            var json = this.Json(new GameSession(newGame.GameId, PlayerRole.CodeSetter));
            return json;
        }

        [HttpPost]
        public JsonResult JoinGame([FromBody] GameSession session)
        {
            var id = session.SessionId;
            var game = m_openGames[id];
            m_openGames.Remove(id);
            m_existingGames.Add(id, game);
            session.Role = PlayerRole.CodeBreaker;
            var json = this.Json(session);
            return json;
        }

        [HttpPost]
        public HttpResponseMessage SetSecretCode([FromBody] SetCodeContainer container)
        {
            GameBoard game;
            if (m_openGames.ContainsKey(container.SessionInfo.SessionId))
            {
                game = m_openGames[container.SessionInfo.SessionId];
            }
            else
            {
                game = m_existingGames[container.SessionInfo.SessionId];
            }

            game.SetCode(container.Code);

            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            return response;
        }

        public JsonResult MakeGuess([FromBody] GuessContainer guess)
        {
            var game = m_existingGames[guess.SessionInfo.SessionId];
            var result = game.MakeGuess(guess.Guess);
            var json = this.Json(result);
            return json;
        }

        public async Task Echo(HttpContext context, WebSocket webSocket)
        {
            var buffer = new byte[1024 * 4];
            WebSocketReceiveResult result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            while (!result.CloseStatus.HasValue)
            {
                await webSocket.SendAsync(new ArraySegment<byte>(buffer, 0, result.Count), result.MessageType, result.EndOfMessage, CancellationToken.None);

                result = await webSocket.ReceiveAsync(new ArraySegment<byte>(buffer), CancellationToken.None);
            }
            await webSocket.CloseAsync(result.CloseStatus.Value, result.CloseStatusDescription, CancellationToken.None);
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
