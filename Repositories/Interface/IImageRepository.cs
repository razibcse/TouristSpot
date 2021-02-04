using Models;
using Models.CreateViewModels;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Interface
{
    public interface IImageRepository:IRepository<Image>
    {
        public bool Remove(RemoveFile model);
        public bool UploadImages(PostImages model);

    }
}
