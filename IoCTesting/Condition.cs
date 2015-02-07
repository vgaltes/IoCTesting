namespace IoCTesting
{
    using System;
    using System.IO;

    internal static class Condition
    {
        public static void NotNull(string argument, string argumentName)
        {
            if (argument == null)
            {
                throw new ArgumentNullException(argumentName);
            }
        }

        public static void NotNull(object argument, string argumentName, string message)
        {
            if (argument == null)
            {
                throw new ArgumentException(message, argumentName);
            }
        }

        public static void FileExists(string argument, string argumentName)
        {
            if (!File.Exists(argument))
            {
                throw new ArgumentException(string.Format("The file {0} does not exsit.", argument), argumentName);
            }
        }
    }
}