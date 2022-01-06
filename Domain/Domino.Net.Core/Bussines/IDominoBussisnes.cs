using Domino.Net.Core.Entities;
using Domino.Net.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domino.Net.Core.Bussisnes;

public interface IDominoBussisnes
{        
    Game CreateNewGame(string name, GameConfiguration gameConfiguration);
    Player AddNewPayer(User user, Game game);
    Task<Game?> FindGameAsync(Guid id, CancellationToken cancellationToken = default);
    Task<IEnumerable<Game>> FindGamesByUserAsync(User user, CancellationToken cancellationToken = default);
    Task<IEnumerable<Game>> FindGamesByNameAsync(string name, CancellationToken cancellationToken = default);
}