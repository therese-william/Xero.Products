using System.Net;

namespace Xero.Products.App.Http
{
    public class HttpException : Exception
    {
        public readonly HttpStatusCode StatusCode;
        public readonly bool IsErrorMiddlewareTarget;
        public HttpException(HttpStatusCode statusCode, string message = null, Exception innerException = null,
            bool isErrorMiddlewareTarget = true)
            : base(message, innerException)
        {
            StatusCode = statusCode;
            IsErrorMiddlewareTarget = isErrorMiddlewareTarget;
        }

        public HttpException(int statusCode, string message = null, Exception innerException = null,
            bool isErrorMiddlewareTarget = true)
            : this((HttpStatusCode)statusCode, message, innerException, isErrorMiddlewareTarget) { }
    }

    public class BadRequestException : HttpException
    {
        public BadRequestException(string message = null, Exception innerException = null,
            bool isErrorMiddlewareTarget = true)
            : base(HttpStatusCode.BadRequest, message, innerException, isErrorMiddlewareTarget) { }
    }

    public class ConflictException : HttpException
    {
        public ConflictException(string message = null, Exception innerException = null,
            bool isErrorMiddlewareTarget = true)
            : base(HttpStatusCode.Conflict, message, innerException, isErrorMiddlewareTarget) { }
    }

    public class NotFoundException : HttpException
    {
        public NotFoundException(string message = null, Exception innerException = null,
            bool isErrorMiddlewareTarget = true)
            : base(HttpStatusCode.NotFound, message, innerException, isErrorMiddlewareTarget) { }
    }

    public class NoContentException : HttpException
    {
        public NoContentException(string message = null, Exception innerException = null,
            bool isErrorMiddlewareTarget = true)
            : base(HttpStatusCode.NoContent, message, innerException, isErrorMiddlewareTarget) { }
    }

    public class ForbiddenException : HttpException
    {
        public ForbiddenException(string message = null, Exception innerException = null,
            bool isErrorMiddlewareTarget = true)
            : base(HttpStatusCode.Forbidden, message, innerException, isErrorMiddlewareTarget) { }
    }

    public class UnauthorizedException : HttpException
    {
        public UnauthorizedException(string message = null, Exception innerException = null,
            bool isErrorMiddlewareTarget = true)
            : base(HttpStatusCode.Unauthorized, message, innerException, isErrorMiddlewareTarget) { }
    }

    public class UnsupportedMediaTypeException : HttpException
    {
        public UnsupportedMediaTypeException(string message = null, Exception innerException = null,
            bool isErrorMiddlewareTarget = true)
            : base(HttpStatusCode.UnsupportedMediaType, message, innerException, isErrorMiddlewareTarget) { }
    }

    public class ServerErrorException : HttpException
    {
        public ServerErrorException(string message = null, Exception innerException = null,
            bool isErrorMiddlewareTarget = true)
            : base(HttpStatusCode.InternalServerError, message, innerException, isErrorMiddlewareTarget) { }
    }

    public class HttpResponseException : HttpException
    {
        public readonly string Response;
        public readonly string Url;

        public HttpResponseException(HttpStatusCode statusCode, string msg, Exception ex,
            string url, string response, bool isErrorMiddlewareTarget = false) :
            base(statusCode, msg, ex, isErrorMiddlewareTarget)
        {
            Response = response;
            Url = url;
        }
    }
}
