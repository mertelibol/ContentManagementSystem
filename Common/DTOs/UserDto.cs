﻿using System;
using System.Collections.Generic;
using System.Text;

namespace Common.DTOs
{
    public class UserDto
    {

        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DisplayName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int AuthorityId { get; set; }
        public string Image { get; set; }


    }
}
