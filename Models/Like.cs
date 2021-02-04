using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
    public class Like
    {
        public Like()
        {
            UIDs = new List<UID>();
        }
        public int Id { get; set; }
        public int TotalCount { get; set; }

        public List<UID> UIDs { get; set; }

        public int PostId { get; set; }

    }
}
