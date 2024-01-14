using backend_daw.DTOs.Util;
using backend_daw.DTOs.Workout;
using backend_daw.DTOs.WorkoutExercise;
using backend_daw.Extensions;
using backend_daw.Services.ExerciseServices;
using backend_daw.Services.UserServices;
using backend_daw.Services.WorkoutExerciseServices;
using backend_daw.Services.WorkoutServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace backend_daw.Controllers
{
    public class WorkoutExerciseController: ControllerBase
    {
        private readonly IWorkoutExerciseService _workoutExerciseService;
        private readonly IWorkoutService _workoutService;
        private readonly IExerciseService _exerciseService;
        public WorkoutExerciseController(IWorkoutExerciseService workoutExerciseService, IWorkoutService workoutService, IExerciseService exerciseService)
        {
            _workoutExerciseService = workoutExerciseService;
            _workoutService = workoutService;
            _exerciseService = exerciseService;
        }

        [HttpPost]
        [Authorize]
        [Route("createWorkoutExercise")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
        public async Task<IActionResult> CreateWorkoutExercise([FromBody] CreateWorkoutExerciseRequest request)
        {
            var result = await _workoutExerciseService
                .CreateWorkoutExercise(request.WorkoutId, request.ExerciseId, request.Reps, request.Sets, request.Weight);

            var resultDto = result.ToResultDto();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [HttpPut]
        [Authorize]
        [Route("updateWorkoutExercise")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
        public async Task<IActionResult> UpdateWorkoutExercise([FromBody] UpdateWorkoutExerciseRequest request)
        {
            var result = await _workoutExerciseService.UpdateWorkoutExercise(request.WorkoutExerciseId, request.Reps, request.Sets, request.Weight);

            var resultDto = result.ToResultDto();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [HttpDelete]
        [Authorize]
        [Route("deleteWorkoutExercise")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
        public async Task<IActionResult> DeleteWorkoutExercise([FromBody] DeleteWorkoutExerciseRequest request)
        {
            var result = await _workoutExerciseService.DeleteWorkoutExercise(request.WorkoutExerciseId);

            var resultDto = result.ToResultDto();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }

        [HttpGet]
        [Authorize]
        [Route("getAllWorkoutExercises")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(ResultDto<string>))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(ResultDto<string>))]
        public async Task<IActionResult> GetWorkoutExercises(int workoutId)
        {
            var result = await _workoutExerciseService.GetWorkoutExercises(workoutId);

            var resultDto = result.ToResultDto();

            if (!resultDto.IsSuccess)
            {
                return BadRequest(resultDto);
            }

            return Ok(resultDto);
        }
    }
}
