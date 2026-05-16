using System;
using System.Text.Json.Serialization;
using mini_crm.Feature.Customer.GetCustomerInfo;

namespace mini_crm.Dto;

[JsonPolymorphic(TypeDiscriminatorPropertyName ="type")]
[JsonDerivedType(typeof(ClientInfoDto), typeDiscriminator: "client")]
[JsonDerivedType(typeof(VendorInfoDto), typeDiscriminator: "vendor")]
[JsonDerivedType(typeof(PartnerInfoDto), typeDiscriminator: "partner")]

public class CustomerInfoResponseDto
{
    public Guid CustomerId {get; set;}
}

public class ClientInfoDto : CustomerInfoResponseDto
{
    public ClientType ClientType {get; set;}
    public string? CompanyName {get; set;}
    public DateOnly? DateOfBirth {get; set;}
    public decimal TotalSpent {get; set;}
    public DateTime? LatestPurchaseDate {get; set;}
}

public class VendorInfoDto : CustomerInfoResponseDto
{
    public string? CompanyName {get; set;}
    public string? TaxIdentifierNumber {get; set;}
    public string? WebsiteUrl {get; set;}
    public string? BankAccountNumber {get; set;}
    public string? BankName {get; set;}
    public decimal? CurrentDebt {get; set;}
}

public class PartnerInfoDto : CustomerInfoResponseDto
{
    public string? CompanyName {get; set;}
    public string? TaxIdentifierNumber {get; set;}
    public string? ContractNumber {get; set;}
    public DateOnly? ContractEffectiveDate {get; set;}
    public DateOnly? ContractExpiryDate {get; set;}
    public decimal? TotalRevenue {get; set;}
}
