using Microsoft.EntityFrameworkCore;
using Models;
using Repositories.Base;
using Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TouristSpot.Data;

namespace Repository.Repo
{
    public class PostRepository : Repository<Post>, IPostRepository
    {
        public PostRepository(ApplicationDbContext db) : base(db)
        {

        }
        public override bool Add(Post entity)
        {
            
            return base.Add(entity);
        }

        public bool DeletePostWithRelatedData(int postId)
        {
            var like= db.Likes.Where(pid => pid.PostId == postId).FirstOrDefault();
            db.Likes.Remove(like);
            var lusers = db.uIDs.Where(id => id.LikeId == like.Id).ToList();
            
            foreach(var user in lusers)
            {
                db.uIDs.Remove(user);
            }

            var rating= db.Ratings.Where(pid => pid.PostId == postId).FirstOrDefault();
            db.Ratings.Remove(rating);
            var rusers = db.rUIDs.Where(id => id.RatingId == rating.Id).ToList();
            foreach(var user in rusers)
            {
                db.rUIDs.Remove(user);
            }
            var comments = db.Comments.Where(pid => pid.PostId == postId).ToList();
            foreach(var comment in comments)
            {
                db.Comments.Remove(comment);
            }

            var post = db.Posts.Where(pid => pid.Id == postId).FirstOrDefault();
            db.Posts.Remove(post);

            return db.SaveChanges()>0;
        }

        public override ICollection<Post> GetAll()
        {
            var posts = db.Posts
                .Include(i => i.Images)
                .Include(v => v.Videos)
                .Include(c => c.Comments)
                .Include(l => l.Like)
                .Include(r => r.Rating).ThenInclude(u=>u.UIDs)
                .AsNoTracking().ToList();

            return posts;
        }

        public Post GetPostDetails(int id)
        {
            var post = db.Posts.Where(pid => pid.Id == id)
                .Include(i => i.Images)
                .Include(v => v.Videos).FirstOrDefault();

            return post;
        }
    }
}
