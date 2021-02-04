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
    public class ImageManager : Manager<Image>, IImageManager
    {
        private IImageRepository repository;
        public ImageManager(IImageRepository repository):base(repository)
        {
            this.repository = repository;     
        }
        public bool Remove(RemoveFile model)
        {
            return repository.Remove(model);
        }

        public bool UploadImages(PostImages model)
        {
            return repository.UploadImages(model);
        }
    }
}
