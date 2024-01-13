using backend_daw.DTOs;
using backend_daw.Entities;
using backend_daw.Extensions;
using backend_daw.Services;
using fitness_app_backend.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend_daw.Controllers
{
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public FeedbackController(IFeedbackService feedbackService, IPostService postService, IUserService userService)
        {
            _feedbackService = feedbackService;
            _postService = postService;
            _userService = userService;
        }

        [HttpPost]
        [Authorize]
        [Route("create")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
        public async Task<IActionResult> CreateFeedback([FromBody] CreateFeedbackRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Unable to retrieve user information from claims.");
            }

            var result = await _feedbackService.CreateFeedback(userId, userName, request.PostId, request.Value);

            var resultDto = result.ToResultDto();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [HttpPost]
        [Authorize]
        [Route("delete")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
        public async Task<IActionResult> DeleteFeedback([FromBody] DeleteFeedbackRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Unable to retrieve user information from claims.");
            }

            var result = await _feedbackService.DeleteFeedback(userId, request.PostId);

            var resultDto = result.ToResultDto();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [HttpGet]
        [Authorize]
        [Route("getAll")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
        public async Task<IActionResult> GetPosts()
        {
            var result = await _feedbackService.GetFeedbacks();

            var resultDto = result.ToResultDto();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }
    }
}
