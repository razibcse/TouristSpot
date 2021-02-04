using Models;
using Models.CreateViewModels;
using Repositories.Base;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;
using TouristSpot.Data;

namespace Repositories.Repo
{
    public class ImageRepository : Repository<Image>, IImageRepository
    {
        public ImageRepository(ApplicationDbContext db) : base(db)
        {
        }

        public bool Remove(RemoveFile model)
        {
            var post = db.Posts.Find(model.PostId);
            if (post.OwnerID != model.OwnerId)
            {
                return false;
            }
            Image image = new Image();
            image.Id = (int)model.Id;
            image.PostId = (int)model.PostId;
            image.FilePath = model.Path;

            return base.Remove(image);
        }

        public bool UploadImages(PostImages model)
        {
            var post = db.Posts.Find(model.Id);
            if (post.OwnerID != model.OwnerId)
            {
                return false;
            }
            foreach(var im in model.files)
            {
                Image image = new Image();
                image.PostId = im.PostId;
                image.FilePath = im.FilePath;
                if (!base.Add(image)){
                    return false;
                }
            }
            
            return true;
        }
    }
}
