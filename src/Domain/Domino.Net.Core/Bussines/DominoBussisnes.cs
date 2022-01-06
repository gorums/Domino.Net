using Domino.Net.Core.Entities;
using Domino.Net.Core.Repositories;
using Domino.Net.Core.ValueObjects;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Domino.Net.Core.Bussisnes;

public class DominoBussisnes : IDominoBussisnes
{
    private readonly IGameRepository gameRepository;

    public DominoBussisnes(IGameRepository gameRepository)
    {
        this.gameRepository = gameRepository;
    }

    public Game CreateNewGame(string name, GameConfiguration gameConfiguration)
    {
        var newGame = new Game(name, gameConfiguration);

        gameRepository.SaveNewGame(newGame);

        return newGame;
    }

    public Player AddNewPayer(User user, Game game)
    {
        if (!game.HasAvailableSlot)
        {
            throw new Exception(); // custom exception here
        }

        var newPlayer = new Player(user, game);
        game.AddPlayer(newPlayer);

        gameRepository.SaveNewPlayer(game, newPlayer);

        return newPlayer;
    }

    public async Task<Game?> FindGameAsync(Guid id, CancellationToken cancellationToken = default) => await gameRepository.FindGameAsync(id, cancellationToken);

    public async Task<IEnumerable<Game>> FindGamesByUserAsync(User user, CancellationToken cancellationToken = default) => await gameRepository.FindGamesByUserAsync(user.Id, cancellationToken);

    public async Task<IEnumerable<Game>> FindGamesByNameAsync(string name, CancellationToken cancellationToken = default) => await gameRepository.FindGamesByNameAsync(name, cancellationToken);
    
}

