using System;

namespace Domino.Net.Core.Exceptions;

public class SquareDotsException : ArgumentOutOfRangeException
{
    public SquareDotsException(string msg) : base(msg)
    {

    }
}
