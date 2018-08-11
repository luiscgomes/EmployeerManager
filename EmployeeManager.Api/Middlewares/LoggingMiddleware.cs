using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Internal;
using Serilog;
using Serilog.Context;
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManager.Api.Middlewares
{
    public class LoggingMiddleware
    {
        public RequestDelegate Next { get; }

        public LoggingMiddleware(RequestDelegate next)
        {
            Next = next ?? throw new ArgumentNullException(nameof(next));
        }

        public async Task Invoke(HttpContext context)
        {
            HandleRequestId(context);

            var requestBodyText = await GetRequestBody(context.Request);

            LogRequest(context, requestBodyText);

            var originalBodyStream = context.Response.Body;

            using (var responseBody = new MemoryStream())
            {
                context.Response.Body = responseBody;

                try
                {
                    await Next(context);
                }
                catch (Exception ex)
                {
                    Log.Error(ex, "Unexpected error occurred!");
                    context.Response.StatusCode = 500;
                    throw;
                }
                finally
                {
                    await LogResponse(context);

                    await responseBody.CopyToAsync(originalBodyStream);
                }
            }
        }

        private static void HandleRequestId(HttpContext context)
        {
            var requestId = Guid.NewGuid();
            var headerRequestId = context.Request.Headers["RequestId"];

            if (headerRequestId.Count > 0)
            {
                requestId = Guid.TryParse(headerRequestId, out var parsedRequestId) ? parsedRequestId : Guid.NewGuid();
                context.Request.Headers.Remove("RequestId");
            }

            context.Request.Headers.Add("RequestId", requestId.ToString());
            context.Response.Headers.Add("RequestId", requestId.ToString());

            LogContext.PushProperty("RequestId", requestId);
        }

        private static async Task<string> GetRequestBody(HttpRequest request)
        {
            request.EnableRewind();

            var body = request.Body;

            var buffer = new byte[Convert.ToInt32(request.ContentLength)];
            await request.Body.ReadAsync(buffer, 0, buffer.Length);
            var bodyAsText = Encoding.UTF8.GetString(buffer);
            body.Seek(0, SeekOrigin.Begin);
            request.Body = body;

            return bodyAsText;
        }

        private static async Task<string> GetResponseBody(HttpResponse response)
        {
            response.Body.Seek(0, SeekOrigin.Begin);
            var responseBodyInText = await new StreamReader(response.Body).ReadToEndAsync();
            response.Body.Seek(0, SeekOrigin.Begin);

            return responseBodyInText;
        }

        private static void LogRequest(
            HttpContext context,
            string requestBodyText)
        {
            Log.Information("[REQUEST] - {Method} - Body: {RequestBody}" +
                            " - Path: {RequestPath} - RequestIp: {RequestIp}",
                context.Request.Method,
                requestBodyText,
                $"{context.Request.Path}{context.Request.QueryString}",
                context.Connection.RemoteIpAddress);
        }

        private static async Task LogResponse(HttpContext context)
        {
            var responseBodyText = await GetResponseBody(context.Response);

            Log.Information("[RESPONSE] - STATUS CODE: {StatusCode} - BODY: {ResponseBody}",
                context.Response.StatusCode,
                responseBodyText);
        }
    }
}