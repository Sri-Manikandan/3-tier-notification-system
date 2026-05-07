using System;

namespace NotificationModelLibrary.Exceptions
{
    public class CustomException : Exception
    {
        private string _message;
        public CustomException(string message) : base(message)
        {
            _message = message;
        }

        public override string Message => _message;
    }
}