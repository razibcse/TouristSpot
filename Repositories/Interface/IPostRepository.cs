using Models;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repositories.Interface
{
    public interface IPostRepository : IRepository<Post>
    {
        public Post GetPostDetails(int id);
        public bool DeletePostWithRelatedData(int postId);
    }
}
