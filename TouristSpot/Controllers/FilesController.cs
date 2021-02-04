using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.CreateViewModels;
using TouristSpot.Data;
using TouristSpot.UserServices;

namespace TouristSpot.Controllers
{
    public class FilesController : Controller
    {
        private readonly IUserService userService;
        private readonly IVideoManager videoManager;
        private readonly IWebHostEnvironment environment;

        public FilesController(
            IUserService userService,
            IVideoManager videoManager,
            IWebHostEnvironment environment
            )
        {
            this.userService = userService;
            this.videoManager = videoManager;
            this.environment = environment;
        }

        // GET: Files
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Files.ToListAsync());
        //}

        [Authorize]
        public IActionResult Remove(RemoveFile model)
        {
            if (model.Id == null || model.Path == null || model.PostId == null)
            {
                return NotFound();
            }
            model.OwnerId = userService.GetUserId();
            if (videoManager.Remove(model))
            {
                var uploadFolder = Path.Combine(environment.WebRootPath, "videos");
                System.IO.File.Delete(uploadFolder + "/" + model.Path);
            }
            return RedirectToAction("Edit", "Posts", new { id = model.PostId });
        }

        public IActionResult UploadVideo(UploadImages model)
        {
            if (ModelState.IsValid)
            {
                model.OwnerId = userService.GetUserId();
                PostImages postImages = new PostImages();
                postImages.Id = model.Id;
                postImages.OwnerId = userService.GetUserId();
                if (model.files != null)
                {
                    foreach (IFormFile image in model.files)
                    {
                        Image path = new Image();
                        var uploadFolder = Path.Combine(environment.WebRootPath, "videos");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);
                        image.CopyTo(new FileStream(filePath, FileMode.Create));
                        path.FilePath = uniqueFileName;
                        path.PostId = model.Id;
                        postImages.files.Add(path);

                    }

                    if (videoManager.UploadVideo(postImages))
                    {

                        return RedirectToAction("Edit", "Posts", new { id = model.Id });
                    }
                }

                return RedirectToAction("Edit", "Posts", new { id = model.Id });
            }
            return View();
        }

        // GET: Files/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var file = await _context.Files
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (file == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(file);
        //}

        //// GET: Files/Create
        //public IActionResult Create()
        //{
        //    return View();
        //}

        //// POST: Files/Create
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create([Bind("Id,FilePath,PostId")] Video file)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        _context.Add(file);
        //        await _context.SaveChangesAsync();
        //        return RedirectToAction(nameof(Index));
        //    }
        //    return View(file);
        //}

        //// GET: Files/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var file = await _context.Files.FindAsync(id);
        //    if (file == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(file);
        //}

        //// POST: Files/Edit/5
        //// To protect from overposting attacks, enable the specific properties you want to bind to.
        //// For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,FilePath,PostId")] Video file)
        //{
        //    if (id != file.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(file);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!FileExists(file.Id))
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
        //    return View(file);
        //}

        //// GET: Files/Delete/5
        //public async Task<IActionResult> Delete(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var file = await _context.Files
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (file == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(file);
        //}

        //// POST: Files/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var file = await _context.Files.FindAsync(id);
        //    _context.Files.Remove(file);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool FileExists(int id)
        //{
        //    return _context.Files.Any(e => e.Id == id);
        //}
    }
}
