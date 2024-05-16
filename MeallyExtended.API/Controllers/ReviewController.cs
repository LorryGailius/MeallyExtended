using System.Data;
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

    /// <summary>
    /// Get reviews for specific recipe. Supports pooling of recipes 
    /// </summary>
    /// <param name="recipeId"></param>
    /// <param name="limit"></param>
    /// <param name="skip"></param>
    /// <Returns>List of reviews</Returns>
    [HttpGet("{recipeId:guid}")]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    public async Task<IActionResult> GetReviews([FromRoute] Guid recipeId, [FromQuery] int limit = 5, [FromQuery] int skip = 0)
    {
        try
        {
            var reviews = await _reviewService.GetLimitedReviews(recipeId, limit, skip);

            ICollection<ReviewDto> result = reviews.Select(MeallyMapper.ReviewToDto).ToList();

            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Create a review for recipe. Needs to be authorized 
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    [HttpPost]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
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

    /// <summary>
    /// Update review for recipe. Needs to be authorized
    /// </summary>
    /// <param name="request"></param>
    /// <returns>Updated review</returns>
    [HttpPut]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
    [ProducesResponseType(409)]
    public async Task<IActionResult> UpdateReview([FromBody] UpdateReviewRequest request)
    {
        try
        {
            var userEmail = User.Claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;

            var result = await _reviewService.UpdateReview(request, userEmail);
            return Ok(MeallyMapper.ReviewToDto(result));
        }
        catch (DBConcurrencyException)
        {
            return Conflict("Review has already been updated. Please refresh and try again.");
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    /// <summary>
    /// Delete review for recipe. Needs to be authorized and review creator
    /// </summary>
    /// <param name="reviewId"></param>
    /// <returns></returns>
    [HttpDelete]
    [Authorize]
    [ProducesResponseType(200)]
    [ProducesResponseType(400)]
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