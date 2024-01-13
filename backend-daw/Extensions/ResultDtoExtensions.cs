using backend_daw.DTOs.Util;
using FluentResults;

namespace backend_daw.Extensions
{
    public static class ResultDtoExtensions
    {
        public static ResultDto<TResponse> ToResultDto<TResponse>(this Result<TResponse> result)
        {
            var errorMessages = result.Errors?.Select(error => error.Message);

            return new ResultDto<TResponse>(result.IsSuccess, result.ValueOrDefault, errorMessages);
        }
    }
}
