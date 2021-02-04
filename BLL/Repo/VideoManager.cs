using BLL.Base;
using BLL.Interface;
using Models;
using Models.CreateViewModels;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Repo
{
    public class VideoManager:Manager<Video>,IVideoManager
    {
        private readonly IVideoRepository repository;

        public VideoManager(IVideoRepository repository):base(repository)
        {
            this.repository = repository;
        }

        public bool Remove(RemoveFile model)
        {
            return repository.Remove(model);
        }

        public bool UploadVideo(PostImages model)
        {
            return repository.UploadVideo(model);
        }
    }
}
