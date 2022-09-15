using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMed.API.Medical_Interconsultation.Domain.Models;
using SocialMed.API.Medical_Interconsultation.Domain.Services;
using SocialMed.API.Medical_Interconsultation.Resources;
using SocialMed.API.Shared.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace SocialMed.API.Medical_Interconsultation.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]

public class RecommendationsController: ControllerBase
{
    private readonly IRecommendationService _recommendationService;
    private readonly IMapper _mapper;

    public RecommendationsController(IRecommendationService recommendationService, IMapper mapper)
    {
        _recommendationService = recommendationService;
        _mapper = mapper;
    }
    
    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RecommendationResource>), 200)]
    public async Task<IEnumerable<RecommendationResource>> GetAllAsync()
    {
        var recommendation = await _recommendationService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Recommendation>, IEnumerable<RecommendationResource>>(recommendation);
        return resources;
    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveRecommendationResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var recommendation = _mapper.Map<SaveRecommendationResource, Recommendation>(resource);
        var result = await _recommendationService.SaveAsync(recommendation);

        if (!result.Success)
            return BadRequest(result.Message);

        var recommendationResource = _mapper.Map<Recommendation, RecommendationResource>(result.Resource);

        return Created(nameof(PostAsync), recommendationResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _recommendationService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var recommendationResource = _mapper.Map<Recommendation, RecommendationResource>(result.Resource);

        return Ok(recommendationResource);
    }
    
    
}