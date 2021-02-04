using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class Image
    {
        public int Id { get; set; }
        public string FilePath { get; set; }
        public int PostId { get; set; }
    }
}
