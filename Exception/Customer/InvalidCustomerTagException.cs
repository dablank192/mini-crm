using System;
using mini_crm.Dto;

namespace mini_crm.Exception.Customer;

public class InvalidCustomerTagException : System.Exception
{
    public InvalidCustomerTagException(CustomerCategory customerCategory) : base(
        $"Customer tag is either null or invalid: '{customerCategory}'"
    ) {}
}
