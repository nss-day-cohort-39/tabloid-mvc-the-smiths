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
  

        // GET: RepositoryController/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: RepositoryController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Category category)
        {
            try
            {

                _categoryRepository.Add(category);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(category);
            }
        }

        // GET: RepositoryController/Edit/5
        public ActionResult Edit(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);
            
            if (category == null)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // POST: RepositoryController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, Category category)
        {
            try
            {
                _categoryRepository.UpdateCategory(category);
                
                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View(category);
            }
        }

        // GET: RepositoryController/Delete/5
        public ActionResult Delete(int id)
        {
            var category = _categoryRepository.GetCategoryById(id);

            if (category == null)
            {
                return RedirectToAction("Index");
            }
            return View(category);
        }

        // POST: RepositoryController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, Category category)
        {
            try
            {
                _categoryRepository.Delete(category);

                return RedirectToAction(nameof(Index));
            }
            catch (Exception ex)
            {
                return View(category);
            }

        }
    }
}
