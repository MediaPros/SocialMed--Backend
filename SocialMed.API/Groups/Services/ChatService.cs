using SocialMed.API.Groups.Domain.Models;
using SocialMed.API.Groups.Domain.Repositories;
using SocialMed.API.Groups.Domain.Services;
using SocialMed.API.Groups.Domain.Services.Communication;
using SocialMed.API.Security.Domain.Repositories;
using SocialMed.API.Shared.Domain.Repositories;

namespace SocialMed.API.Groups.Services;

public class ChatService: IChatService
{
    private readonly IChatRepository _chatRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;

    public ChatService(IChatRepository chatRepository,IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _chatRepository = chatRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }
    public async Task<IEnumerable<Chat>> ListAsync()
    {
        return await _chatRepository.ListAsync();
    }

    public async Task<IEnumerable<Chat>> ListByUserIdAsync(int userId)
    {
        return await _chatRepository.FindByUserIdAsync(userId);
    }

    public async Task<IEnumerable<Chat>> ListByUserDestinyIdAsync(int userId)
    {
        return await _chatRepository.FindByUserDestinyIdAsync(userId);
    }

    public async Task<Chat> GetByIdAsync(int id)
    {
        return await _chatRepository.FindByIdAsync(id);
    }

    public async Task<Chat> GetByUserIdAndUserDestinyIdAsync(int userId, int userDestinyId)
    {
        return await _chatRepository.FindByUserIdAndUserDestinyIdAsync(userId,userDestinyId);
    }

    public async Task<ChatResponse> SaveAsync(Chat chat)
    {
        // Validate user id

        var existingUser = await _userRepository.FindByIdAsync(chat.UserId);
        var existingUserDestiny = await _userRepository.FindByIdAsync(chat.UserDestinyId);

        if (existingUser == null||existingUserDestiny==null)
            return new ChatResponse("Invalid User");
        
        // Validate Title

        var existingChatWithThisUserDestiny = await _chatRepository.FindByUserIdAndUserDestinyIdAsync(chat.UserId,chat.UserDestinyId);
        if (existingChatWithThisUserDestiny != null)
            return new ChatResponse("Chat with user destiny already exists.");
        try
        {
            // Add Tutorial
            await _chatRepository.AddAsync(chat);
            // Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            // Return response
            return new ChatResponse(chat);

        }
        catch (Exception e)
        {
            // Error Handling
            return new ChatResponse($"An error occurred while saving the new Chat: {e.Message}");
        }
    }

    public async Task<ChatResponse> UpdateAsync(int id, Chat chat)
    {
        var existingChat = await _chatRepository.FindByIdAsync(id);
        
        // Validate chat

        if (existingChat == null)
            return new ChatResponse("Chat not found.");

        // Validate Chat users

        var existingUser = await _userRepository.FindByIdAsync(chat.UserId);
        var existingUserDestiny = await _userRepository.FindByIdAsync(chat.UserDestinyId);

        if (existingUser == null||existingUserDestiny==null)
            return new ChatResponse("Invalid User or Invalid User Destiny");
        

        // Modify Fields
        existingChat.Messages = chat.Messages;// pe

        try
        {
            _chatRepository.Update(existingChat);
            await _unitOfWork.CompleteAsync();

            return new ChatResponse(existingChat);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new ChatResponse($"An error occurred while updating the Chat: {e.Message}");
        }
    }

    public async Task<ChatResponse> DeleteAsync(int id)
    {
        var existingChat = await _chatRepository.FindByIdAsync(id);
        
        // Validate chat

        if (existingChat == null)
            return new ChatResponse("Chat not found.");
        
        try
        {
            _chatRepository.Remove(existingChat);
            await _unitOfWork.CompleteAsync();

            return new ChatResponse(existingChat);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new ChatResponse($"An error occurred while deleting the Chat: {e.Message}");
        }
    }
}