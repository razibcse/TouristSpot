﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public string Description { get; set; }

        public int PostId { get; set; }

    }
}