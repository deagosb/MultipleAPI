using System;

namespace ClientWebApi.Models
{
    public class ErrorResult
    {
        public string ErrorMessage { get; set; }
        public int ResultCode { get; set; }
        public DateTime TimeStamp { get; set; }

        public ErrorResult(string errorMessage, int resultCode)
        {
            ErrorMessage = errorMessage;
            ResultCode = resultCode;
            TimeStamp = DateTime.Now;
        }
    }    
}
