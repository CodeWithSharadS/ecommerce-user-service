namespace eCommerce.Core.Entities;

public class ApplicationUser
{
    public Guid User_Id { get; set; }
    public string? Email { get; set; }
    public string? Password { get; set; }
    public string? Person_Name { get; set; }
    public string? Gender { get; set; }
}
