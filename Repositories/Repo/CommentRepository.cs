using Models;
using Models.CreateViewModels;
using Repositories.Base;
using Repository.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TouristSpot.Data;

namespace Repository.Repo
{
    public class CommentRepository:Repository<Comment>,ICommentRepository
    {
        public CommentRepository(ApplicationDbContext db):base(db)
        {
           
        }

        public bool AddComment(AddComment model)
        {
            Comment com = new Comment();
            com.PostId = model.PostId;
            com.OwnerId = model.OwnerId;
            com.Description = model.Description;
            return base.Add(com);
        }

    }
}
