using BLL.Base;
using Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Interface
{
    public interface IPostManager:IManager<Post>
    {
        public Post GetPostDetails(int id);
        public bool DeletePostWithRelatedData(int postId);
    }
}
