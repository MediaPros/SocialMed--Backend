using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMed.API.Forums.Domain.Models;
using SocialMed.API.Forums.Domain.Services;
using SocialMed.API.Forums.Resources;
using SocialMed.API.Security.Authorization.Attributes;

namespace SocialMed.API.Forums.Controllers;

[Authorize]
[ApiController]
[Route("/api/v1/forums/{forumId}/ratings")]
public class ForumRatingsController: ControllerBase
{
    private readonly IRatingService _ratingService;
    private readonly IMapper _mapper;

    public ForumRatingsController(IRatingService ratingService, IMapper mapper)
    {
        _ratingService = ratingService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<RatingResource>> GetAllRatingsByForumIdAsync(int forumId)
    {
        var ratings = await _ratingService.ListByForumIdAsync(forumId);
        var resources = _mapper.Map<IEnumerable<Rating>, IEnumerable<RatingResource>>(ratings);
        return resources;
    }
}