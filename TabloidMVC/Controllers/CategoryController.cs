using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class CategoryController : Controller
    {

        private CategoryRepository _categoryRepository;
            public CategoryController(IConfiguration config)
            {
            _categoryRepository = new CategoryRepository(config);
            }


        // GET: RepositoryController
        public IActionResult Index()
        {
            List<Category> categories = _categoryRepository.GetAll();
            
            return View(categories);
        }

        // GET: RepositoryController/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: RepositoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RepositoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RepositoryController/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: RepositoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: RepositoryController/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: RepositoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}
