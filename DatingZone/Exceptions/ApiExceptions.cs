// an allrounder exception class which can be used to throw all kind of exceptions!!!

namespace DatingZone.Exceptions
{
    public class ApiExceptions
    {
        public ApiExceptions(int statusCode, string message = null, string details = null)
        {
            this.StatusCode = statusCode;
            this.Message = message;
            this.Details = details;
        }

        public int StatusCode { get; set; }
        public string Message { get; set; }
        public string Details { get; set; }
    }
}
