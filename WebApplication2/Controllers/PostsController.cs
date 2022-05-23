using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.lib;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class PostsController : Controller
    {
        SocialContext context;
        public PostsController(SocialContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> upload(IFormFile nkar, string text)// ???????????????????????
        {
            string name = RandomStringGenerator.RandomString(20) + ".jpg";

              
            if (nkar.Length > 0)
            {
                var filePath = "wwwroot/nkarner/" + name;

                using (var stream = System.IO.File.Create(filePath))
                {
                    await nkar.CopyToAsync(stream);
                }
            }

            int? es = HttpContext.Session.GetInt32("user");
            var data = (from elm in context.Users
                        where elm.id == es
                        select elm).FirstOrDefault();

            Photos p = new Photos();
            p.userId = (int)es;
            p.photo = name;
            p.desc = text;
            context.Photos.Add(p);
            context.SaveChanges();

            return Redirect("/Posts/MyPosts");

        }
        public IActionResult Grarumner()
        {
            return View();
        }
        public IActionResult MyPosts()
        {
            int? es = HttpContext.Session.GetInt32("user");

            if (es == null)
            {
                return Redirect("/user/login");
            }
            return View();
        }
        public IActionResult getmyposts()
        {
            int? es = HttpContext.Session.GetInt32("user");
            var data = (from elm in context.Photos
                        where elm.userId == es
                        select elm).ToList();
            return Json(data);
        }

       

    }
}
