using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using OrderingSystem.Data;
using OrderingSystem.Models;
using System;

namespace OrderingSystem.Controllers
{
    public class UnitController : Controller
    {
        private readonly ApplicationDbContext _db;

        public UnitController(ApplicationDbContext context)
        {
            _db = context;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var uNITS = _db.Units.ToList();
            return View(uNITS);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Create(Unit model)
        {
            _db.Units.Add(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpGet]
        public IActionResult Edit(int id)
        {
            var uNITS = _db.Units.Where(x => x.UnitId == id).FirstOrDefault();
            return View(uNITS);
        }


        [HttpPost]
        public IActionResult Edit(Unit model)
        {
            _db.Units.Update(model);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

     

        public IActionResult Delete(int id)
        {
            var uNITS = _db.Units.Find(id);
            _db.Units.Remove(uNITS);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}

