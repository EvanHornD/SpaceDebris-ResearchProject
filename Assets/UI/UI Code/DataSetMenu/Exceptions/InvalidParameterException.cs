using System;

public class InvalidParameterException: Exception
{
    public InvalidParameterException(string message) : base(message) { }
}
