namespace eCommerce.Core.DTO;

public record AuthenticationResponse
(
    Guid UserID,
    string? Email,
    string? PersoneName,
    string? Gender,
    string? Token,        
    bool Success
);

//use it when we dont want to use ConstructUsing in ApplicationUserMappingProfile

/*
public record AuthenticationResponse
(
    Guid UserID,
    string? Email,
    string? PersoneName,
    string? Gender,
    string? Token,
    bool Success
)
{
    public AuthenticationResponse() : this(default, default, default, default, default, default)
    {

    }
}
*/