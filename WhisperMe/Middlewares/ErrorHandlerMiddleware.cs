using AutoMapper;
using Newtonsoft.Json;
using System.Net;
using WhisperMe.ViewModels.Responses;

namespace WhisperMe.Middlewares
{
    public class ErrorHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IMapper _mapper;

        public ErrorHandlerMiddleware(RequestDelegate next, IMapper mapper)
        {
            _next = next;
            _mapper = mapper;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Error error)
            {
                await HandleExceptionAsync(context, error);
            }
            catch (Exception exception)
            {
                var response = context.Response;
                response.ContentType = "application/json";

                var errorList = new List<ErrorRaw>()
                {
                new ErrorRaw()
                    {
                        general_message = exception.Message,
                        code = "500",
                    }
                };

                var result = System.Text.Json.JsonSerializer.Serialize(errorList);


                response.HttpContext.Response.StatusCode = 400;

                await response.WriteAsync(result);

            }
        }

        private Task HandleExceptionAsync(HttpContext context, Error exception)
        {
            var code = HttpStatusCode.InternalServerError;
            var response = context.Response;

            response.ContentType = "application/json";
            response.StatusCode = (int)code;

            if (exception is Exception)
            {
                code = HttpStatusCode.BadRequest;
            }

            var listaErros = new List<ErrorRaw>();

            listaErros.Add(_mapper.Map<ErrorRaw>(exception));


            var ex = new ExceptionVM() { errors = listaErros };

            if (ex.errors.First().general_message == "Invalid Token")
            {
                response.HttpContext.Response.StatusCode = HttpStatusCode.Unauthorized.GetHashCode();
            }
            else
            {
                response.HttpContext.Response.StatusCode = Convert.ToInt32(exception.Code);
            }
            return response.WriteAsync(JsonConvert.SerializeObject(ex.errors));

        }
    }
}
