using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Models
{
    public class ApplicationUser: IdentityUser
    {
        public string FullName { get; set; }
        public string Country { get; set; }
        public string City { get; set; }
        public string Avatar { get; set; }

    }
}
