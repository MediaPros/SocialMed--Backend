using SocialMed.API.Groups.Domain.Models;
using SocialMed.API.Groups.Domain.Repositories;
using SocialMed.API.Groups.Domain.Services;
using SocialMed.API.Groups.Domain.Services.Communication;
using SocialMed.API.Security.Domain.Repositories;
using SocialMed.API.Shared.Domain.Repositories;

namespace SocialMed.API.Groups.Services;

public class MessageService: IMessageService
{
    private readonly IChatRepository _chatRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    private readonly IMessageRepository _messageRepository;
    public MessageService(
        IUserRepository userRepository,
        IChatRepository chatRepository, 
        IUnitOfWork unitOfWork, 
        IMessageRepository messageRepository)
    {
        _chatRepository = chatRepository;
        _messageRepository = messageRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<Message>> ListAsync()
    {
        return await _messageRepository.ListAsync();
    }
    

    public async Task<IEnumerable<Message>> ListByChatIdAsync(int id)
    {
        return await _messageRepository.ListByChatIdAsync(id);
    }

    public async Task<MessageResponse> SaveAsync(Message message)
    {
        // Validate user id

        var existingUser = await _userRepository.FindByIdAsync(message.UserId);
        
        var existingUserDestiny = await _userRepository.FindByIdAsync(message.UserDestinyId);
        var existingChat = await _chatRepository.FindByIdAsync(message.ChatId);


        if (existingUser == null||existingChat==null||existingUserDestiny==null)
            return new MessageResponse("Invalid User or Chat");
        
        // Validate Title
        
        try
        {
            // Add Tutorial
            await _messageRepository.AddAsync(message);
            // Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            // Return response
            return new MessageResponse(message);

        }
        catch (Exception e)
        {
            // Error Handling
            return new MessageResponse($"An error occurred while saving the new Message: {e.Message}");
        }
    }

    public async Task<MessageResponse> UpdateAsync(int id, Message message)
    {
        var existingMessage = await _messageRepository.FindByIdAsync(id);
        
        // Validate chat

        if (existingMessage == null)
            return new MessageResponse("Message not found.");

        // Validate Chat users

        var existingUser = await _userRepository.FindByIdAsync(message.UserId);

        if (existingUser == null)
            return new MessageResponse("Invalid User ");
        

        // Modify Fields
        existingMessage.Content = message.Content;// pe

        try
        {
            _messageRepository.Update(existingMessage);
            await _unitOfWork.CompleteAsync();

            return new MessageResponse(existingMessage);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new MessageResponse($"An error occurred while updating the Message: {e.Message}");
        }
    }

    public async Task<MessageResponse> DeleteAsync(int id)
    {
        var existingMessage = await _messageRepository.FindByIdAsync(id);
        
        // Validate chat

        if (existingMessage == null)
            return new MessageResponse("Message not found.");
        
        try
        {
            _messageRepository.Remove(existingMessage);
            await _unitOfWork.CompleteAsync();

            return new MessageResponse(existingMessage);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new MessageResponse($"An error occurred while deleting the Message: {e.Message}");
        }
    }
}