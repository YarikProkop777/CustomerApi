using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using CustomerApi.Data.Database;
using CustomerApi.Domain.Entities;

namespace CustomerApi.Data.Repository.v1
{
    public class CustomerRepository : Repository<Customer>, ICustomerRepository
    {
        public CustomerRepository(CustomerContext customerContext) : base(customerContext)
        {
        }

        public async Task<Customer> GetCustomerByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await customerContext.Customers.FirstOrDefaultAsync(customer => customer.Id == id, cancellationToken);
        }
    }
}
