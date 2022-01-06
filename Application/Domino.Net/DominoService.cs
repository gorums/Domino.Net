using Domino.Net.Core.Bussisnes;
using Domino.Net.Core.Entities;
using Domino.Net.Core.ValueObjects;
using Domino.Net.Infrestructure.Persistence;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domino.Net;

public class DominoService
{
    private readonly IUnitOfWork unitOfWork;
    private readonly IDominoBussisnes dominoBussisnes;    

    public DominoService(IUnitOfWork unitOfWork, IDominoBussisnes dominoBussisnes)
    {
        this.unitOfWork = unitOfWork;
        this.dominoBussisnes = dominoBussisnes;
    }

    public async Task<Game> CreateNewGameAsync(string name, GameConfiguration gameConfiguration, CancellationToken cancellationToken = default)
    {
        var newGame = this.dominoBussisnes.CreateNewGame(name, gameConfiguration);

        await this.unitOfWork.CommitAsync(cancellationToken);

        return newGame;
    }

    public async Task<Player> AddNewPayerAsync(User user, Game game, CancellationToken cancellationToken = default)
    {
        var newPlayer = this.dominoBussisnes.AddNewPayer(user, game);

        await this.unitOfWork.CommitAsync(cancellationToken);

        return newPlayer;
    }

    public async Task<Game?> FindGameAsync(Guid id, CancellationToken cancellationToken = default) => await this.dominoBussisnes.FindGameAsync(id, cancellationToken);

    public async Task<IEnumerable<Game>> FindGamesByUserAsync(User user, CancellationToken cancellationToken = default) => await this.dominoBussisnes.FindGamesByUserAsync(user, cancellationToken);

    public async Task<IEnumerable<Game>> FindGamesByNameAsync(string name, CancellationToken cancellationToken = default) => await this.dominoBussisnes.FindGamesByNameAsync(name, cancellationToken);

    
}

