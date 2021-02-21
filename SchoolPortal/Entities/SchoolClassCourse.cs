using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolPortal.Entities
{
    public partial class SchoolClassCourse
    {
        public Guid SchoolClassId { get; set; }
        public Guid SchoolCourseId { get; set; }
        public string TeacherId { get; set; }

        public virtual SchoolClass SchoolClass { get; set; }
        public virtual SchoolCourse SchoolCourse { get; set; }
    }
}
