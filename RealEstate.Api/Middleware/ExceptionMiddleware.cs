using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace RealEstate.Api.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        public ExceptionMiddleware(RequestDelegate next) => _next = next;

        public async Task Invoke(HttpContext ctx)
        {
            try { await _next(ctx); }
            catch (Exception ex)
            {
                var problem = new ProblemDetails
                {
                    Title = "An error occurred while processing your request.",
                    Detail = ex.Message,
                    Status = StatusCodes.Status500InternalServerError
                };
                ctx.Response.StatusCode = problem.Status.Value;
                ctx.Response.ContentType = "application/problem+json";
                await ctx.Response.WriteAsync(JsonSerializer.Serialize(problem));
            }
        }
    }
}
