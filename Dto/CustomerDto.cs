using System;

namespace mini_crm.Dto;

public class CustomerDto
{
    public Guid CustomerId {get; set;}
    public string? FirstName {get; set;}
    public string? LastName {get; set;}
    public string? Email {get; set;}
    public string? PhoneNumber {get; set;}
    public CustomerCategory? CustomerTag {get; set;}
<<<<<<< HEAD
    public DateTime LastUpdated {get; set;}
=======
>>>>>>> origin/main
}
