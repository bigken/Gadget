using System;
using System.Collections.Generic;
using System.Text;

namespace Gadget.IService.Models
{
    public class AuthorModel
    {
        public long AuthorId { get; set; }

        public string AuthorName { get; set; }

        public string AvatarUrl { get; set; }

        public string Email { get; set; }
    }
}
