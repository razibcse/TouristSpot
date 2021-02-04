using BLL.Base;
using BLL.Interface;
using Models;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Repo
{
    public class PostManager : Manager<Post>, IPostManager
    {
        IPostRepository postRepository;

        public PostManager(IPostRepository repository) : base(repository)
        {
            postRepository = repository;
        }
        public override bool Add(Post entity)
        {


            return base.Add(entity);
        }
        public override ICollection<Post> GetAll()
        {
            return base.GetAll();
        }

        public Post GetPostDetails(int id)
        {
            return postRepository.GetPostDetails(id);
        }

        public bool DeletePostWithRelatedData(int postId)
        {
            return postRepository.DeletePostWithRelatedData(postId);
        }

    }
}
