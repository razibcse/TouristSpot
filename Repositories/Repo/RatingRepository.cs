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
    public class RatingRepository : Repository<Rating>, IRatingRepository
    {
        public RatingRepository(ApplicationDbContext db):base(db)
        {

        }

        public bool AddRating(AddRating model)
        {
            var ratings = db.Ratings.Include(u => u.UIDs).Where(pid => pid.PostId == model.PostId).FirstOrDefault();
            if (ratings == null)
            {
                Rating l = new Rating();
                l.PostId = model.PostId;
                RUID uID = new RUID();
                uID.U_ID = model.UserId;
                l.UIDs.Add(uID);

                l.TotalRating = model.RatingValue;
                l.AverageRating = model.RatingValue;
                return base.Add(l);
            }

            foreach (var u in ratings.UIDs)
            {
                if (u.U_ID == model.UserId)
                {
                    return false;
                }
            }

            RUID ID = new RUID();
            ID.U_ID = model.UserId;
            ratings.UIDs.Add(ID);
            ratings.TotalRating = ratings.TotalRating + model.RatingValue;
            double avg = (double)ratings.TotalRating / ratings.UIDs.Count;
            double avgValue = Math.Round(avg, 1);
            ratings.AverageRating = avgValue;
            
            return base.Update(ratings);

        }
    }
}
