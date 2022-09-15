using SocialMed.API.Medical_Interconsultation.Domain.Models;
using SocialMed.API.Medical_Interconsultation.Domain.Repositories;
using SocialMed.API.Medical_Interconsultation.Domain.Services;
using SocialMed.API.Medical_Interconsultation.Domain.Services.Communication;
using SocialMed.API.Security.Domain.Repositories;
using SocialMed.API.Shared.Domain.Repositories;

namespace SocialMed.API.Medical_Interconsultation.Services;

public class RecommendationService : IRecommendationService
{
    private readonly IRecommendationRepository _recommendationRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RecommendationService(IRecommendationRepository recommendationRepository, IUnitOfWork unitOfWork, IUserRepository userRepository)
    {
        _recommendationRepository = recommendationRepository;
        _unitOfWork = unitOfWork;
        _userRepository = userRepository;
    }

    public async Task<IEnumerable<Recommendation>> ListAsync()
    {
        return await _recommendationRepository.ListAsync();
    }

    public async Task<RecommendationResponse> SaveAsync(Recommendation recommendation)
    {
        var existingRecommendationUser = await _userRepository.FindByIdAsync(recommendation.recommendationUserId);

        if (existingRecommendationUser == null)
            return new RecommendationResponse("Invalid recommendation user");

        var existingRecommendedUser = await _userRepository.FindByIdAsync(recommendation.recommendedUserId);
        
        if(existingRecommendedUser == null)
            return new RecommendationResponse("Invalid recommended user");
        
        try
        {
            await _recommendationRepository.AddAsync(recommendation);
            await _unitOfWork.CompleteAsync();
            return new RecommendationResponse(recommendation);
        }
        catch (Exception e)
        {
            return new RecommendationResponse($"An error occurred while saving recommendation: {e.Message}");
        }
    }

    public async Task<RecommendationResponse> DeleteAsync(int id)
    {
        var existingRecommendation = await _recommendationRepository.FindByIdAsync(id);
        if (existingRecommendation == null)
            return new RecommendationResponse("Recommendation not found.");
        try
        {
            _recommendationRepository.Remove(existingRecommendation);
            await _unitOfWork.CompleteAsync();
            return new RecommendationResponse(existingRecommendation);
        }
        catch (Exception e)
        {
            return new RecommendationResponse($"An error ocurred while deleting recommendation: {e.Message}");
        }
    }
}