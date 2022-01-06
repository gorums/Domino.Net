using Domino.Net.Core.Exceptions;
using ValueOf;

namespace Domino.Net.Core.ValueObjects;

public class SquareDots : ValueOf<int, SquareDots>
{
    private readonly int maximeDots;

    public SquareDots() : this(9)
    {
    }

    public SquareDots(int maximeDots)
    {
        this.maximeDots = maximeDots;
    }

    protected override void Validate()
    {
        base.Validate();

        if (Value > maximeDots || Value < 0)
        {
            throw new SquareDotsException($"The square dots needs to be from 0 to {maximeDots}");
        }
    }
}

