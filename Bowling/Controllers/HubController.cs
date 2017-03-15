using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Bowling.Controllers
{
    public class HubController : Controller
    {
        // GET: Hub
        public ActionResult Help ()
        {
            return View();
        }
    }
}