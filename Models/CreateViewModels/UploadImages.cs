using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models.CreateViewModels
{
    public class UploadImages
    {
        public int Id { get; set; }
        public string OwnerId { get; set; }
        public List<IFormFile> files { get; set; }
    }
}
