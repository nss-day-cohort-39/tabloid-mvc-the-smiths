using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using TabloidMVC.Models;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    public class TagController : Controller
    {
        private readonly TagRepository _tagRepository;

        public TagController(IConfiguration config)
        {
            _tagRepository = new TagRepository(config);
        }

        // GET: Tag
        public ActionResult Index()
        {
            List<Tag> tags = _tagRepository.GetAllTags();
            return View(tags);
        }

        // GET: Tag/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Tag/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Tag/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Tag tag)
        {
            try
            {
                // TODO: Add insert logic here
                _tagRepository.AddTag(tag);

                return RedirectToAction("Index");
            }
            catch(Exception ex)
            {
                return View(tag);
            }
        }

        // GET: Tag/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Tag/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }

        // GET: Tag/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Tag/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id, IFormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction(nameof(Index));
            }
            catch
            {
                return View();
            }
        }
    }
}