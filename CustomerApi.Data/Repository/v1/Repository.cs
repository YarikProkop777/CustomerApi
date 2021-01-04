using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CustomerApi.Data.Database;

namespace CustomerApi.Data.Repository.v1
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class, new()
    {
        protected readonly CustomerContext customerContext;

        public Repository(CustomerContext customerContext)
        {
            this.customerContext = customerContext;
        }

        public IEnumerable<TEntity> GetAll()
        {
            try
            {
                return customerContext.Set<TEntity>();
            }
            catch (Exception ex)
            {
                throw new Exception($"Couldn't retrieve entities: {ex.Message}");
            }
        }

        public async Task<TEntity> AddAsync(TEntity entity)
        {
            if(entity == null)
            {
                throw new ArgumentException($"{nameof(AddAsync)} entity must not be null");
            }

            try
            {
                await customerContext.AddAsync(entity);
                await customerContext.SaveChangesAsync();

                // we returned entity that was be saved previously
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} couldn't be saved: {ex.Message}");
            }
        }

        public async Task<TEntity> UpdateAsync(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentException($"{nameof(UpdateAsync)} entity must not be null");
            }

            try
            {
                customerContext.Update(entity);
                await customerContext.SaveChangesAsync();

                // we returned entity that was be updated previously
                return entity;
            }
            catch (Exception ex)
            {
                throw new Exception($"{nameof(entity)} couldn't be updated: {ex.Message}");
            }
        }
    }
}
