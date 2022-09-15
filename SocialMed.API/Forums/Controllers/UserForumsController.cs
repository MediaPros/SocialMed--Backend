using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMed.API.Forums.Domain.Services;
using SocialMed.API.Forums.Resources;
using SocialMed.API.SocialMedCenter.Domain.Models;

namespace SocialMed.API.Forums.Controllers;

[ApiController]
[Route("/api/v1/users/{userId}/forums")]
public class UserForumsController: ControllerBase
{
    private readonly IForumService _forumService;
    private readonly IMapper _mapper;

    public UserForumsController(IForumService forumService, IMapper mapper)
    {
        _forumService = forumService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<ForumResource>> GetAllForumsByUserIdAsync(int userId)
    {
        var forums = await _forumService.ListByUserIdAsync(userId);
        var resources = _mapper.Map<IEnumerable<Forum>, IEnumerable<ForumResource>>(forums);
        return resources;
    }
}