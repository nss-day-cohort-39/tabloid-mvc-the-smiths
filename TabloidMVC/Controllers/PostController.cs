using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using TabloidMVC.Models;
using TabloidMVC.Models.ViewModels;
using TabloidMVC.Repositories;

namespace TabloidMVC.Controllers
{
    [Authorize]
    public class PostController : Controller
    {
        private readonly PostRepository _postRepository;
        private readonly CategoryRepository _categoryRepository;
        private readonly TagRepository _tagRepository;
        private readonly PostTagRepository _postTagRepository;



        public PostController(IConfiguration config)
        {
            _postRepository = new PostRepository(config);
            _categoryRepository = new CategoryRepository(config);
            _tagRepository = new TagRepository(config);
            _postTagRepository = new PostTagRepository(config);



        }

        public IActionResult Index()
        {
            List<Post> posts = _postRepository.GetAllPublishedPosts();

            return View(posts);
        }

        public IActionResult MyIndex(int id)
        {
            List<Post> posts = _postRepository.GetAllPublishedPosts();

            var myPosts = new List<Post>();
            foreach(Post post in posts)
            {
                if (post.UserProfileId == GetCurrentUserProfileId())
                {
                    myPosts.Add(post);
                }       
            }

            return View(myPosts);
        }

        public IActionResult Details(int id)
        {
            var post = _postRepository.GetPublisedPostById(id);
            if (post == null)
            {
                int userId = GetCurrentUserProfileId();
                post = _postRepository.GetUserPostById(id, userId);
                if (post == null)
                {
                    return NotFound();
                }
            }
            return View(post);
        }

        public IActionResult Create()
        {
            var vm = new PostCreateViewModel();
            vm.CategoryOptions = _categoryRepository.GetAll();
            return View(vm);
        }

        [HttpPost]
        public IActionResult Create(PostCreateViewModel vm)
        {
            try
            {
                vm.Post.CreateDateTime = DateAndTime.Now;
                vm.Post.IsApproved = true;
                vm.Post.UserProfileId = GetCurrentUserProfileId();

                _postRepository.Add(vm.Post);

                return RedirectToAction("Details", new { id = vm.Post.Id });
            } 
            catch
            {
                vm.CategoryOptions = _categoryRepository.GetAll();
                return View(vm);
            }
        }

        private int GetCurrentUserProfileId()
        {
            string id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            return int.Parse(id);
        }

        [Authorize]
        public IActionResult Delete(int id)
        {
            Post post = _postRepository.GetPublisedPostById(id);

            if(post == null || post.UserProfileId != GetCurrentUserProfileId())
            {
                return NotFound();
            }

            return View(post);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id, Post post)
        {
            try
            {
                _postRepository.DeletePost(id);
                return RedirectToAction("Index");
            }
            catch
            {
                return View(post);
            }
        }

        [Authorize]
        public IActionResult Update(int id)
        {
            Post post = _postRepository.GetPublisedPostById(id);

            if (post == null || post.UserProfileId != GetCurrentUserProfileId())
            {
                return NotFound();
            }

            return View(post);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Update(int id, Post post)
        {
            try
            {
                _postRepository.UpdatePost(post);

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                return View(post);
            }
        }

        public IActionResult TagManager(int id)
        {
            var vm = new PostTagViewModel();
            vm.Tags = _tagRepository.GetAllTags();
            vm.PostTags = _postTagRepository.GetAllPostTags();
            vm.Post = _postRepository.GetUserPostById(id, GetCurrentUserProfileId());

            return View(vm);
        }

        [HttpPost]
        public IActionResult TagManager(int id, Tag tag )
        {
            var postTag = new PostTag()
            {
                PostId = id,
                TagId = tag.Id
            };
            _postTagRepository.Add(postTag);

            return RedirectToAction("TagManager");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult TagManageDelete(int id, Tag tag)
        {
                PostTag postTag = _postTagRepository.GetPostTagByPostIdAndTagId(id, tag.Id);

                _postTagRepository.DeletePostTag(postTag.Id);

                return RedirectToAction("TagManager", new { id = id });
           
        }

    }
}
