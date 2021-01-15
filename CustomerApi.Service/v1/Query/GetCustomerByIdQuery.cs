﻿using System;
using CustomerApi.Domain.Entities;
using MediatR;

namespace CustomerApi.Service.v1.Query
{
    // T - response of the query
    public class GetCustomerByIdQuery : IRequest<Customer>
    {
        public Guid Id { get; set; }
    }
}
