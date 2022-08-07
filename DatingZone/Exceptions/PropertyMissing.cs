using System;

namespace DatingZone.Exceptions
{
    public class PropertyMissing: Exception
    {
        public PropertyMissing(string message): base(message)
        { }
    }
}
