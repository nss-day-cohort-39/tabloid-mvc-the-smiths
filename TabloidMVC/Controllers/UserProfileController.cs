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
    public class UserProfileController : Controller
    {
        private readonly UserProfileRepository _userProfileRepo;
       
        public UserProfileController(IConfiguration config)
        {
           _userProfileRepo = new UserProfileRepository(config);
        }

        // GET: UserProfileController1
        public ActionResult Index()
        {
            List<UserProfile> userProfiles = _userProfileRepo.GetAllUserProfiles();
            return View(userProfiles);
        }

        // GET: UserProfileController1/Details/5
        public ActionResult Details(int id)
        {
            try
            {
                var userProfile = _userProfileRepo.GetById(id);
                return View(userProfile);
            }
            catch
            {
               
                return View("Index");
            }

           
        }

        // GET: UserProfileController1/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: UserProfileController1/Create
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

        // GET: UserProfileController1/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: UserProfileController1/Edit/5
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

        // GET: UserProfileController1/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: UserProfileController1/Delete/5
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
