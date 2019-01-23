
using System.Threading.Tasks;
using BackendTest.Domain.Entity;

namespace BackendTest.Repository
{
    public interface IStudentRepository : IGenericRepository<Domain.Entity.Student>
    {
        Task<Student> Insert(Student student);

        Task Update(Student student);

        Task Delete(Student student);
    }
}