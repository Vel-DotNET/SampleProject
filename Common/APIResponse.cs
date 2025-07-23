using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Common
{
    public class APIResponse
    {
        public object Result { get; set; }

        public bool IsSuccess { get; set; } = true;

        public string Message { get; set; } = "";

        public HttpStatusCode StatusCode { get; set; }
    }

}
