using System;
using System.Collections.Generic;
using BackendTest.Domain.Entity;

namespace BackendTest.Domain.Dto
{
    public class ClassDetailDto
    {
        public int Id { get; set; }
        public int ClassDetailId { get; set; }
        public int ClassMasterId { get; set; }
        public string ClassName { get; set; }
        public List<StudentDto> StudentDtos { get; set; }
    }
}