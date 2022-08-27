using System.Net;
using System.Text.Json;
using AktifBank.CustomerOrder.Api.Model;
using AktifBank.CustomerOrder.Shared.Const;
using FluentValidation;

namespace AktifBank.CustomerOrder.Api.Middleware
{
    public class ExceptionHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionHandlerMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        { 
            try
            {
                await _next(context);
            }
            catch (Exception error)
            {
                var result = new ReqError();
                var response = context.Response;
                response.ContentType = "application/json";
                switch (error)
                {
                    case AppException:
                        result.Message = error.Message;
                        response.StatusCode = (int)HttpStatusCode.BadRequest;

                        break;
                    case ValidationException:
                        result.Message = "";
                        response.StatusCode = (int)HttpStatusCode.BadRequest; //error.GetExceptionErrorCode();
                        foreach (var itemError in ((ValidationException)error).Errors)
                        {
                            result.Message += itemError.ErrorMessage + Environment.NewLine;
                        }

                        break;
                    default:
                        var message = SetMessage(error);
                        result.Message = MessageConst.AN_UNEXPECTED_ERROR_OCCURRED;
                        break;
                }
                await response.WriteAsync(JsonSerializer.Serialize(result));
            }
        }
        private string SetMessage(Exception ex)
        {
            if (ex.InnerException != null)
            {
                SetMessage(ex.InnerException);
            }
            return ex.Message;
        }
    }
}
