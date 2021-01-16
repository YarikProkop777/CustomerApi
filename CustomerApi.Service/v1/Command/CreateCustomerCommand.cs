using MediatR;
using CustomerApi.Domain.Entities;

namespace CustomerApi.Service.v1.Command
{
    public class CreateCustomerCommand : IRequest<Customer>
    {
        public Customer Customer { get; set; }
    }
}
