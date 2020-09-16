using System;

namespace Prime.HttpClients
{
    public class PharmanetCollegeApiException : Exception
    {
        public PharmanetCollegeApiException() : base() { }
        public PharmanetCollegeApiException(string message) : base(message) { }
        public PharmanetCollegeApiException(string message, Exception inner) : base(message, inner) { }
    }
}
