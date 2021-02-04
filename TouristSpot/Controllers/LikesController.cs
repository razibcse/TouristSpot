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
    public class LikesController : Controller
    {
        private readonly ILikeManager likeManager;
        private readonly IUserService userService;

        public LikesController(
            ILikeManager likeManager,
            IUserService userService)
        {
            this.likeManager = likeManager;
            this.userService = userService;
        }

        [Authorize]
        public IActionResult AddLike(AddLike model)
        {
            model.UserId = userService.GetUserId();
            bool isLikeAdded= likeManager.AddLike(model);
            if (isLikeAdded)
            {
                return RedirectToAction("Index", "Posts");
            }
            return RedirectToAction("Index", "Posts");
        }

        // GET: Likes
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Likes.ToListAsync());
        //}

        //// GET: Likes/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var like = await _context.Likes
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (like == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(like);
        //}

        // GET: Likes/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        // POST: Likes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,TotalCount,PostId")] Like like)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(like);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(like);
        //}

        // GET: Likes/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var like = await _context.Likes.FindAsync(id);
        //    if (like == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(like);
        //}

        // POST: Likes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,TotalCount,PostId")] Like like)
        //{
        //    if (id != like.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(like);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!LikeExists(like.Id))
        //            {
        //                return NotFound();
        //            }
        //            else
        //            {
        //                throw;
        //            }
        //        }
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(like);
        //}

        // GET: Likes/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var like = await _context.Likes
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (like == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(like);
        //}

        // POST: Likes/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var like = await _context.Likes.FindAsync(id);
        //    _context.Likes.Remove(like);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool LikeExists(int id)
        //{
        //    return _context.Likes.Any(e => e.Id == id);
        //}
    }
}
