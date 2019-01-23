using BackendTest.Domain;
using BackendTest.Domain.Entity;

namespace BackendTest.Repository
{
    public class ClassDetailRepository : GenericRepository<ClassDetail>, IClassDetailRepository
    {
        public ClassDetailRepository(BackendTestDbContext dbContext) : base(dbContext)
        {
        }
    }
}