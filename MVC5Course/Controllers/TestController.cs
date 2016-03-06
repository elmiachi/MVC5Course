using MVC5Course.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVC5Course.Controllers
{
    public class TestController : Controller
    {
        // GET: Test
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult MemberProfile()
        {
            //var db = new FabricsEntities();

            //var client = new Client() {ClientId=2 };
            //db.Client.Add(client);
            //db.SaveChanges();

            return View();
        }
        [HttpPost]
        public ActionResult MemberProfile(MemberViewModel data)
        {
            return View();
        }
       
    }
}