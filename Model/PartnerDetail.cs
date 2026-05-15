using System;

namespace mini_crm.Model;

public class PartnerDetail
{
    public int Id {get; set;}
    public Guid CustomerId {get; set;}
    public required string CompanyName {get; set;}
    public string? TaxIdentifierNumber {get; set;}
    public required string ContractNumber {get; set;}
    public DateOnly ContractEffectiveDate {get; set;}
    public DateOnly ContractExpiryDate {get; set;}
    public decimal TotalRevenue {get; set;}

    public Customer? Customer {get; set;}
}
