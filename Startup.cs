using ExceptionApiHandler.Models;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Http;

namespace ExceptionApiHandler;

public static class Startup
{
    public static void ApplyException(this IApplicationBuilder app)
    {
        app.UseExceptionHandler(error =>
        {
            error.Run(async context =>
            {
                var errorException = context.Features.Get<IExceptionHandlerFeature>().Error as ErrorException;

                context.Response.StatusCode = (int)errorException.ErrorType;
                context.Response.ContentType = "application/json";
                
                await context.Response.WriteAsync(new ErrorDetails
                {
                    StatusCode = context.Response.StatusCode,
                    Message = errorException.Message
                }.ToString());
            });
        });
        
    }
}