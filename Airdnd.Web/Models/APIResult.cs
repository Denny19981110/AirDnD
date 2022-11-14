namespace Airdnd.Web.Models
{
    public class APIResult
    {
        public APIResult(APIStatus status, string msg, object result) 
        {
            Status = status;
            Msg = msg;
            Result = result;
        }

        public APIResult(APIStatus status)
        {
            Status = status;
        }

        public APIResult(APIStatus status, string msg)
        {
            Status = status;
            Msg = msg;
        }

        public APIResult(APIStatus status, object result)
        {
            Status = status;
            Result = result;
        }

        public APIStatus Status { get; set; }
        public string Msg { get; set; }
        public object Result { get; set; }

        public enum APIStatus
        {
            Success = 2000,
            Fail = 4000
        }
    }
}
