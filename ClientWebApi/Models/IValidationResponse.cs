using System.Text.Json.Serialization;

namespace ClientWebApi.Models
{
    public interface IValidationResponse
    {
        /// <summary>
        /// List of errors
        /// </summary>
        [JsonIgnore]
        ErrorResponse ErrorResponse { get; set; }
    }
}
