using System;

namespace MastersProject.App.Extensions
{
    internal static class NullCheck
    {
        public static void AssertNotNull(this object? obj, string message = "")
        {
            if (obj is null)
            {
                throw new NullReferenceException(message);
            }
        }
        public static void AssertNotZero(this object? obj, string memberName, string message = "Member {0} cannot me zero.")
        {
            if (obj is 0 or 0.0)
            {
                throw new ArgumentException(string.Format(message, memberName));
            }
        }
    }
}
