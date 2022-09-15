using SocialMed.API.Forums.Domain.Models;
using SocialMed.API.Forums.Domain.Repositories;
using SocialMed.API.Forums.Domain.Services;
using SocialMed.API.Forums.Domain.Services.Communication;
using SocialMed.API.Shared.Domain.Repositories;

namespace SocialMed.API.Forums.Services;

public class RatingService : IRatingService
{
    private IRatingRepository _ratingRepository;
    private IForumRepository _forumRepository;
    private IUnitOfWork _unitOfWork;

    public RatingService(IRatingRepository ratingRepository, IForumRepository forumRepository, IUnitOfWork unitOfWork)
    {
        _ratingRepository = ratingRepository;
        _forumRepository = forumRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<IEnumerable<Rating>> ListAsync()
    {
        return await _ratingRepository.ListAsync();
    }

    public async Task<IEnumerable<Rating>> ListByForumIdAsync(int forumId)
    {
        return await _ratingRepository.ListByForumIdAsync(forumId);
    }

    public async Task<RatingResponse> SaveAsync(Rating rating)
    {
        var forum = await _forumRepository.FindByIdAsync(rating.ForumId);
        if (forum == null)
            return new RatingResponse("Forum doesn't exist.");
        
        try
        {
            await _ratingRepository.AddAsync(rating);
            await _unitOfWork.CompleteAsync();
            return new RatingResponse(rating);
        }
        catch (Exception e)
        {
            return new RatingResponse($"An error occurred while saving the rating: {e.Message}");
        }
    }

    public async Task<RatingResponse> UpdateAsync(int id, Rating rating)
    {
        var existingRating = await _ratingRepository.FindByIdAsync(id);
        if (existingRating == null)
            return new RatingResponse("Rating doesn't exist.");
        
        var forum = await _forumRepository.FindByIdAsync(rating.ForumId);
        if (forum == null)
            return new RatingResponse("Forum doesn't exist.");

        existingRating.Rate = rating.Rate;
        
        try
        {
            _ratingRepository.Update(existingRating);
            await _unitOfWork.CompleteAsync();
            return new RatingResponse(existingRating);
        }
        catch (Exception e)
        {
            return new RatingResponse($"An error occurred while saving the rating: {e.Message}");
        }
    }

    public async Task<RatingResponse> DeleteAsync(int id)
    {
        var existingRating = await _ratingRepository.FindByIdAsync(id);
        if (existingRating == null)
            return new RatingResponse("Rating doesn't exist.");

        try
        {
            _ratingRepository.Remove(existingRating);
            await _unitOfWork.CompleteAsync();
            return new RatingResponse(existingRating);
        }
        catch (Exception e)
        {
            return new RatingResponse($"An error occurred while deleting the rating {e.Message}");
        }
    }
}