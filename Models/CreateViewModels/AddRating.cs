using System;
using System.Collections.Generic;
using System.Text;

namespace Models.CreateViewModels
{
    public class AddRating
    {
        public int PostId { get; set; }
        public string UserId { get; set; }
        public int RatingValue { get; set; }

    }
}
