using BLL.Base;
using BLL.Interface;
using Models;
using Models.CreateViewModels;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Text;

namespace BLL.Repo
{
    public class RatingManager : Manager<Rating>, IRatingManager
    {
        IRatingRepository ratingRepository;
        public RatingManager(IRatingRepository repository):base(repository)
        {
            ratingRepository = repository;
        }

        public bool AddRating(AddRating model)
        {
            return ratingRepository.AddRating(model);
        }
    }
}
