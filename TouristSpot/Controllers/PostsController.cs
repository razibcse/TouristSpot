using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using TouristSpot.Data;
using TouristSpot.Models.ViewModels;
using TouristSpot.UserServices;

namespace TouristSpot.Controllers
{
    [Authorize]
    public class PostsController : Controller
    {
        private readonly IWebHostEnvironment environment;
        private readonly IPostManager postManager;
        private readonly IUserService userService;
        private readonly UserManager<ApplicationUser> userManager;

        public PostsController(
            IWebHostEnvironment environment,
            IPostManager postManager,
            IUserService userService,
            UserManager<ApplicationUser> userManager)
        {
            this.environment = environment;
            this.postManager = postManager;
            this.userService = userService;
            this.userManager = userManager;
        }

        // GET: Posts
        public IActionResult Index()
        {
            var posts = postManager.GetAll();
            var models = new List<IndexViewModel>();
            foreach(var post in posts)
            {
                IndexViewModel model = new IndexViewModel();
                var user = userManager.FindByIdAsync(post.OwnerID).Result;
                model.FullName = user.FullName;
                model.Avatar = user.Avatar;
                model.Title = post.Title;
                model.Description = post.Description;
                model.DateTime = post.DateTime.ToString();
                model.PostId = post.Id;
                model.Country = post.Country;
                model.City = post.City;
                if (post.Like != null)
                {
                    model.Like = post.Like.TotalCount;
                }
                else
                {
                    model.Like = 0;
                }

                if (post.Rating != null)
                {
                    model.AvgRating = post.Rating.AverageRating;
                    model.TotalRatingCount = post.Rating.UIDs.Count;
                }
                else
                {
                    model.AvgRating = 0;
                    model.TotalRatingCount = 0;
                }

                List<string> imagesPath = new List<string>();
                if (post.Images != null)
                {
                    foreach(var image in post.Images)
                    {
                        imagesPath.Add(image.FilePath);

                    }
                    model.ImagePath = imagesPath;
                }
                List<string> videoPath = new List<string>();
                if (post.Videos != null)
                {
                    foreach(var video in post.Videos)
                    {
                        videoPath.Add(video.FilePath);

                    }
                    model.VideoPath = videoPath;
                }
                if (post.Comments != null)
                {
                    foreach(var c in post.Comments)
                    {
                        var c_user = userManager.FindByIdAsync(c.OwnerId).Result;
                        CommentsViewModel commentsView = new CommentsViewModel();
                        commentsView.Avatar = c_user.Avatar;
                        commentsView.FullName = c_user.FullName;
                        commentsView.Description = c.Description;
                        commentsView.Id = c.Id;
                        commentsView.ownerId = c.OwnerId;
                        model.Comments.Add(commentsView);
                    }
                }
                models.Add(model);
            }
            return View(models);
        }

        // GET: Posts/Details/5
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = postManager.GetPostDetails((int)id);

            if (post == null)
            {
                return NotFound();
            }
            ViewData["id"] = userService.GetUserId();
            return View(post);
        }

        // GET: Posts/Create
        [Authorize]
        public IActionResult Create()
        {
            return View();
        }

        // POST: Posts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(CreatePostModel model)
        {
            if (ModelState.IsValid)
            {
                Post post = new Post();
                if (model.Images != null)
                {
                    foreach (IFormFile image in model.Images)
                    {
                        Image path = new Image();
                        var uploadFolder = Path.Combine(environment.WebRootPath, "images");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);
                        image.CopyTo(new FileStream(filePath, FileMode.Create));
                        path.FilePath = uniqueFileName;
                        post.Images.Add(path);
                    }
                }
                if (model.Videos != null)
                {
                    foreach (IFormFile video in model.Videos)
                    {
                        Video path = new Video();
                        var uploadFolder = Path.Combine(environment.WebRootPath, "videos");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + video.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);
                        video.CopyTo(new FileStream(filePath, FileMode.Create));
                        path.FilePath = uniqueFileName;
                        post.Videos.Add(path);
                    }
                }

                post.Title = model.Title;
                post.Description = model.Description;
                post.DateTime = System.DateTime.Now;
                post.Country = model.Country;
                post.City = model.City;
                post.OwnerID = userService.GetUserId();

                bool isAdded=postManager.Add(post);
                if (isAdded)
                return RedirectToAction(nameof(Index));
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(model);
        }

        // GET: Posts/Edit/5
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = postManager.GetPostDetails((int)id);
            if (post == null)
            {
                return NotFound();
            }
            if (post.OwnerID != userService.GetUserId())
            {
                return Forbid();
            }
            return View(post);
        }

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id,
            [Bind("Id,OwnerID,Title,Description,Country,City,DateTime")] Post post)
        {
            if (id != post.Id)
            {
                return NotFound();
            }
            
            if (ModelState.IsValid)
            {
                try
                {
                    if (postManager.Update(post))
                    {
                        return RedirectToAction("Edit", "Posts", new { id = post.Id });
                    }
                    
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
            }
            return RedirectToAction("Edit", "Posts", new { id = post.Id });
        }

        // GET: Posts/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var post = postManager.GetPostDetails((int)id);

            if (post == null)
            {
                return NotFound();
            }
            if (post.OwnerID != userService.GetUserId())
            {
                return Forbid();
            }
            return View(post);
        }

        // POST: Posts/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Deletes(int id)
        {
            postManager.DeletePostWithRelatedData(id);

            return RedirectToAction(nameof(Index));
        }


        //private bool PostExists(int id)
        //{
        //    return _context.Posts.Any(e => e.Id == id);
        //}
    }
}
