using System.Threading;
using System.Threading.Tasks;

namespace Domino.Net.Infrestructure.Persistence;

public interface IUnitOfWork
{   
    void RegisterNew(IAggregateDataModel entity, IUnitOfWorkRepository unitofWorkRepository);

    void RegisterUpdate(IAggregateDataModel entity, IUnitOfWorkRepository unitofWorkRepository);

    void RegisterDelete(IAggregateDataModel entity, IUnitOfWorkRepository unitofWorkRepository);

    Task CommitAsync(CancellationToken cancellationToken = default);
}
