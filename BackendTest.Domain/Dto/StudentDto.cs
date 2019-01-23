namespace BackendTest.Domain.Dto
{
    public class StudentDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int Age { get; set; }
        public decimal Gpa { get; set; }
        public string FullName => $"{FirstName} {LastName}";
    }
}