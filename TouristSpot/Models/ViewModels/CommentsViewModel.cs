using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TouristSpot.Models.ViewModels
{
    public class CommentsViewModel
    {
        public int Id { get; set; }

        public string FullName { get; set; }
        public string Avatar { get; set; }
        public string Description { get; set; }
        public string ownerId { get; set; }


    }
}
