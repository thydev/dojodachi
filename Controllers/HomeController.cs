using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System;

namespace dojodachi.Controllers
{
    
    public class HomeController : Controller
    {
        private static Random random = new Random();

        [HttpGet]
        [Route("")]
        public IActionResult Index()
        {
            Dachi dachi;
            // If the session does not exist
            if(HttpContext.Session.GetObjectFromJson<Dachi>("dachi") == null)
            {
                dachi = new Dachi();
                HttpContext.Session.SetObjectAsJson("dachi", dachi);
            } else {
                dachi = HttpContext.Session.GetObjectFromJson<Dachi>("dachi");
            }
            
            ViewBag.dachi = dachi;
            // System.Console.WriteLine(dachi.happiness);
            return View();
        }

        [HttpGet]
        [Route("displayobj")]
        public JsonResult DisplayObj()
        {
            var AnonObject = new {
                                FirstName = "Raz",
                                LastName = "Aquato",
                                Age = 10
                            };
            return Json(AnonObject);
        }


        
        [HttpGet]
        [Route("actions/{playAction}")]
        public JsonResult Actions(string playAction)
        {
            Dachi dachi;
            // If the session does not exist
            if(HttpContext.Session.GetObjectFromJson<Dachi>("dachi") == null)
            {
                // System.Console.WriteLine("inside null ?????");
                dachi = new Dachi();
                HttpContext.Session.SetObjectAsJson("dachi", dachi);

            } else {
                dachi = HttpContext.Session.GetObjectFromJson<Dachi>("dachi");
                // System.Console.WriteLine("Not nulll please!!!!");
            }

            switch (playAction)
            {
                case "feed":
                    Console.WriteLine("Case Feed");
                    // Feeding your Dojodachi costs 1 meal and gains a random amount of fullness 
                    // between 5 and 10 (you cannot feed your Dojodachi if you do not have meals)
                    System.Console.WriteLine(dachi.meals + " meals");
                    if(dachi.meals > 0){
                        random = new Random();
                        dachi.meals -= 1;
                        dachi.fullness += random.Next(5, 10);
                        HttpContext.Session.SetObjectAsJson("dachi", dachi);
                    }

                    break;
                case "play":
                    Console.WriteLine("Case Play");
                    break;
                case "work":
                    Console.WriteLine("Case Work");
                    break;
                case "sleep":
                    Console.WriteLine("Case Sleep");
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            
            

            var response = new {
                status = "Ok",
                dachi = dachi
                // dachi = JsonConvert.SerializeObject(dachi)
            };

            return Json(response);
        }

        [HttpGet]
        [Route("restart")]

        public IActionResult restart()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}