using BLL.Base;
using Models;
using Models.CreateViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interface
{
    public interface IVideoManager:IManager<Video>
    {
        public bool Remove(RemoveFile model);
        public bool UploadVideo(PostImages model);
    }
}
