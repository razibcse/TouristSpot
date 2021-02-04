using BLL.Base;
using Models;
using Models.CreateViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interface
{
    public interface IImageManager:IManager<Image>
    {
        public bool Remove(RemoveFile model);
        public bool UploadImages(PostImages model);
    }
}

