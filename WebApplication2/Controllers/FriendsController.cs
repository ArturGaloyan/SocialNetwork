using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;
using WebApplication2.Controllers;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace WebApplication2.Controllers
{
    public class FriendsController : Controller
    {
        SocialContext context;
        public FriendsController(SocialContext context)
        {
            this.context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Add(int id)
        {
            int? es = HttpContext.Session.GetInt32("user");
            Requests r = new Requests();
            r.User1id = (int)es;
            r.User2id = id;
            context.Requests.Add(r);
            context.SaveChanges();
            
            return Json("Ok");

        }
        public IActionResult MyRequests()
        {
            int? es = HttpContext.Session.GetInt32("user");
            if (es==null)
            {
                return Redirect("/User/Login");
            }
            return View();
        }
        public IActionResult GetMyRequests()
        {
            int? es = HttpContext.Session.GetInt32("user");

            var data = (from elm in context.Requests
                        where elm.User2id == es
                        select new
                        {
                            id = elm.User1id,
                            name = elm.User1.name,
                            surname = elm.User1.surname
                        }).ToList();
            return Json(data);
        }
        public IActionResult Avelacnel_friend(int id)
        {
            int? es = HttpContext.Session.GetInt32("user");
            var data = (from elm in context.Requests
                        where elm.User2id == es && elm.User1id == id
                        select elm).FirstOrDefault();


            context.Requests.Remove(data);
            
            Friends f = new Friends();
            f.User1id = (int)es;
            f.User2id = id;

            context.Friends.Add(f);
            context.SaveChanges();
                        
            return Json(data);
        }

        public IActionResult RemoveRequests(int id)
        {
            int? es = HttpContext.Session.GetInt32("user");
            var data = (from elm in context.Requests
                        where elm.User2id == es && elm.User1id == id
                        select elm).FirstOrDefault();


            context.Requests.Remove(data);
            context.SaveChanges();
            return Json(data);


        }
        public IActionResult MyFriends()
        {
            int? es = HttpContext.Session.GetInt32("user");
            return View();
        }

        public IActionResult GetMyFriendList()
        {
            int? es = HttpContext.Session.GetInt32("user");


            var mas1 = (from item in context.Friends
                        where item.User1id == es

                        select new
                        {
                            id=item.User2id,
                            name = item.User2.name,
                            surname = item.User2.surname
                        }).ToList();

            var mas2 = (from item in context.Friends
                        where item.User2id == es
                        select new
                        { 
                            id=item.User1id,
                            name=item.User1.name,
                            surname=item.User1.surname
                        
                        }).ToList();

            var data= mas2.Union(mas1);
            return Json(data);

        }

        public IActionResult Delete_friend(int id)
        {
            int? es = HttpContext.Session.GetInt32("user");

            var data = (from elm in context.Friends
                        where elm.User1id == es && elm.User2id == id || 
                        (elm.User2id== es && elm.User1id==id)
                        select elm).FirstOrDefault();
       
            context.Friends.Remove(data);             
            context.SaveChanges();

            Merjvacnery merj = new Merjvacnery();
            merj.User1id = (int)es;
            merj.User2id = id;
            merj.text = "Dzez jnjel e ir ynkerneri cucakic.";

            ViewBag.tvyalner = merj;// ashxatum e erb delete enq anum;

            context.Merjvacneries.Add(merj);
            context.SaveChanges();          
            return Json(data);
        }

    }
}