using System.Net;

namespace MovieLibrary.Models.Responses
{
    public class HttpResponse<T>
    {
        public HttpStatusCode StatusCode { get; set; }
        public string Message { get; set; }
        public T Value { get; set; }
    }
}
