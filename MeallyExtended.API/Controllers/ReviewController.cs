using Azure.Core;
using MeallyExtended.Business.Interfaces;
using MeallyExtended.Business.Mappers;
using MeallyExtended.Contracts.Dto;
using MeallyExtended.Contracts.Requests.Review;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace MeallyExtended.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReviewController : ControllerBase
{
    private readonly IReviewService _reviewService;

    public ReviewController(IReviewService reviewService)
    {
        _reviewService = reviewService;
    }

    [HttpGet]
    public async Task<IActionResult> GetReviews([FromRoute] Guid recipeId, [FromQuery] int limit = 5, [FromQuery] int skip = 0)
    {
        try
        {
            var reviews =  await _reviewService.GetLimitedReviews(recipeId, limit, skip);

            ICollection<ReviewDto> result = new List<ReviewDto>();
            foreach(var review in reviews)
            {
                result.Add(MeallyMapper.ReviewToDto(review));
            }

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> CreateReview([FromBody] CreateReviewRequest request) 
    {
        try
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var review = MeallyMapper.CreateReviewRequestToReviewDto(request, userEmail);
            var result = await _reviewService.AddReview(review);

            return Ok(MeallyMapper.ReviewToDto(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut]
    [Authorize]
    public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewRequest request)
    {
        try
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var result = await _reviewService.UpdateReview(request, userEmail);
            return Ok(MeallyMapper.ReviewToDto(result));
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete]
    [Authorize]
    public async Task<IActionResult> DeleteReview([FromHeader] Guid reviewId) 
    {
        try
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            var result = await _reviewService.DeleteReview(reviewId, userEmail);

            return Ok();
        }
        catch (Exception ex)
        {
            return BadRequest();
        }

    }

}