using Domino.Net.Core.ValueObjects;
using ValueOf;

namespace Domino.Net.Core;

public class Piece : ValueOf<(SquareDots, SquareDots), Piece>
{
    public Piece()
    {
    }

    public Piece(SquareDots topSquareDots, SquareDots bottomSquareDots)
    {
        TopSquareDots = topSquareDots;
        BottomSquareDots = bottomSquareDots;
    }

    public SquareDots TopSquareDots { get; private set; } = new();

    public SquareDots BottomSquareDots { get; private set; } = new();

    public bool IsInTheTable { get; private set; } = false;

    public bool IsInThePack { get; private set; } = false;

    public void SetInTheTable() => IsInTheTable = true;

    public int Sum => TopSquareDots.Value + BottomSquareDots.Value;

    public bool IsDouble => TopSquareDots.Value == BottomSquareDots.Value;

    public bool IsMatching(SquareDots dots) => dots.Value == TopSquareDots.Value || dots.Value == BottomSquareDots.Value; 

    public static Piece Deserialize(string serializePiece)
    {
       var splitPiece = serializePiece.Split(':');

        return new Piece(SquareDots.From(int.Parse(splitPiece[0])), SquareDots.From(int.Parse(splitPiece[1])));
    }

    public override string ToString()
    {
        return $"{TopSquareDots}:{BottomSquareDots}";
    }
}

