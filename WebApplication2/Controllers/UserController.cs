using System;
using System.Collections.Generic;
using System.Diagnostics.Eventing.Reader;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using WebApplication2.Models;
using WebApplication2.NewFolder;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using Microsoft.AspNetCore.Http;
using System.IO;
using WebApplication2.lib;

namespace WebApplication2.Controllers
{
    public class UserController : Controller
    {
       SocialContext context;

        public object ConfigurationManager { get; private set; }

        public UserController(SocialContext context)
        {
            this.context = context;
        }

       
        public async Task<IActionResult> upload(IFormFile nkar)
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

            //vercnel id, linq=ov gtnel, photon poxel, saveChanges, uxarkel /profile
            int? es = HttpContext.Session.GetInt32("user");

            var data = (from elm in context.Users
                        where elm.id==es 
                        select elm ).FirstOrDefault();
            data.photo = name;
            context.SaveChanges();
            return Redirect("/User/Profile");
           
        }
        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Signup()
        {
            return View();
        }
        public IActionResult Login()
        {
            return View();

        }
        public IActionResult Stugel(string id)
        {
            var data = (from item in context.Users
                        where item.login == id
                        select item).ToList().Count;

            return Json(data);

        }
        public IActionResult Grancvel(User obj)
        {
            if (string.IsNullOrEmpty(obj.name) || string.IsNullOrEmpty(obj.surname) ||
                string.IsNullOrEmpty(obj.login) || string.IsNullOrEmpty(obj.password))
            {
                TempData["Namak"] = "Խնդրում ենք լրացնել բոլոր դաշտերը";
                return Redirect("/User/Signup");
            }

            if (obj.password.Length < 6)
            {
                TempData["Namak"] = "Ծածկագիրը կարճ է";
                return Redirect("/User/Signup");
            }
            if (obj.name == obj.surname)
            {
                TempData["Namak"] = "Անունը և ազգանունը չեն կարող նույնը լինել";
                return Redirect("/User/Signup");
            }

            var t = (from elm in context.Users where elm.login == obj.login select elm).ToList().Count;
            if (t > 0)
            {
                TempData["Namak"] = "Այս օգտանունը արդեն զբաղված է";
                return Redirect("/User/Signup");
            }


            bool k = false;
            int s = 0;
            for (int i = 0; i < obj.password.Length; i++)
            {
                if (char.IsUpper(obj.password[i])
                  && obj.password.Contains('!')
                  || obj.password.Contains('@')
                  || obj.password.Contains('#')
                  || obj.password.Contains('$')
                  || obj.password.Contains('%')
                  || obj.password.Contains('^')
                  || obj.password.Contains('&')
                  || obj.password.Contains('*'))
                {
                    k = true;
                    s++;

                }
            }


            if (k == false || s < 2)
            {
                TempData["Namak"] = "Գաղտնաբառը պետք է պարունակի 2 մեծատառ և տվյալ սիմվոլներից գոնե 1-ը (!@#$%^&*)";
                return Redirect("/User/Signup");
            }




            bool b = false;
            int d = 0;

            if (obj.login.Contains('@') && !obj.login.Contains("@.")
                && !obj.login.Contains("..") && !obj.login.EndsWith('.'))



            {
                b = true;
                d++;
            }

            else if (b == false || d != 1)
            {
                TempData["Namak"] = "օգտանունը սխալ է";
                return Redirect("/User/Signup");
            }
            obj.type = 0;
            obj.block = 0;
            obj.password = SecurePasswordHasher.Hash(obj.password);
            context.Users.Add(obj);
            context.SaveChanges();
            return Redirect("/User/Signup");
        }

        public IActionResult Mutqagrel(User obj)
        {
            var data = (from elm in context.Users where elm.login == obj.login select elm).FirstOrDefault();
            if(data==null)
            {
                TempData["Namak"] = " լոգինը սխալ է";
               return  Redirect("/User/Login");

            }        
            else if( !SecurePasswordHasher.Verify(obj.password,data.password))         
            {
                TempData["Namak"] = " գաղտնաբառը չի գտնվել";
                return Redirect("/User/Login");
            }
            else
            {
                HttpContext.Session.SetInt32("user", data.id);
                if (data.block == 1)
                {
                    TempData["Namak"] = "Դուք արգելափակված եք";
                    return Redirect("/User/Login");
                }               
                if (data.type == 0)
                {

                    var x = (from elm in context.Merjvacneries
                             where elm.User2id == data.id
                             select elm).ToList();

                    //foreach (Merjvacnery elm in x)
                    //{
                    //    return Json(elm.text);
                    //}

                    ViewBag.tvyalner = x;



                    return Redirect("/User/Profile");
                }
                if (data.type == 1)
                {
                    return Redirect("/Admin/Dashboard");
                }
                

                return Redirect("/User/Profile");
            }

        }


        public IActionResult Profile()
        {
            //  int id = Convert.ToInt32(TempData["User"]);
            int? es = HttpContext.Session.GetInt32("user");
            
            if (es == null)
            {
                return Redirect("/User/Login");
            }
            var data = (from elm in context.Users
                        where elm.id == es
                        select elm).FirstOrDefault();

            ViewBag.currentUser = data;

            var x = (from elm in context.Merjvacneries
                     where elm.User2id == es
                     select elm).ToList();




            (from item in context.Merjvacneries  // ???????
                where item.User2id == es
                select new
                        {
                            users = (from elm in context.Users
                                          where item.User1id == elm.id
                                          select elm).ToList()
                        }).ToList();

 
            

            ViewBag.tvyalner = x;
            return View();
        }

        public IActionResult Settings()
        {
            int? id = HttpContext.Session.GetInt32("user");
            if (id == null)
            {
                return Redirect("/User/Login");
            }
            return View();
        }

        [HttpPost]
        public IActionResult Settings(User obj)
        {
            int? id = HttpContext.Session.GetInt32("user");
            var data = (from elm in context.Users where elm.id == id select elm).FirstOrDefault();
            if (!SecurePasswordHasher.Verify(obj.password, data.password))
            {
                TempData["Namak"] = "Գաղտնաբառը սխալ է";
                return Redirect("/User/Settings");
            }


            var login_zbaxvac = (from elm in context.Users
                                 where elm.login == obj.login
                                 select elm).ToList().Count;

            if (login_zbaxvac > 0)
            {
                TempData["Namak"] = "Լոգինը զբաղված է";
                return Redirect("/User/Settings");
            }


            data.login = obj.login;
            context.SaveChanges();
            return Redirect("/User/Profile");
        }

        public IActionResult changePassword(PasswordViewModel obj)
        {
            int? id = HttpContext.Session.GetInt32("user");
            var data = (from elm in context.Users where elm.id == id select elm).FirstOrDefault();
            if (!SecurePasswordHasher.Verify(obj.oldPassword, data.password))
            {
                TempData["Namak2"] = "Գաղտնաբառը սխալ է";
                return Redirect("/User/Settings");
            }

          
            bool kk = false;
            for(int i=0;i<obj.newPassword.Length;i++)
            {
                if(char.IsUpper(obj.newPassword[i]))
                {
                    kk = true;
                }
            }
            if(kk==false)
            {
                TempData["Namak2"] = "Գաղտնաբառը պետք է պարունակի մեծատառ";
                return Redirect("/User/Settings");
            }
            if (obj.newPassword.Length < 6)
            {
                TempData["Namak2"] = "Գաղտնաբառը կարճ է";
                return Redirect("/User/Settings");
            }

            obj.newPassword = SecurePasswordHasher.Hash(obj.newPassword);

            data.password = obj.newPassword;
            context.SaveChanges();
            return Redirect("/User/Profile");
            
        }
        public IActionResult SearchFriends(string id)
        {
            int? es = HttpContext.Session.GetInt32("user");

            var data = (from item in context.Users
                        where item.name.StartsWith(id)
                        select new {

                            name = item.name,
                            surname = item.surname,
                            id = item.id,
                            photo=item.photo,
                            arefriends = (from elm in context.Friends
                                          where (elm.User1id==es && elm.User2id==item.id)
                                          || (elm.User2id==es && elm.User1id==item.id)
                                          select elm).ToList().Count,

                            isRequestSent = (from elm in context.Requests
                                             where elm.User1id == es && elm.User2id == item.id
                                             select elm).ToList().Count

                        }).ToList();
            return Json(data);
        }
        public IActionResult Logout()
        {
            HttpContext.Session.Remove("user");
            return Redirect("/user/login");
        }




    }
}






