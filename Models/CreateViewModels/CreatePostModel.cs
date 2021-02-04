using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Http;

namespace Models
{
    public class CreatePostModel
    {
        public CreatePostModel()
        {
            Images = new List<IFormFile>();
            Videos = new List<IFormFile>();
        }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public List<IFormFile> Images { get; set; }
        public List<IFormFile> Videos { get; set; }

    }
}
