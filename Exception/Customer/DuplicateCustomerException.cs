using System;

namespace mini_crm.Exception.Customer;

public class DuplicateCustomerException : System.Exception
{
    public DuplicateCustomerException () : base(
        $"Customer already existed"
    ) {}
}
