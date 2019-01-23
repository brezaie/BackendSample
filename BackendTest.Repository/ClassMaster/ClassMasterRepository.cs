using System;
using System.Linq;
using System.Threading.Tasks;
using BackendTest.Common;
using BackendTest.Domain;
using BackendTest.Domain.Entity;

namespace BackendTest.Repository
{
    public class ClassMasterRepository : GenericRepository<ClassMaster>, IClassMasterRepository
    {
        public CustomLogger Logger = new CustomLogger();

        public ClassMasterRepository(BackendTestDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<ClassMaster> Insert(ClassMaster entity)
        {
            try
            {
                Validate(entity);

                entity.CreationDate = entity.ModificationDate = DateTime.Now;

                return await base.Insert(entity);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }

        public async Task Update(ClassMaster entity)
        {
            try
            {
                if (entity.Id <= 0)
                {
                    var ex = new Exception("Master class is not set");
                    throw ex;
                }

                var storedEntity = GetAll().FirstOrDefault(x => x.Id == entity.Id);
                if (storedEntity == null)
                {
                    var ex = new Exception("Invalid master class");
                    throw ex;
                }

                Validate(entity);

                entity.ModificationDate = DateTime.Now;
                entity.CreationDate = storedEntity.CreationDate;

                await base.Update(entity);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw;
            }
        }

        public async Task Delete(ClassMaster entity)
        {
            try
            {
                if (entity.Id <= 0)
                {
                    var ex = new Exception("Master class is not set");
                    throw ex;
                }

                var storedEntity = GetAll().FirstOrDefault(x => x.Id == entity.Id);
                if (storedEntity == null)
                {
                    var ex = new Exception("Invalid master class");
                    throw ex;
                }

                await base.Delete(entity);
            }
            catch (Exception ex)
            {
                Logger.ErrorException(ex.Message, ex);
                throw ex;
            }
        }

        private void Validate(ClassMaster entity)
        {
            if (string.IsNullOrEmpty(entity.ClassName))
            {
                var ex = new Exception("ClassName is not set");
                throw ex;
            }

            if (string.IsNullOrEmpty(entity.Location))
            {
                var ex = new Exception("Location is not set");
                throw ex;
            }

            if (string.IsNullOrEmpty(entity.TeacherName))
            {
                var ex = new Exception("Teacher's name is not set");
                throw ex;
            }
        }
    }
}