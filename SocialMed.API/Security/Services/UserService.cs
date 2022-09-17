using AutoMapper;
using SocialMed.API.Security.Authorization.Handlers.Interfaces;
using SocialMed.API.Security.Domain.Models;
using SocialMed.API.Security.Domain.Repositories;
using SocialMed.API.Security.Domain.Services;
using SocialMed.API.Security.Domain.Services.Communication;
using SocialMed.API.Security.Exceptions;
using SocialMed.API.Shared.Domain.Repositories;
using BCryptNet = BCrypt.Net.BCrypt;

namespace SocialMed.API.Security.Services;

public class UserService : IUserService
{
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IJwtHandler _jwtHandler;
    private readonly IMapper _mapper;

    public UserService(IUserRepository userRepository, IUnitOfWork unitOfWork, IJwtHandler jwtHandler, IMapper mapper)
    {
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
        _jwtHandler = jwtHandler;
        _mapper = mapper;
    }
    public async Task<IEnumerable<User>> ListAsync()
    {
        return await  _userRepository.ListAsync();
    }

    public async Task<User> GetByIdAsync(int id)
    {
        var user = await _userRepository.FindByIdAsync(id);
        if (user == null)
            throw new KeyNotFoundException("User not found.");
        return user;
    }

    public async Task<AuthenticateResponse> Authenticate(AuthenticateRequest request)
    {
        var user = await _userRepository.FindByEmailAsync(request.Email);
        //Console.WriteLine($"Request: {request.Email}, {request.Password}");
        //Console.WriteLine($"User: {user.Id}, {user.Name}, {user.LastName}, {user.PasswordHash}");

        if (user == null || !BCryptNet.Verify(request.Password, user.PasswordHash))
        {
            Console.WriteLine("Authentication Error");
            throw new AppException("Username or password is incorrect.");
        }
        Console.WriteLine("Authentication successful. About to generate token.");
        var response = _mapper.Map<AuthenticateResponse>(user);
        Console.WriteLine($"Response: {response.Id}, {response.Name}, {response.LastName}, {response.Email}");
        response.Token = _jwtHandler.GenerateToken(user);
        Console.WriteLine($"Generated Token: {response.Token}");
        return response;
    }

    public async Task RegisterAsync(RegisterRequest request)
    {
        if (_userRepository.ExistsByEmail(request.Email))
            throw new AppException($"Email '{request.Email}' is already taken.");

        var user = _mapper.Map<User>(request);

        user.PasswordHash = BCryptNet.HashPassword(request.Password);
        user.Recommendation = 0;

        try
        {
            await _userRepository.AddAsync(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while saving the user: {e.Message}");
        }
    }

    public async Task<User> FindByEmailAsync(string email)
    {
        var user = await _userRepository.FindByEmailAsync(email);
        if (user == null)
            return new User();
        return user;
    }

    public async Task UpdateAsync(int id, UpdateRequest request)
    {

        var user = GetById(id);
        var userWithEmail = await _userRepository.FindByEmailAsync(request.Email);
        
        if(userWithEmail != null && userWithEmail.Id != user.Id)
            throw new AppException($"Email '{request.Email}' is already taken.");

        if (!string.IsNullOrEmpty(request.Password))
            user.PasswordHash = BCryptNet.HashPassword(request.Password);

        _mapper.Map(request, user);
        try
        {
            _userRepository.Update(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while updating the user: {e.Message}");
        }
    }

    public async Task DeleteAsync(int id)
    {
        var user = GetById(id);
        try
        {
            _userRepository.Remove(user);
            await _unitOfWork.CompleteAsync();
        }
        catch (Exception e)
        {
            throw new AppException($"An error occurred while deleting the user: {e.Message}");
        }
    }

    private User GetById(int id)
    {
        var user = _userRepository.FindById(id);
        if (user == null) throw new KeyNotFoundException("User not found.");
        return user;
    }
}