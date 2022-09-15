using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMed.API.Groups.Domain.Models;
using SocialMed.API.Groups.Domain.Repositories;
using SocialMed.API.Groups.Domain.Services;
using SocialMed.API.Groups.Resources;
using SocialMed.API.Shared.Extensions;

namespace SocialMed.API.Groups.Controllers;

[ApiController]
[Route("/api/v1/[controller]")]
public class ChatsController : ControllerBase
{
    private readonly IChatService _chatService;
    private readonly IMapper _mapper;

    public ChatsController(IChatService chatService, IMapper mapper)
    {
        _chatService = chatService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<ChatResource>> GetAllAsync()
    {
        var chats = await _chatService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Chat>, IEnumerable<ChatResource>>(chats);

        return resources;

    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveChatResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var chat = _mapper.Map<SaveChatResource, Chat>(resource);

        var result = await _chatService.SaveAsync(chat);

        if (!result.Success)
            return BadRequest(result.Message);

        var chatResource = _mapper.Map<Chat, ChatResource>(result.Resource);

        return Ok(chatResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveChatResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var chat= _mapper.Map<SaveChatResource, Chat>(resource);

        var result = await _chatService.UpdateAsync(id, chat);

        if (!result.Success)
            return BadRequest(result.Message);

        var chatResource = _mapper.Map<Chat, ChatResource>(result.Resource);

        return Ok(chatResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _chatService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var chatResource = _mapper.Map<Chat, ChatResource>(result.Resource);

        return Ok(chatResource);
    } 
    
}