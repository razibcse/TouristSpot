using System;
using System.Collections.Generic;
using System.Text;

namespace Models.CreateViewModels
{
    public class RemoveFile
    {
        public int? Id { get; set; }
        public string Path { get; set; }
        public int? PostId { get; set; }
        public string OwnerId { get; set; }

    }
}
