using System;

namespace DatingZone.Exceptions
{
    public class UserUnautorized: Exception
    {
        public UserUnautorized(string message): base(message)
        { }
    }
}
