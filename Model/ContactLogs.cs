using System;
using mini_crm.Dto;

namespace mini_crm.Model;

public class ContactLogs
{
    public int Id {get; set;}
    public int CustomerId {get; set;}
    public DateTime ContactDate {get; set;}
    public ContactMethod ContactMethod {get; set;}
    public string? Note {get; set;}

    public Customer? Customer {get; set;}
}
