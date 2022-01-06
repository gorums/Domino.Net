using Domino.Net.Core.Enum;
using System.Collections.Generic;
using ValueOf;

namespace Domino.Net.Core.ValueObjects;

public class GameConfiguration : ValueOf<(PackCount, Dots), GameConfiguration>
{   
    public GameConfiguration()
    {
    }

    public GameConfiguration(PackCount packCount, Dots dots = Dots.NINE)
    {
        PackCount = packCount;
        Dots = dots;

        this.GeneratePieces();
    }

    public PackCount PackCount { get; private set; } = new();

    public Dots Dots { get; private set; } = Dots.NINE;

    public List<Piece> Pieces { get; internal set; } = new();

    private void GeneratePieces()
    {
        Pieces = new List<Piece>();
    }

    public (Pack, Pack, Pack, Pack) GenerateRamdonPacks()
    {
        return
        (
            new Pack(new List<Piece>(), PackCount.From(10)),
            new Pack(new List<Piece>(), PackCount.From(10)),
            new Pack(new List<Piece>(), PackCount.From(10)),
            new Pack(new List<Piece>(), PackCount.From(10))
        );
    }
}
