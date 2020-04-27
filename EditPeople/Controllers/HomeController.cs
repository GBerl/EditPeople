using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EditPeople.Models;
using People.Data;

namespace EditPeople.Controllers
{
    public class HomeController : Controller
    {
       
        private string _connection = @"Data Source=.\sqlexpress;Initial Catalog=People;Integrated Security=True";
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult ShowPeople()
        {
            var db = new Peopledb(_connection);
            return Json(db.GetAllPeople());
            
        }
        [HttpPost]
        public IActionResult Edit(Person p)
        {
            var db = new Peopledb(_connection);
            db.Edit(p);
            return Json(p);
    
        }
        [HttpPost]
        public IActionResult Delete(Person p)
        {
            var db = new Peopledb(_connection);
            db.Delete(p);
            return Json(p);

        }
       
        public IActionResult AddPerson(Person p)
        {
            var db = new Peopledb(_connection);
            db.Add(p);
            return Json(p);

        }

    }
}
