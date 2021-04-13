namespace EasyIn.Models
{
    public class ResponseError
    {
        public string Message { get; private set; }

        public ResponseError(string message)
        {
            Message = message;
        }
    }
}