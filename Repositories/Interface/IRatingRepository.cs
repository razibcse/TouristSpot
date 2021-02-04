using Models;
using Models.CreateViewModels;
using Repositories.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository.Interface
{
    public interface IRatingRepository:IRepository<Rating>
    {
        public bool AddRating(AddRating model);
    }
}
