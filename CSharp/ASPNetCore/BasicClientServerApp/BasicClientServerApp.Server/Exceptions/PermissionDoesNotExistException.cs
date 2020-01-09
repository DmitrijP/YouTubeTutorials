using System;
    
namespace BasicClientServerApp.Server.Exceptions
{
    public class PermissionDoesNotExistException : Exception
    {
        public PermissionDoesNotExistException(string message) : base(message)
        {
        }
    }
}
