using BackendTest.Domain;

namespace BackendTest.Repository.ClassDetailStudent
{
    public class ClassDetailStudentRepository : GenericRepository<Domain.Entity.ClassDetailStudent>, IClassDetailStudentRepository
    {
        public ClassDetailStudentRepository(BackendTestDbContext dbContext) : base(dbContext)
        {
        }
    }
}