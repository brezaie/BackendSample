using System;
using System.Collections.Generic;

namespace BackendTest.Domain.Entity
{
    public class ClassDetail
    {
        public int Id { get; set; }
        public int ClassMasterId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        public ClassMaster ClassMaster { get; set; }
        public ICollection<ClassDetailStudent> ClassDetailStudents { get; set; }
    }
}
