using Microsoft.EntityFrameworkCore;
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
    public class LikeRepository : Repository<Like>, ILikeRepository
    {
        public LikeRepository(ApplicationDbContext db) : base(db)
        {
        }

        public bool AddLike(AddLike like)
        {
            var likes = db.Likes.Include(u => u.UIDs).Where(pid => pid.PostId == like.PostId).FirstOrDefault();

            // var users = db.uIDs.Where(l => l.LikeId != null).ToList();

            if (likes == null)
            {
                Like l = new Like();
                l.PostId = like.PostId;
                UID uID = new UID();
                uID.U_ID = like.UserId;
                l.UIDs.Add(uID);
                l.TotalCount = 1;
                return base.Add(l);
            }

            foreach(var u in likes.UIDs)
            {
                if (u.U_ID == like.UserId)
                {
                    return false;
                }
            }

            likes.TotalCount = likes.TotalCount + 1;
            UID ID = new UID();
            ID.U_ID = like.UserId;
            likes.UIDs.Add(ID);

            return base.Update(likes);
        }
    }
}
