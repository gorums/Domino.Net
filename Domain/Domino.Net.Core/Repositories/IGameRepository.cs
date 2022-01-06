using Domino.Net.Core.Entities;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domino.Net.Core.Repositories;

public interface IGameRepository
{
    void SaveNewGame(Game newGame);
    Task<Game?> FindGameAsync(Guid id, CancellationToken cancellationToken);
    Task<IEnumerable<Game>> FindGamesByUserAsync(Guid userId, CancellationToken cancellationToken);
    Task<IEnumerable<Game>> FindGamesByNameAsync(string name, CancellationToken cancellationToken);
    void SaveNewPlayer(Game game, Player newPlayer);
}
