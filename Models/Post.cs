using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Post
    {
        public Post()
        {
            Images = new List<Image>();
            Videos = new List<Video>();
            Comments = new List<Comment>();
        }
        public int Id { get; set; }
        public string OwnerID { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime DateTime { get; set; }
        public string Country { get; set; }
        public string City { get; set; }


        public List<Image> Images { get; set; }
        public List<Video> Videos { get; set; }
        public List<Comment> Comments { get; set; }
        public Like Like { get; set; }
        public Rating Rating { get; set; }

    }
}
