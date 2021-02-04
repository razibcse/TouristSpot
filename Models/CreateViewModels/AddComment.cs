using System;
using System.Collections.Generic;
using System.Text;

namespace Models.CreateViewModels
{
    public class AddComment
    {
        public string OwnerId { get; set; }
        public string Description { get; set; }

        public int PostId { get; set; }
    }
}
