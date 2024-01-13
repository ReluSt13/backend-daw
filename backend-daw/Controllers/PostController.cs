using backend_daw.DTOs;
using backend_daw.Extensions;
using backend_daw.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend_daw.Controllers
{
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public PostController(IPostService postService, IUserService userService)
        {
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
        public async Task<IActionResult> CreatePost([FromBody] CreatePostRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Unable to retrieve user information from claims.");
            }

            var result = await _postService.CreatePost(userId, request.Content, request.Image);

            var resultDto = result.ToResultDto();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [HttpPost]
        [Authorize]
        [Route("update")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
        public async Task<IActionResult> UpdatePost([FromBody] UpdatePostRequest request)
        {
            var result = await _postService.UpdatePost(request.PostId, request.Content, request.Image);

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
        public async Task<IActionResult> DeletePost([FromBody] DeletePostRequest request)
        {
            var result = await _postService.DeletePost(request.PostId);

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
            var result = await _postService.GetPosts();

            var resultDto = result.ToResultDto();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }
    
    }
}
