using System;

namespace Domino.Net.Core.Exceptions;

public class PackCountException : ArgumentException
{
    public PackCountException(string msg): base(msg)
    {

    }
}

