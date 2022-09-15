using Microsoft.AspNetCore.Mvc;
using SocialMed.API.Forums.Domain.Services;
using AutoMapper;
using SocialMed.API.Forums.Domain.Models;
using SocialMed.API.Forums.Resources;
using SocialMed.API.Shared.Extensions;
using Swashbuckle.AspNetCore.Annotations;

namespace SocialMed.API.Forums.Controllers;

public class RatingController : ControllerBase
{
    private readonly IRatingService _ratingService;
    private readonly IMapper _mapper;

    public RatingController(IMapper mapper, IRatingService ratingService)
    {
        this._mapper = mapper;
        this._ratingService = ratingService;
    }

    [HttpGet]
    [ProducesResponseType(typeof(IEnumerable<RatingResource>), 200)]
    public async Task<IEnumerable<RatingResource>> GetAllAsync()
    {
        var ratings = await _ratingService.ListAsync();
        var resources = _mapper.Map<IEnumerable<Rating>, IEnumerable<RatingResource>>(ratings);
        return resources;
    }

    [HttpPost]
    [ProducesResponseType(typeof(ForumResource), 201)]
    [ProducesResponseType(typeof(List<string>), 400)]
    [ProducesResponseType(500)]
    [SwaggerResponse(201, "The rating was successfully created.", typeof(ForumResource))]
    [SwaggerResponse(400, "The rating data is not valid.")]
    public async Task<IActionResult> PostAsync([FromBody] SaveRatingResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var rating = _mapper.Map<SaveRatingResource, Rating>(resource);
        var result = await _ratingService.SaveAsync(rating);

        if (!result.Success)
            return BadRequest(result.Message);

        var ratingResource = _mapper.Map<Rating, RatingResource>(result.Resource);

        return Created(nameof(PostAsync), ratingResource);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> PutAsync(int id, [FromBody] SaveRatingResource resource)
    {
        if (!ModelState.IsValid)
            return BadRequest(ModelState.GetErrorMessages());
        var rating = _mapper.Map<SaveRatingResource, Rating>(resource);
        var result = await _ratingService.UpdateAsync(id, rating);
        
        if (!result.Success)
            return BadRequest(result.Message);

        var ratingResource = _mapper.Map<Rating, RatingResource>(result.Resource);
        return Ok(ratingResource);
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteAsync(int id)
    {
        var result = await _ratingService.DeleteAsync(id);

        if (!result.Success)
            return BadRequest(result.Message);
        
        var ratingResource = _mapper.Map<Rating, RatingResource>(result.Resource);

        return Ok(ratingResource);
    }
}