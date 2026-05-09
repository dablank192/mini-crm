using System;

namespace mini_crm.Model;

public class VendorDetail
{
    public int Id {get; set;}
    public Guid CustomerId {get; set;}
    public string? CompanyName {get; set;}
    public string? TaxIdentifierNumber {get; set;}
    public string? WebsiteUrl {get; set;}
    public string? BankAccountNumber {get; set;}
    public string? BankName {get; set;}
    public decimal CurrentDebt {get; set;}

    public Customer? Customer {get; set;}
}
