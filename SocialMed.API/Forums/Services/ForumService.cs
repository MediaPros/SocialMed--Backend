using SocialMed.API.Forums.Domain.Models;
using SocialMed.API.Forums.Domain.Repositories;
using SocialMed.API.Forums.Domain.Services;
using SocialMed.API.Forums.Domain.Services.Communication;
using SocialMed.API.Security.Domain.Repositories;
using SocialMed.API.Shared.Domain.Repositories;
using SocialMed.API.SocialMedCenter.Domain.Models;

namespace SocialMed.API.Forums.Services;

public class ForumService : IForumService
{
    private readonly IForumRepository _forumRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly IUserRepository _userRepository;
    public ForumService(
        IUnitOfWork unitOfWork, 
        IUserRepository userRepository,
        IForumRepository forumRepository)
    {
        _forumRepository = forumRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }
    public async Task<IEnumerable<Forum>> ListAsync()
    {
        return await  _forumRepository.ListAsync();
    }

    public async Task<ForumResponse> SaveAsync(Forum forum)
    {
        // Validate user id

        var existingUser = await _userRepository.FindByIdAsync(forum.UserId);

        if (existingUser == null)
            return new ForumResponse("Invalid User");
        
        // Validate Title

        var existingForum = await _forumRepository.FindByTitleAsync(forum.Title);

        if (existingForum != null)
            return new ForumResponse("Forum already exist with this title.");
        try
        {
            // Add Tutorial
            await _forumRepository.AddAsync(forum);
            
            // Complete Transaction
            await _unitOfWork.CompleteAsync();
            
            // Return response
            return new ForumResponse(forum);

        }
        catch (Exception e)
        {
            // Error Handling
            return new ForumResponse($"An error occurred while saving the new forum: {e.Message}");
        }
    }

    public async Task<Forum> FindByIdAsync(int id)
    {
        return await _forumRepository.FindByIdAsync(id);
    }

    public async Task<IEnumerable<Forum>> ListByUserIdAsync(int userId)
    {
        return await _forumRepository.ListByUserIdAsync(userId);
    }

    public async Task<ForumResponse> UpdateAsync(int id, Forum forum)
    {
        var existingForum = await _forumRepository.FindByIdAsync(id);
        
        if (existingForum== null)
            return new ForumResponse("forum not found.");
        
        var existingUser = await _userRepository.FindByIdAsync(forum.UserId);
        if (existingUser==null)
            return new ForumResponse("Invalid User not exist");
        

        // Modify Fields
        
        existingForum.Content = forum.Content;
        existingForum.Title = forum.Title;
        existingForum.Date = forum.Date;
        
        try
        {
            _forumRepository.Update(existingForum);
            await _unitOfWork.CompleteAsync();

            return new ForumResponse(existingForum);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new ForumResponse($"An error occurred while updating the Forum: {e.Message}");
        }
    }

    public async Task<ForumResponse> DeleteAsync(int id)
    {
        var existingForum = await _forumRepository.FindByIdAsync(id);
        
        // Validate chat

        if (existingForum== null)
            return new ForumResponse("Forum not found.");
        
        try
        {
            _forumRepository.Remove(existingForum);
            await _unitOfWork.CompleteAsync();

            return new ForumResponse(existingForum);
            
        }
        catch (Exception e)
        {
            // Error Handling
            return new ForumResponse($"An error occurred while deleting the Forum: {e.Message}");
        }
    }
}