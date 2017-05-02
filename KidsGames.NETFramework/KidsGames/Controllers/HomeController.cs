using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace KidsGames.Controllers
{
    public class HomeController : Controller
    {
        static List<object> m_list = new List<object>();

        public ActionResult Index()
        {
            m_list.Add(new byte[9999999]);
            for (int i = 0; i < 99999; i++)
            {
                var rand = new Random();
            }
            try
            {
                throw new NotImplementedException();
            }
            catch { }
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}