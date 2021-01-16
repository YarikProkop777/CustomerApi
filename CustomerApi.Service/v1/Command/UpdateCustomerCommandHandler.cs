using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CustomerApi.Data.Repository.v1;
using CustomerApi.Domain.Entities;

namespace CustomerApi.Service.v1.Command
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository)
        {
            _customerRepository = customerRepository;
        }

        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            return await _customerRepository.UpdateAsync(request.Customer);
        }
    }
}
