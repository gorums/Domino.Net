using Domino.Net.Core.Entities;
using Domino.Net.Core.Repositories;
using Domino.Net.Infrestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domino.Net.Sql.Repositories;

public class GameRepository : IGameRepository, IUnitOfWorkRepository
{
    private IUnitOfWork _unitOfWork;
    public GameRepository(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public Task<Game> FindGameAsync(Guid id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Game>> FindGamesByNameAsync(string name, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public Task<IEnumerable<Game>> FindGamesByUserAsync(Guid userId, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }

    public void SaveNewGame(Game newGame)
    {
        throw new NotImplementedException();
    }

    public void SaveNewPlayer(Game game, Player newPlayer)
    {
        throw new NotImplementedException();
    }

    public Task PersistCreationOfAsync(IAggregateDataModel entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task PersistUpdateOfAsync(IAggregateDataModel entity, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }

    public Task PersistDeleteOfAsync(IAggregateDataModel entity, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}

