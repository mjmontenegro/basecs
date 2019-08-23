using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using crudi.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;


namespace crudi.Controllers
{
    public class HomeController : Controller
    {
        private MyContext dbContext;

        public HomeController(MyContext context)
        {
            dbContext = context;
        }
        [HttpGet("")]        
        public IActionResult Index()
        {
            // List<User> myUsers = dbContext.Users.ToList();
            return View("Index");
        }

        public IActionResult Privacy()
        {
            return View();
        }
        [HttpGet("/user/new")]
        public IActionResult NewUser()
        {
            return View("NewUser");
        }
        [HttpPost("user/create")]
        public IActionResult CreateUser(User newUser)
        {
            if(ModelState.IsValid)
            {
                if(!dbContext.Users.Any(u => u.Email == newUser.Email))
                {
                    PasswordHasher<User> Hasher = new PasswordHasher<User>();
                    newUser.Password = Hasher.HashPassword(newUser, newUser.Password);
                    dbContext.Add(newUser);
                    dbContext.SaveChanges();
                    User userInDb = dbContext.Users.FirstOrDefault(u => u.Email == newUser.Email);
                    HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                    return RedirectToAction("Dashboard");
                }
                else
                {
                    ModelState.AddModelError("Email", "Email is already in use!");
                }
            }
            return View("Index");
        }
        public IActionResult Login(LoginUser userSubmission)
        {
            if(ModelState.IsValid)
            {
                var userInDb = dbContext.Users.FirstOrDefault(u => u.Email == userSubmission.LoginEmail);
                if( userInDb != null)
                {
                    var hasher = new PasswordHasher<LoginUser>();
                    var result = hasher.VerifyHashedPassword(userSubmission, userInDb.Password, userSubmission.LoginPassword);
                    if(result != 0)
                    {
                        //log in user
                        HttpContext.Session.SetInt32("UserId", userInDb.UserId);
                        return RedirectToAction("Dashboard");
                    }
                }
                ModelState.AddModelError("LoginEmail", "Invalid Email/Password");
            }
            return View("Index");
        }
        [HttpGet("dashboard")]
        public IActionResult Dashboard()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Name = dbContext.Users.FirstOrDefault(u => u.UserId == userId).FirstName;
            ViewBag.UserId = dbContext.Users.FirstOrDefault(u => u.UserId == userId).UserId;
            List<Wedding> weddingsWithAttendees = dbContext.Weddings.Include(w => w.Attendees).ToList();
            return View("Dashboard", weddingsWithAttendees);
        }
        [HttpGet("wedding/new")]
        public IActionResult NewWedding()
        {
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Name = dbContext.Users.FirstOrDefault(u => u.UserId == userId).FirstName;
            ViewBag.UserId = dbContext.Users.FirstOrDefault(u => u.UserId == userId).UserId;
            return View("NewWedding");
        }
        [HttpPost("wedding/create")]
        public IActionResult CreateWedding(Wedding newWedding)
        {
            if(ModelState.IsValid)
            {
                dbContext.Weddings.Add(newWedding);
                dbContext.SaveChanges();
                return RedirectToAction("Dashboard");
            }
            int? userId = HttpContext.Session.GetInt32("UserId");
            if (userId == null)
            {
                return RedirectToAction("Index");
            }
            ViewBag.Name = dbContext.Users.FirstOrDefault(u => u.UserId == userId).FirstName;
            ViewBag.UserId = dbContext.Users.FirstOrDefault(u => u.UserId == userId).UserId;

            return View("NewWedding");
        }

        [HttpGet("wedding/join/{weddingId}")]
        public IActionResult Join(int weddingId)
        {
            // Add to DB
            RSVP myRSVP = new RSVP();
            myRSVP.UserId = (int)HttpContext.Session.GetInt32("UserId");
            myRSVP.WeddingId = weddingId;
            dbContext.RSVPs.Add(myRSVP);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet("wedding/leave/{weddingId}")]
        public IActionResult Leave(int weddingId)
        {
            int userId = (int)HttpContext.Session.GetInt32("UserId");
            RSVP resv = dbContext.RSVPs.FirstOrDefault(r => r.UserId == userId && r.WeddingId == weddingId);
            dbContext.RSVPs.Remove(resv);
            dbContext.SaveChanges();
            return RedirectToAction("Dashboard");
        }
        [HttpGet("wedding/delete/{weddingId}")]
        public IActionResult Delete(int weddingId)
        {
            int userId = (int)HttpContext.Session.GetInt32("UserId");
            Wedding weddingToDelete = dbContext.Weddings.FirstOrDefault(w => w.WeddingId == weddingId);
            if(weddingToDelete.UserId == userId)
            {
                dbContext.Weddings.Remove(weddingToDelete);4
                dbContext.SaveChanges();
            }
            return RedirectToAction("Dashboard");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
