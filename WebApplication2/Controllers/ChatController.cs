using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class ChatController : Controller
    {
        SocialContext context;
        public ChatController(SocialContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }


        public IActionResult dashboard()
        {
            int? es = HttpContext.Session.GetInt32("user");
            if (es == null)
            {
                return Redirect("/user/login");
            }
            return View();
        }


        public IActionResult Show_friends()
        {
            int? es = HttpContext.Session.GetInt32("user");

            var mas1 = (from item in context.Friends
                        where item.User1id == es

                        select new
                        {
                            id = item.User2id,
                            name = item.User2.name,
                            surname = item.User2.surname,
                            nornamak=0,
                            photo=item.User2.photo
                        }).ToList();

            var mas2 = (from item in context.Friends
                        where item.User2id == es
                        select new
                        {
                            id = item.User1id,
                            name = item.User1.name,
                            surname = item.User1.surname,
                            nornamak = 0,
                            photo = item.User1.photo



                        }).ToList();

            var data = mas2.Union(mas1);
            return Json(data);
        }

        public IActionResult Show_chat(int id)
        {
            int? es = HttpContext.Session.GetInt32("user");

            var data = (from elm in context.Messages
                        where (elm.User1id == es && elm.User2id==id )

                        || (elm.User2id == es && elm.User1id==id )

                        select elm
                        ).ToList();

            foreach(Messages m in data)
            {
                if (m.User2id== es && m.User1id==id)
                {
                    m.status = 1;
                }
            }
            context.SaveChanges();

            return Json(data);
            
        }

        public IActionResult sendMessage(int user_id, string namak)
        {
            int? es = HttpContext.Session.GetInt32("user");

            Messages m = new Messages();
            m.User1id = (int)es;
            m.User2id = user_id;
            m.text = namak;
            m.status = 0;
            context.Add(m);
            context.SaveChanges();

            return Json(user_id + " " + namak);
        }

        public IActionResult getMyNotifications()
        {
            int? es = HttpContext.Session.GetInt32("user");

            var qanak = (from item in context.Messages
                         where item.User2id == es && item.status == 0
                         select item).ToList().Count;
            return Json(qanak);
        }


        public IActionResult nornamakner()
        {
            int? es = HttpContext.Session.GetInt32("user");

            var data = (from item in context.Messages
                        where item.User2id == es && item.status == 0
                        select item.User1id).ToList().Distinct();

            return Json(data);
        }





    }
}