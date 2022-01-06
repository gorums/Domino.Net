using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace Domino.Net.Infrestructure.Persistence;

public class UnitOfWork : IUnitOfWork
{
    private readonly Dictionary<IAggregateDataModel, IUnitOfWorkRepository> addedEntities;
    private readonly Dictionary<IAggregateDataModel, IUnitOfWorkRepository> changedEntities;
    private readonly Dictionary<IAggregateDataModel, IUnitOfWorkRepository> deletedEntities;

    public UnitOfWork()
    {
        addedEntities = new Dictionary<IAggregateDataModel, IUnitOfWorkRepository>();
        changedEntities = new Dictionary<IAggregateDataModel,IUnitOfWorkRepository>();
        deletedEntities = new Dictionary<IAggregateDataModel, IUnitOfWorkRepository>();
    }

    public async Task CommitAsync(CancellationToken cancellationToken = default)
    {
        using TransactionScope scope = new TransactionScope();
            
        foreach (IAggregateDataModel entity in this.addedEntities.Keys)
        {
            await this.addedEntities[entity].PersistCreationOfAsync(entity, cancellationToken);
        }

        foreach (IAggregateDataModel entity in this.changedEntities.Keys)
        {
            await this.changedEntities[entity].PersistUpdateOfAsync(entity, cancellationToken);
        }

        foreach (IAggregateDataModel entity in this.deletedEntities.Keys)
        {
            await this.changedEntities[entity].PersistDeleteOfAsync(entity, cancellationToken);
        }

        scope.Complete();
        Clear();            
    }        

    public void RegisterNew(IAggregateDataModel entity, IUnitOfWorkRepository unitofWorkRepository)
    {
        if (!addedEntities.ContainsKey(entity))
        {
            addedEntities.Add(entity, unitofWorkRepository);
        };
    }

    public void RegisterUpdate(IAggregateDataModel entity, IUnitOfWorkRepository unitofWorkRepository)
    {
        if (!changedEntities.ContainsKey(entity))
        {
            changedEntities.Add(entity, unitofWorkRepository);
        }
    }

    public void RegisterDelete(IAggregateDataModel entity, IUnitOfWorkRepository unitofWorkRepository)
    {
        if (!deletedEntities.ContainsKey(entity))
        {
            deletedEntities.Add(entity, unitofWorkRepository);
        }
    }

    private void Clear()
    {
        addedEntities.Clear();
        changedEntities.Clear();
        deletedEntities.Clear();
    }        
}

