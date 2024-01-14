using backend_daw.DTOs.Post;
using backend_daw.DTOs.Util;
using backend_daw.DTOs.Workout;
using backend_daw.Extensions;
using backend_daw.Services.CommentServices;
using backend_daw.Services.PostServices;
using backend_daw.Services.UserServices;
using backend_daw.Services.WorkoutServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend_daw.Controllers
{
    public class WorkoutController : ControllerBase
    {
        private readonly IWorkoutService _workoutService;
        private readonly IUserService _userService;
        public WorkoutController(IWorkoutService workoutService, IUserService userService)
        {
            _workoutService = workoutService;
            _userService = userService;
        }

        [HttpPost]
        [Authorize]
        [Route("createWorkout")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
        public async Task<IActionResult> CreateWorkkout([FromBody] CreateWorkoutRequest request)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (string.IsNullOrEmpty(userId))
            {
                return BadRequest("Unable to retrieve user information from claims.");
            }

            var result = await _workoutService.CreateWorkout(userId, request.Name);

            var resultDto = result.ToResultDto();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [HttpPut]
        [Authorize]
        [Route("updateWorkout")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
        public async Task<IActionResult> UpdateWorkout([FromBody] UpdateWorkoutRequest request)
        {
            var result = await _workoutService.UpdateWorkout(request.WorkoutId, request.Name);

            var resultDto = result.ToResultDto();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [HttpDelete]
        [Authorize]
        [Route("deleteWorkout")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
        public async Task<IActionResult> DeleteWorkout([FromBody] DeleteWorkoutRequest request)
        {
            var result = await _workoutService.DeleteWorkout(request.WorkoutId);

            var resultDto = result.ToResultDto();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [HttpGet]
        [Authorize]
        [Route("getAllWorkouts")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
        public async Task<IActionResult> GetWorkouts()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            var result = await _workoutService.GetWorkouts(userId);

            var resultDto = result.ToResultDto();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }
    }
}
