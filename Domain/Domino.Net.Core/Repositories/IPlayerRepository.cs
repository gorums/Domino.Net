using Domino.Net.Core.Entities;
using System.Threading;
using System.Threading.Tasks;

namespace Domino.Net.Core.Repositories;

public interface IPlayerRepository
{
    Task<Player> CreatePlayerAsync(Player newPlayer, CancellationToken cancellationToken);
}
