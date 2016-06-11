using GraphDataService.Query.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace GraphVisualization.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult ShortestPath(int fromVertex, int toVertex)
        {
            return View();
        }
    }
}