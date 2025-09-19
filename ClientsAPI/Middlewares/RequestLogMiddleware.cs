using System;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
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
            var headers = context.Request.Headers; // lee los headers de la request
            
            context.Request.EnableBuffering();  // habilita -stream- para leer body

            string bodyAsString = "";
            using (var reader = new StreamReader(
                context.Request.Body,
                encoding: System.Text.Encoding.UTF8,
                detectEncodingFromByteOrderMarks: false,
                leaveOpen: true))
            {
                bodyAsString = await reader.ReadToEndAsync();
                context.Request.Body.Position = 0; // reinicia stream
            }

            //log (serilog)
            Log.Information("Requedt: {Method} {Path} | Headers: {@Headers} | Body: {Body}",
                context.Request.Method,
                context.Request.Path,
                headers,
                bodyAsString);

            await _next(context); // llama al sig middleware
        }
    }
}
