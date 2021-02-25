using System;
using System.Collections.Generic;
using System.Net;
using System.Text;
using System.Text.Json;

namespace WorkshopLibrary.Exceptions
{
    public class ErrorResponse
    {
        public HttpStatusCode StatusCode { get; set; } = HttpStatusCode.InternalServerError;

        public string Message { get; set; } = "An unexpected error occurred.";

        public string ToJsonString()
        {
            return JsonSerializer.Serialize(this);
        }
    }
}
