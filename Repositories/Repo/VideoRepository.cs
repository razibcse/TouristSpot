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
    public class VideoRepository:Repository<Video>,IVideoRepository
    {
        public VideoRepository(ApplicationDbContext db):base(db)
        {

        }

        public bool Remove(RemoveFile model)
        {
            var post = db.Posts.Find(model.PostId);
            if (post.OwnerID != model.OwnerId)
            {
                return false;
            }

            Video video = new Video();
            video.Id = (int)model.Id;
            video.PostId = (int)model.PostId;
            video.FilePath = model.Path;

            return base.Remove(video);
        }

        public bool UploadVideo(PostImages model)
        {
            var post = db.Posts.Find(model.Id);
            if (post.OwnerID != model.OwnerId)
            {
                return false;
            }
            foreach (var im in model.files)
            {
                Video image = new Video();
                image.PostId = im.PostId;
                image.FilePath = im.FilePath;
                if (!base.Add(image))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
