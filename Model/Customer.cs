using System;
using mini_crm.Dto;


namespace mini_crm.Model;

public class Customer
{
    public Guid Id {get; set;}
    public int UserId {get; set;}
    public required string FirstName {get; set;}
    public required string LastName {get; set;}
    public required string Email {get; set;}
    public string? PhoneNumber {get; set;}
    public CustomerCategory CustomerTag {get; set;} = CustomerCategory.Client;
    public DateTime CreatedAt {get; set;} = DateTime.UtcNow;
    public DateTime UpdatedAt {get; set;} = DateTime.UtcNow;

    public User? User {get; set;}
    public List<ContactLogs>? ContactLogs {get; set;}
}
