using System;

namespace mini_crm.Model;

public class PartnerDetail
{
    public int Id {get; set;}
    public Guid CustomerId {get; set;}
    public string CompanyName {get; set;}
    public string? TaxIdentifierNumber {get; set;}
    public string ContractNumber {get; set;}
    public DateTime ContractEffectiveDate {get; set;}
    public DateTime ContractExpiryDate {get; set;}
    public decimal TotalRevenue {get; set;}

    public Customer? Customer {get; set;}
}
