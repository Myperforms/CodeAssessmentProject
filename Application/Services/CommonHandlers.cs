using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CodeAssessment.Application.Response;
using Microsoft.AspNetCore.Http;

namespace CodeAssessment.Application.Services
{

    public static class CommonHandlers
    {
        public static BaseResponse<object> ValidationHandler(FluentValidation.Results.ValidationResult validationResult)
        {
            var validationResponse = new BaseResponse<object>();
            if (validationResult.Errors.Count > 0)
            {
                validationResponse.Code = StatusCodes.Status400BadRequest;
               
                StringBuilder errors = new StringBuilder();
                foreach (var error in validationResult.Errors)
                {
                    errors.Append(error.ErrorMessage);
                }

                validationResponse.Message = errors.ToString();
                validationResponse.HasError = true;
                validationResponse.Data = errors;
            }

            return validationResponse;
        }

        public static BaseResponse<object> InvalidRequestHandler(string message = "")
        {
            return new BaseResponse<object>()
            {
                Code = StatusCodes.Status400BadRequest,
                Message = string.IsNullOrEmpty(message) ? "Invalid Request" : message,
                HasError = true,
                Data = new List<string>(),
            };
        }

        public static BaseResponse<object> SuccessRequestHandler(string message = "")
        {
            return new BaseResponse<object>()
            {
                Code = StatusCodes.Status200OK,
                Message = string.IsNullOrEmpty(message) ? "Success" : message,
                HasError = false,
                Data = true,
            };
        }

        public static BaseResponse<object> EmptyRequestHandler(string message = "")
        {
            return new BaseResponse<object>()
            {
                Code = StatusCodes.Status200OK,
                Message = string.IsNullOrEmpty(message) ? "No Record(s) Found" : message,
                HasError = false,
                Data = new List<string>(),
            };
        }

        public static BaseResponse<object> ExceptionHandler()
        {
            return new BaseResponse<object>()
            {
                Code = StatusCodes.Status500InternalServerError,
                Message = "Oops! An error occurred while processing your request.",
                HasError = true,
                Data = null,
            };
        }
    }
}
