using Domino.Net.Core.ValueObjects;
using System;
using System.Collections.Generic;

namespace Domino.Net.Core.Entities;

public class Game : BaseEntity<Guid>
{
    public string Name { get; internal set; } = string.Empty;

    public ICollection<Player> Players { get; internal set; } = new List<Player>();

    public GameConfiguration GameSetting { get; internal set; }

    public Game(string name, GameConfiguration gameSetting)
    {
        Id = Guid.NewGuid();
        Name = name;
        GameSetting = gameSetting;
    } 

    public void AddPlayer(Player player)
    {
        if (Players.Count == 4)
        {
            throw new InvalidOperationException(); // todo custom exception
        }

        Players.Add(player);
    }

    public bool HasAvailableSlot => Players.Count < 4;
}

