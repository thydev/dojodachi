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
                //When refresh the page after loss or win
                //Reset the game
                if(dachi.energy >= 100 && dachi.fullness >= 100 && dachi.happiness >= 100){
                    dachi = new Dachi();
                    HttpContext.Session.SetObjectAsJson("dachi", dachi);
                } else if (dachi.fullness <= 0 || dachi.happiness <= 0){
                    dachi = new Dachi();
                    HttpContext.Session.SetObjectAsJson("dachi", dachi);
                }
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
            string reaction = "";
            switch (playAction)
            {
                // The maxValue for the upper-bound in the Next() method is exclusive
                case "feed":
                    Console.WriteLine("Case Feed");
                    // Feeding your Dojodachi costs 1 meal and gains a random amount of fullness 
                    // between 5 and 10 (you cannot feed your Dojodachi if you do not have meals)
                    // Every time you play with or feed your dojodachi there should be a 25% chance that it won't like it. 
                    // Energy or meals will still decrease, but happiness and fullness won't change.
                    System.Console.WriteLine(dachi.meals + " meals");
                    if(dachi.meals > 0){
                        random = new Random();
                        dachi.meals -= 1;
                        var fullness = 0;
                        // 25% chance
                        if(random.Next(1, 5) != 1){
                            fullness = random.Next(5, 11);
                            dachi.fullness += fullness;
                        }
                        reaction = $"Feed: Meals -1, Fullness +{fullness}";
                    }

                    break;
                case "play":
                    Console.WriteLine("Case Play");
                    // Playing with your Dojodachi costs 5 energy and gains a random amount of happiness between 5 and 10
                    // Every time you play with or feed your dojodachi there should be a 25% chance that it won't like it. 
                    // Energy or meals will still decrease, but happiness and fullness won't change.
                    random = new Random();
                    dachi.energy -= 5;
                    int happiness = 0;
                    // 25% chance
                    if(random.Next(1, 5) != 1){
                        happiness = random.Next(5, 11);
                        dachi.happiness += happiness;
                    }
                    reaction = $"Play: Energy -5, Happiness +{happiness}";
                    break;
                case "work":
                    Console.WriteLine("Case Work");
                    // Working costs 5 energy and earns between 1 and 3 meals
                    random = new Random();
                    dachi.energy -= 5;
                    int meals = random.Next(1, 4);
                    dachi.meals += meals;
                    reaction = $"Work: Energy -5, Meals +{meals}";
                    break;
                case "sleep":
                    Console.WriteLine("Case Sleep");
                    // Sleeping earns 15 energy and decreases fullness and happiness each by 5
                    dachi.fullness -= 5;
                    dachi.happiness -= 5;
                    dachi.energy += 15;
                    reaction = $"Sleep: Fullness -5, Happiness -5, Energy +15";
                    break;
                default:
                    Console.WriteLine("Default case");
                    break;
            }
            
            
            HttpContext.Session.SetObjectAsJson("dachi", dachi);
            var response = new {
                status = "Ok",
                dachi = dachi,
                reaction = reaction
                // dachi = JsonConvert.SerializeObject(dachi)
            };

            return Json(response);
        }

        [HttpPost]
        [Route("restart")]
        public IActionResult restart()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index");
        }
    }
}