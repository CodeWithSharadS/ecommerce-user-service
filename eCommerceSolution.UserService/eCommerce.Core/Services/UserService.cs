using AutoMapper;
using eCommerce.Core.DTO;
using eCommerce.Core.Entities;
using eCommerce.Core.RepositoryContracts;
using eCommerce.Core.ServiceContracts;

namespace eCommerce.Core.Services;

internal class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IMapper mapper) 
    {
        _userRepository = userRepository; 
        _mapper = mapper;
    }

    public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
    {
        ApplicationUser? applicationUser =  await _userRepository.GetUserByEmailAndPassword(loginRequest.Email, loginRequest.Password);

        if (applicationUser == null) 
        {
            return null;
        }

        //return new AuthenticationResponse(applicationUser.UserId, applicationUser.Email, applicationUser.PersonName, applicationUser.Gender, "token", Success : true);

        return _mapper.Map<AuthenticationResponse>(applicationUser) with { Success = true, Token = "token" };
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {
        //ApplicationUser applicationUser = new ApplicationUser()
        //{
        //    PersonName = registerRequest.PersonName,
        //    Email = registerRequest.Email,
        //    Password = registerRequest.Password,
        //    Gender = registerRequest.Gender.ToString()
        //};    

        ApplicationUser applicationUser = _mapper.Map<ApplicationUser>(registerRequest);

         ApplicationUser? registerUser = await _userRepository.AddUser(applicationUser);

        if (registerUser == null)
        {
            return null;
        }

        //return new AuthenticationResponse(registerUser.UserId, registerUser.Email, 
        //                    registerUser.PersonName, registerUser.Gender, "token", Success : true);

       return  _mapper.Map<AuthenticationResponse>(registerUser) with { Success = true, Token = "token"};
    }
}
