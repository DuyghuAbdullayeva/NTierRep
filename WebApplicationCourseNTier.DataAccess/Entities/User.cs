﻿using Microsoft.AspNetCore.Identity;

namespace WebApplicationCourseNTier.DataAccess.Entities
{
    public class User : IdentityUser
    {
        public string FullName { get; set; }
    }
}