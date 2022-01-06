using Domino.Net.Core.Exceptions;
using ValueOf;

namespace Domino.Net.Core.ValueObjects;

public class PackCount : ValueOf<int, PackCount>
{
    private readonly int packCount;

    public PackCount() : this(10)
    {
    }

    public PackCount(int packCount)
    {
        this.packCount = packCount;
    }

    protected override void Validate()
    {
        base.Validate();

        if (Value > packCount || Value < 0)
        {
            throw new PackCountException("The pack needs to have at least 2 pieces and not more that 10 pieces.");
        }
    }
}
