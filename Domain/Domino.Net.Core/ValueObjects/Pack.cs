using Domino.Net.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using ValueOf;

namespace Domino.Net.Core.ValueObjects;

public class Pack : ValueOf<int, PackCount>
{
    public IEnumerable<Piece> Pieces { get; set; }

    public Pack() : this(new List<Piece>(), PackCount.From(10))
    {
    }

    public Pack(IEnumerable<Piece> pieces, PackCount packCount)
    {
        Pieces = pieces ?? throw new ArgumentNullException(nameof(pieces));

        if (Pieces.Count() != packCount.Value)
        {
            throw new ArgumentException($"We need to have {packCount} pieces.");
        }
    }   

    public int Sum => Pieces.Where(p => !p.IsInTheTable).Sum(p => p.Sum);

    public int Count => Pieces.Count(p => !p.IsInTheTable);

    public string Serialize()
    {
        return String.Join(";", Pieces.Select(p => p.ToString()));
    }

    public static Pack Deserialize(string serializePiece)
    {
        var splitPieces = serializePiece.Split(';');

        return new Pack(splitPieces.Select(p =>  Piece.Deserialize(p)), PackCount.From(10));
    }
}

