using System.Net;

namespace Store.DTOs.Common
{
    public class BaseResponse
    {
        public object? Data { get; set; }
        public string? Message { get; set; }
    }
}
