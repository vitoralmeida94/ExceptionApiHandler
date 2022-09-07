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
                var errorException = context.Features.Get<IExceptionHandlerFeature>().Error;

                if (errorException.GetType() != typeof(ErrorException))
                {

                    BuildHttpMessage(context, errorException.Message);
                }
                else
                {
                    BuildHttpMessage(context, errorException.Message,(int)(errorException as ErrorException).ErrorType);
                }

                
            });
        });
        
    }

    private static async void BuildHttpMessage(HttpContext context, string message,int statusCode = 500)
    {
        context.Response.StatusCode = statusCode;
        context.Response.ContentType = "application/json";

        await context.Response.WriteAsync(new ErrorDetails
        {
            StatusCode = context.Response.StatusCode,
            Message = message
        }.ToString());
    }
}