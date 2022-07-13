using System.Collections.Generic;

namespace ClientWebApi.Models
{
    public class ErrorResponse
    {
        public List<ErrorResult> ErrorResults { get; set; }

        public ErrorResponse()
        {
            ErrorResults = new List<ErrorResult>();
        }
    }
}
