using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcSample.Models;

// For more information on enabling MVC for empty projects, visit http://go.microsoft.com/fwlink/?LinkID=397860

namespace MvcSample.Controllers
{
    public class StudentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public StudentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            ViewData["Title"] = "Students";
            var model = _context.Students.ToList();
            return View(model);
        }
        [Route("Student/{id}/Details")]
        [HttpGet]
        public IActionResult Details(int id)
        {
            var model = _context.Students.Include(student => student.Subjects).FirstOrDefault(student => student.Id.Equals(id));
            return View(model);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult AddSubject(int id, string name, int level)
        {
            _context.Students.Include(student => student.Subjects).FirstOrDefault(student => student.Id.Equals(id)).Subjects.Add(new Subject {Level = level, Name = name});
            _context.SaveChanges();
            return RedirectToAction("Details","Student",new {Id = id});
        }

        [HttpGet]
        public IActionResult Edit(int? id)
        {
            Student student = _context.Students.Single(m => m.Id.Equals(id));
            ViewData["Title"] = "Edit " + student.FirstName + " " + student.LastName;
            return View(student);
        }

        [HttpPost]
        public IActionResult Edit(int id, Student student)
        {
            if (ModelState.IsValid)
            {
                _context.Entry(student).State = EntityState.Modified;
                _context.SaveChanges();
                return RedirectToAction("Index");
            }         
            return View(student);

        }
        
        public IActionResult Delete(int id)
        {
            var original = _context.Students.FirstOrDefault(student => student.Id.Equals(id));
            if (original != null)
            {
                _context.Students.Remove(original);
                _context.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}
