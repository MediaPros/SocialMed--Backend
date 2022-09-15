using System.Collections.Generic;
using System.Net.Mime;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMed.API.Forums.Domain.Services;
using SocialMed.API.Forums.Resources;
using SocialMed.API.Shared.Extensions;
using SocialMed.API.SocialMedCenter.Domain.Models;
using SocialMed.API.SocialMedCenter.Resources;
using Swashbuckle.AspNetCore.Annotations;

namespace SocialMed.API.Forums.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
[Produces(MediaTypeNames.Application.Json)]
[SwaggerTag("Create, read, update and delete Forums")]
public class ForumsController: ControllerBase
{
    private readonly IForumService _forumService;
    private readonly IMapper _mapper;

    public ForumsController(IMapper mapper, IForumService forumService)
    {
        _mapper = mapper;
        _forumService = forumService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<ForumResource>), 200)]
    public async Task<IEnumerable<ForumResource>> GetAllAsync()
    {
        var forums = await _forumService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Forum>, IEnumerable<ForumResource>>(forums);
        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ForumResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The forum was successfully created.", typeof(ForumResource))]
    [SwaggerResponse(400, "The forum data is not valid.")]
    public async Task<IActionResult> PostAsync([FromBody] SaveForumResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var forum = _mapper.Map<SaveForumResource, Forum>(resource);
        var result = await _forumService.SaveAsync(forum);

        if (!result.Success)
            return BadRequest(result.Message);

        var forumResource = _mapper.Map<Forum, ForumResource>(result.Resource);

        return Created(nameof(PostAsync), forumResource);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveForumResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var forum = _mapper.Map<SaveForumResource, Forum>(resource);
        var result = await _forumService.UpdateAsync(id, forum);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var forumResource = _mapper.Map<Forum, ForumResource>(result.Resource);
        return Ok(forumResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _forumService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var forumResource = _mapper.Map<Forum, ForumResource>(result.Resource);

        return Ok(forumResource);
    }
}