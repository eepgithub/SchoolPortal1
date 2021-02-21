using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolPortal.Entities
{
    public partial class SchoolCourse
    {
        public SchoolCourse()
        {
            SchoolClassCourses = new HashSet<SchoolClassCourse>();
        }

        public Guid Id { get; set; }
        public string CourseName { get; set; }

        public virtual ICollection<SchoolClassCourse> SchoolClassCourses { get; set; }
    }
}
