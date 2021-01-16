using System;
using MediatR;
using CustomerApi.Domain.Entities;

namespace CustomerApi.Service.v1.Query
{
    // T - response of the query
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public Guid Id { get; set; }
    }
}
