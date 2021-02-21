using System;
using System.Collections.Generic;

#nullable disable

namespace SchoolPortal.Entities
{
    public partial class SchoolClassStudent
    {
        public string StudentId { get; set; }
        public Guid SchoolClassId { get; set; }

        public virtual SchoolClass SchoolClass { get; set; }
    }
}
