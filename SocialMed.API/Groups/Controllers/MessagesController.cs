﻿using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using SocialMed.API.Groups.Domain.Models;
using SocialMed.API.Groups.Domain.Services;
using SocialMed.API.Groups.Resources;
using SocialMed.API.Security.Authorization.Attributes;
using SocialMed.API.Shared.Extensions;

namespace SocialMed.API.Groups.Controllers;

[Authorize]
[ApiController]
[Route("/api/v1/[controller]")]
public class MessagesController : ControllerBase
{
    private readonly IMessageService _messageService;
    private readonly IMapper _mapper;

    public MessagesController(IMessageService messageService, IMapper mapper)
    {
        _messageService = messageService;
        _mapper = mapper;
    }
    
    [HttpGet]
    public async Task<IEnumerable<MessageResource>> GetAllAsync()
    {
        var messages = await _messageService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Message>, IEnumerable<MessageResource>>(messages);

        return resources;

    }
    
    [HttpPost]
    public async Task<IActionResult> PostAsync([FromBody] SaveMessageResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var message = _mapper.Map<SaveMessageResource, Message>(resource);

        var result = await _messageService.SaveAsync(message);

        if (!result.Success)
            return BadRequest(result.Message);

        var messageResource = _mapper.Map<Message, MessageResource>(result.Resource);

        return Ok(messageResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveMessageResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());

        var message= _mapper.Map<SaveMessageResource, Message>(resource);

        var result = await _messageService.UpdateAsync(id, message);

        if (!result.Success)
            return BadRequest(result.Message);

        var messageResource = _mapper.Map<Message, MessageResource>(result.Resource);

        return Ok(messageResource);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _messageService.DeleteAsync(id);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var messageResource = _mapper.Map<Message, MessageResource>(result.Resource);

        return Ok(messageResource);
    } 
}