using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.CreateViewModels;
using TouristSpot.Data;
using TouristSpot.UserServices;

namespace TouristSpot.Controllers
{
    public class CommentsController : Controller
    {
        private readonly IUserService userService;
        private readonly ICommentManager commentManager;

        public CommentsController(ApplicationDbContext context,
            IUserService userService,
            ICommentManager commentManager)
        {
            this.userService = userService;
            this.commentManager = commentManager;
        }

        // GET: Comments
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Comments.ToListAsync());
        //}

        // GET: Comments/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var comment = await _context.Comments
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (comment == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(comment);
        //}

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult AddComment(AddComment model)
        {
            if (ModelState.IsValid)
            {
                model.OwnerId = userService.GetUserId();
                if (commentManager.AddComment(model))
                {
                    return RedirectToAction("Index", "Posts");
                }
            }
            return RedirectToAction("Index", "Posts");
        }


        // GET: Comments/Edit/5
        [Authorize]
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }
            var comment = commentManager.GetById((int)id);
            if (comment.OwnerId != userService.GetUserId())
            {
                return Forbid();
            }
            return View(comment);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, [Bind("Id,OwnerId,Description,PostId")] Comment comment)
        {
            if (id != comment.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    commentManager.Update(comment);
                }
                catch (DbUpdateConcurrencyException)
                {
                    
                }
                return View(comment);
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var comment = commentManager.GetById((int)id);
            if (comment.OwnerId != userService.GetUserId())
            {
                return Forbid();
            }
            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            var comment = commentManager.GetById((int)id);
            commentManager.Remove(comment);
            return RedirectToAction("Index","Posts");
        }

        //private bool CommentExists(int id)
        //{
        //    return _context.Comments.Any(e => e.Id == id);
        //}
    }
}
