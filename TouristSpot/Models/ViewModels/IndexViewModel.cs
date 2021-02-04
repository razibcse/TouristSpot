using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristSpot.Models.ViewModels
{
    public class IndexViewModel
    {

        public IndexViewModel()
        {
            ImagePath = new List<string>();
            VideoPath = new List<string>();
            Comments = new List<CommentsViewModel>();
        }

        public int PostId { get; set; }

        public string FullName { get; set; }
        public string Avatar { get; set; }

        public string Title { get; set; }
        public string Description { get; set; }
        public string DateTime { get; set; }

        public string Country { get; set; }
        public string City { get; set; }



        public List<string> ImagePath { get; set; }
        public List<string> VideoPath { get; set; }

        public int Like { get; set; }
        public double AvgRating { get; set; }
        public int TotalRatingCount { get; set; }

        public List<CommentsViewModel> Comments { get; set; }


    }
}
