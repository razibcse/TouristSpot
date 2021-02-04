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
    public class ImagesController : Controller
    {
        private readonly IUserService userService;
        private readonly IImageManager imageManager;
        private readonly IWebHostEnvironment environment;

        public ImagesController(
            IUserService userService,
            IImageManager imageManager,
            IWebHostEnvironment environment
            )
        {
            this.userService = userService;
            this.imageManager = imageManager;
            this.environment = environment;
        }

        // GET: Images
        //public async Task<IActionResult> Index()
        //{
        //    return View(await _context.Images.ToListAsync());
        //}

        //// GET: Images/Details/5
        //public async Task<IActionResult> Details(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var image = await _context.Images
        //        .FirstOrDefaultAsync(m => m.Id == id);
        //    if (image == null)
        //    {
        //        return NotFound();
        //    }

        //    return View(image);
        //}

        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UploadImage(UploadImages model)
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
                        var uploadFolder = Path.Combine(environment.WebRootPath, "images");
                        string uniqueFileName = Guid.NewGuid().ToString() + "_" + image.FileName;
                        string filePath = Path.Combine(uploadFolder, uniqueFileName);
                        image.CopyTo(new FileStream(filePath, FileMode.Create));
                        path.FilePath = uniqueFileName;
                        path.PostId = model.Id;
                        postImages.files.Add(path);
                        
                    }
                    //System.IO.File.close();
                    if (imageManager.UploadImages(postImages))
                    {

                        return RedirectToAction("Edit", "Posts", new { id = model.Id });
                    }
                }

                return RedirectToAction("Edit", "Posts", new { id = model.Id });
            }
            return View();
        }

        // GET: Images/Edit/5
        //public async Task<IActionResult> Edit(int? id)
        //{
        //    if (id == null)
        //    {
        //        return NotFound();
        //    }

        //    var image = await _context.Images.FindAsync(id);
        //    if (image == null)
        //    {
        //        return NotFound();
        //    }
        //    return View(image);
        //}

        // POST: Images/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Edit(int id, [Bind("Id,FilePath,PostId")] Image image)
        //{
        //    if (id != image.Id)
        //    {
        //        return NotFound();
        //    }

        //    if (ModelState.IsValid)
        //    {
        //        try
        //        {
        //            _context.Update(image);
        //            await _context.SaveChangesAsync();
        //        }
        //        catch (DbUpdateConcurrencyException)
        //        {
        //            if (!ImageExists(image.Id))
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
        //    return View(image);
      //  }

        [Authorize]
        public IActionResult Remove(RemoveFile model)
        {
            if (model.Id == null || model.Path == null || model.PostId == null)
            {
                return NotFound();
            }
            model.OwnerId = userService.GetUserId();
            if (imageManager.Remove(model))
            {
                var uploadFolder = Path.Combine(environment.WebRootPath, "images");
                //System.IO.File.
                try
                {
                    System.IO.File.Delete(uploadFolder + "/" + model.Path);
                }
                catch
                {
                    return RedirectToAction("Edit", "Posts", new { id = model.PostId });
                }
            }
            //int id =(int) model.Id;
            //string rout = "Edit/" + id;
            return RedirectToAction("Edit","Posts",new { id=model.PostId});
        }

        // POST: Images/Delete/5
        //[HttpPost, ActionName("Delete")]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> DeleteConfirmed(int id)
        //{
        //    var image = await _context.Images.FindAsync(id);
        //    _context.Images.Remove(image);
        //    await _context.SaveChangesAsync();
        //    return RedirectToAction(nameof(Index));
        //}

        //private bool ImageExists(int id)
        //{
        //    return _context.Images.Any(e => e.Id == id);
        //}
    }
}
