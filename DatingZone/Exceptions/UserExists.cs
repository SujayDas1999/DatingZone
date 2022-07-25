using System;

namespace DatingZone.Exceptions
{
    public class UserExists: Exception
    {
        public UserExists(string message): base(message)
        { }
    }
}
