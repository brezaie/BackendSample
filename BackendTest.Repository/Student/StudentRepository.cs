using System;
using System.Linq;
using System.Threading.Tasks;
using BackendTest.Common;
using BackendTest.Domain;
using BackendTest.Domain.Entity;

namespace BackendTest.Repository
{
    public class StudentRepository : GenericRepository<Student>, IStudentRepository
    {
        public CustomLogger Logger = new CustomLogger();

        public StudentRepository(BackendTestDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<Student> Insert(Student student)
        {
            try
            {
                Validate(student);

                student.CreationDate = student.ModificationDate = DateTime.Now;

                return await base.Insert(student);

            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }

        public async Task Update(Student student)
        {
            try
            {
                var doesStudentExist = GetAll().FirstOrDefault(x => x.Id == student.Id);
                if(doesStudentExist == null)
                    throw new Exception("Student does not exist!");

                Validate(student);

                student.ModificationDate = DateTime.Now;
                student.CreationDate = doesStudentExist.CreationDate;

                await base.Update(student);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }

        public async Task Delete(Student student)
        {
            try
            {
                var doesStudentExist = GetAll().FirstOrDefault(x => x.Id == student.Id);
                if (doesStudentExist == null)
                    throw new Exception("Student does not exist!");

                await base.Delete(student);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }


        private void Validate(Student student)
        {
            try
            {
                if (string.IsNullOrEmpty(student.FirstName))
                    throw new Exception("First name is empty");

                if (string.IsNullOrEmpty(student.LastName))
                    throw new Exception("Last name is empty");

                if (student.Age < 0 || student.Age > 200)
                    throw new Exception("Age is not set correctly");

                if (student.Gpa < 0 || student.Gpa > 20)
                    throw new Exception("GPA is not set correctly. It must be >= 0 OR <= 20");
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }
    }
}