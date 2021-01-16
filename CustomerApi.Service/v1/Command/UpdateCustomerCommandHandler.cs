using System.Threading;
using System.Threading.Tasks;
using MediatR;
using CustomerApi.Data.Repository.v1;
using CustomerApi.Domain.Entities;
using CustomerApi.Messaging.Send.Sender.v1;

namespace CustomerApi.Service.v1.Command
{
    public class UpdateCustomerCommandHandler : IRequestHandler<UpdateCustomerCommand, Customer>
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly ICustomerUpdateSender _customerUpdateSender;

        public UpdateCustomerCommandHandler(ICustomerRepository customerRepository, ICustomerUpdateSender customerUpdateSender)
        {
            _customerRepository = customerRepository;
            _customerUpdateSender = customerUpdateSender;
        }

        public async Task<Customer> Handle(UpdateCustomerCommand request, CancellationToken cancellationToken)
        {
            var customer = await _customerRepository.UpdateAsync(request.Customer);

            // publish data to queue
            _customerUpdateSender.SendCustomer(customer);

            return customer;
        }
    }
}
