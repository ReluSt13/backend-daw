using backend_daw.DTOs.Comment;
using backend_daw.DTOs.Post;
using backend_daw.DTOs.Util;
using backend_daw.Extensions;
using backend_daw.Services.CommentServices;
using backend_daw.Services.PostServices;
using backend_daw.Services.UserServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend_daw.Controllers
{
    public class CommentController : ControllerBase
    {
        private readonly ICommentService _commentService;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public CommentController(ICommentService commentService, IPostService postService, IUserService userService)
        {
            _commentService = commentService;
            _postService = postService;
            _userService = userService;
        }

        [HttpPost]
        [Authorize]
        [Route("createComment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
        public async Task<IActionResult> CreateComment([FromBody] CreateCommentRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var userName = User.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Unable to retrieve user information from claims.");
            }

            var result = await _commentService.CreateComment(userId, request.Content, userName, request.PostId);

            var resultDto = result.ToResultDto();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [HttpDelete]
        [Authorize]
        [Route("deleteComment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
        public async Task<IActionResult> DeleteComment([FromBody] DeleteCommentRequest request)
        {
            var result = await _commentService.DeleteComment(request.CommentId);

            var resultDto = result.ToResultDto();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [HttpGet]
        [Authorize]
        [Route("getAllComments/{postId}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
        public async Task<IActionResult> GetComments(int postId)
        {
            var result = await _commentService.GetComments(postId);

            var resultDto = result.ToResultDto();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [HttpPut]
        [Authorize]
        [Route("updateComment")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
        public async Task<IActionResult> UpdateComment([FromBody] UpdateCommentRequest request)
        {
            var result = await _commentService.UpdateComment(request.CommentId, request.Content);

            var resultDto = result.ToResultDto();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }
    }
}
