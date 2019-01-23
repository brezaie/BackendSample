using System;
using System.Collections.Generic;

namespace BackendTest.Domain.Entity
{
    public class ClassMaster
    {
        public ClassMaster()
        {
            ClassDetail = new HashSet<ClassDetail>();
        }

        public int Id { get; set; }
        public string ClassName { get; set; }
        public string TeacherName { get; set; }
        public string Location { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        public ICollection<ClassDetail> ClassDetail { get; set; }
    }
}
