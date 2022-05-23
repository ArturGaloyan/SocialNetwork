using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApplication2.Models;

namespace WebApplication2.Controllers
{
    public class AdminController : Controller
    {
        SocialContext context; 
        public AdminController(SocialContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Dashboard()
        {
            var user1 = (from elm in context.Users where elm.type == 0 select elm).ToList();
            ViewBag.tvyalner = user1;
            return View();
        }

        public IActionResult Block(int id)
        {
            var user = (from elm in context.Users
                        where elm.id == id select elm).FirstOrDefault();
            user.block = 1-user.block;
            context.SaveChanges();
            return Redirect("/Admin/Dashboard");
   
        }
     
       















































    }
}
