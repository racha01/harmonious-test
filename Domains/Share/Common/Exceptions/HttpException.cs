using Microsoft.AspNetCore.Http;
using System.Net;

namespace Common.Exceptions
{
    public class HttpException : Exception
    {
        public HttpStatusCode StatusCode { get; }

        public HttpException()
        {
        }
        //public string message { get; set; }
        public HttpException(string message) : base(message)
        {
        }

        public HttpException(HttpStatusCode statusCode, string message) : base(message)
        {
            StatusCode = statusCode;
        }

        //public HttpException(HttpStatusCode statusCode, string message): base(message)
        //{
        //    message = message;
        //}


    }
}
