using System;
using System.ComponentModel.DataAnnotations;

namespace BackendTest.Domain.Entity
{
    public class ClassDetailStudent
    {
        public int Id { get; set; }
        public int ClassDetailId { get; set; }
        public int StudentId { get; set; }
        public DateTime CreationDate { get; set; }
        public DateTime ModificationDate { get; set; }

        public ClassDetail ClassDetail { get; set; }
        public Student Student { get; set; }

    }
}