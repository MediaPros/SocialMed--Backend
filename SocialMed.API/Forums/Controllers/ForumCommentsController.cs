using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMed.API.Forums.Domain.Services;
using SocialMed.API.Forums.Resources;
using SocialMed.API.SocialMedCenter.Domain.Models;

namespace SocialMed.API.Forums.Controllers;

[ApiController]
[Route("/api/v1/forums/{forumId}/comments")]
public class ForumCommentsController: ControllerBase
{
    private readonly ICommentService _commentService;
    private readonly IMapper _mapper;

    public ForumCommentsController(ICommentService commentService, IMapper mapper)
    {
        _commentService = commentService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IEnumerable<CommentResource>> GetAllCommentsByForumIdAsync(int forumId)
    {
        var comments = await _commentService.ListByForumIdAsync(forumId);
        var resources = _mapper.Map<IEnumerable<Comment>, IEnumerable<CommentResource>>(comments);
        return resources;
    }
}