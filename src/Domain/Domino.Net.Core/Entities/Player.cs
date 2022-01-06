using Domino.Net.Core.ValueObjects;
using System;

namespace Domino.Net.Core.Entities;

public class Player : BaseEntity<Guid>
{
    public User User { get; private set; }

    public Game Game { get; private set; }

    public Pack? Hand { get; private set; }    

    public Player(User user, Game game)
    {
        Id = Guid.NewGuid();
        User = user;
        Game = game;
    }

    public void AssignHand(Pack pack)
    {
        if (Hand != null)
        {
            throw new InvalidOperationException(); // todo: custom exception here!!
        }

        Hand = pack;
    }

    public bool DoIWin => Hand != null && Hand.Count == 0;
}

