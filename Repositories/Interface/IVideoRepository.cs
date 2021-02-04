using Models;
using Models.CreateViewModels;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Interface
{
    public interface IVideoRepository:IRepository<Video>
    {
        public bool Remove(RemoveFile model);
        public bool UploadVideo(PostImages model);
    }
}

