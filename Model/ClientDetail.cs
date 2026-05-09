using System;
using mini_crm.Dto;

namespace mini_crm.Model;

public class ClientDetail
{
    public int Id {get; set;}
    public required Guid CustomerId {get; set;}
    public required ClientType Type {get; set;}
    public string? CompanyName {get; set;} //if client type is Corporate, else null
    public DateOnly? DateOfBirth {get; set;}
    public decimal TotalSpent {get; set;}
    public DateTime LatestPurchaseDate {get; set;}

    public Customer? Customer {get; set;}
}   
