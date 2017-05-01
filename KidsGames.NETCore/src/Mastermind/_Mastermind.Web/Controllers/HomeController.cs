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
        public IActionResult Index()
        {
            // The view being returned is calculated based on the name of the
            // controller (Home) and the name of the action method (Index).
            // So in this case, the view returned is /Views/Home/Index.cshtml.
            return View();
        }

        [HttpPost]
        public HttpResponseMessage SetSecretCode([FromBody] DataContainer data)
        {
            //var req = HttpContext.Request;
            //var read = new StreamReader(req.Body);
            //var body = read.ReadToEnd();
            //var d = JsonConvert.DeserializeObject<DataContainer>(body);
            var response = new HttpResponseMessage(System.Net.HttpStatusCode.OK);
            return response;
        }

        public JsonResult Test()
        {
            var json = this.Json(new DataContainer());
            return json;
        }

        public IActionResult About()
        {
            // Creates a model and passes it on to the view.
            Employee[] model =
            {
                new Employee { Name = "Alfred", Title = "Manager" },
                new Employee { Name = "Sarah", Title = "Accountant" }
            };

            return View(model);
        }
    }

    public class DataContainer
    {
        public List<GamePieces> Codes { get; set; }
    }
}
