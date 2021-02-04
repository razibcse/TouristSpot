using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Rating
    {
        public Rating()
        {
            UIDs = new List<RUID>();
        }
        public int Id { get; set; }
        public int TotalRating { get; set; }
        public double AverageRating { get; set; }
        public List<RUID> UIDs { get; set; }
        public int PostId { get; set; }

    }
}
