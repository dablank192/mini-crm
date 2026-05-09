using System;

namespace mini_crm.Exception.Customer;

public class CustomerIdNotExistedException : System.Exception
{
    public CustomerIdNotExistedException(Guid customerId) : base (
        $"Customer id: '{customerId}' not existed"
    ){}
}
