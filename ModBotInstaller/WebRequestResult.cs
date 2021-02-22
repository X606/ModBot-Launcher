using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace ModBotInstaller
{
    public class WebRequestResult
    {
        public readonly bool IsError;
        public readonly WebExceptionStatus ErrorType;

        public readonly string Result;

        public WebRequestResult(WebExceptionStatus errorType) : this(true, errorType, null)
        {
        }

        public WebRequestResult(string result) : this(false, WebExceptionStatus.Success, result)
        {
        }

        WebRequestResult(bool isError, WebExceptionStatus errorType, string result)
        {
            IsError = isError;
            ErrorType = errorType;
            Result = result;
        }
    }
}
