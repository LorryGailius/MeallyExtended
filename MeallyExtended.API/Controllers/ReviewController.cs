using MeallyExtended.Business.Interfaces;
using Microsoft.AspNetCore.Mvc;

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
    public IActionResult GetReviews()
    {
        throw new NotImplementedException();
    }

    [HttpPost]
    public IActionResult AddReview() 
    {
        throw new NotImplementedException();
    }

    [HttpPut]
    public IActionResult UpdateReview()
    {
        throw new NotImplementedException();
    }

    [HttpDelete]
    public IActionResult DeleteReview() 
    {
        throw new NotImplementedException();
    }

}