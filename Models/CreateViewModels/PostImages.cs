using System;
using System.Collections.Generic;
using System.Text;

namespace Models.CreateViewModels
{
    public class PostImages
    {
        public PostImages()
        {
            files = new List<Image>();
        }
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public List<Image> files { get; set; }
    }
}
