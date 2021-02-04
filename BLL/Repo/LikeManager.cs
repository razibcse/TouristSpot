using BLL.Base;
using BLL.Interface;
using Repository.Repo;
using System;
using System.Collections.Generic;
using System.Text;
using Repository.Interface;
using Models;
using Models.CreateViewModels;

namespace BLL.Repo
{
    public class LikeManager :Manager<Like>, ILikeManager
    {
        ILikeRepository likeRepository;
        public LikeManager(ILikeRepository likeRepository) :base(likeRepository)
        {
            this.likeRepository = likeRepository;
        }
        public bool AddLike(AddLike like)
        {
            return likeRepository.AddLike(like);
        }
    }
}
