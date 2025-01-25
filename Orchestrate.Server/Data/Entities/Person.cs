namespace Orchestrate.Server.Data.Entities
{
  public class Person : ITenantSpecificEntity
  {
    public int PersonId { get; set; }
    public string TenantId { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string? PreferredName { get; set; }
    public string? Email { get; set; }
    public string? Phone { get; set; }
    public Address? Address { get; set; }
    public bool IsActive { get; set; }
  }

  public class Address
  {
    public string Street { get; set; }
    public string City { get; set; }
    public string State { get; set; }
    public string PostalCode { get; set; }
    public string Country { get; set; }
  }
}
