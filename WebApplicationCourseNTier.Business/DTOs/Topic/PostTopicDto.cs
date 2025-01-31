﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebApplicationCourseNTier.DataAccess.Entities;

namespace WebApplicationCourseNTier.Business.DTOs.Topic
{
    public class PostTopicDto
    {
        public string Name { get; set; }
        public int SubjectId { get; set; }
        public List<int> LessonIds { get; set; }
    }
}
