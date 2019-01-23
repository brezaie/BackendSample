
using System.Threading.Tasks;
using BackendTest.Domain.Entity;

namespace BackendTest.Repository
{
    public interface IClassMasterRepository : IGenericRepository<ClassMaster>
    {
        Task<ClassMaster> Insert(ClassMaster entity);

        Task Update(ClassMaster entity);

        Task Delete(ClassMaster entity);
    }
}