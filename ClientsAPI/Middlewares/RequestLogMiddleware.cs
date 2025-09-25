using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Identity.Client;
using Serilog;


namespace ClientsAPI.Middlewares
{
    public class RequestLogMiddleware
    {
        private readonly RequestDelegate _next;

        public RequestLogMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context)
        {
            // request
            context.Request.EnableBuffering();

            string requestBody = "";
            using (var reader = new StreamReader(
                context.Request.Body,
                encoding: Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true))
            {
                requestBody = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0;     //resetea stream
            }

            Log.Information("Request: {Method} {Path} | Query {Query} | Headers: {@Headers} | Body: {Body}",
                context.Request.Method,
                context.Request.Path,
                context.Request.QueryString,
                context.Request.Headers,
                string.IsNullOrWhiteSpace(requestBody) ? "(Empty)" : requestBody
                );

            // response

            var originalBodyStream = context.Response.Body;
            using var responseBody = new MemoryStream();
            context.Response.Body = responseBody;

            await _next(context);

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            string responseBodyText = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);

            Log.Information("Response: {StatusCode} {Path} | Body: {Body}",
                context.Response.StatusCode,
                context.Request.Path,
                string.IsNullOrWhiteSpace(responseBodyText) ? "(Empty)" : responseBodyText);

            await responseBody.CopyToAsync(originalBodyStream);
        }
        }

}
