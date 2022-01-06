using System.Threading;
using System.Threading.Tasks;

namespace Domino.Net.Infrestructure.Persistence;

public interface IUnitOfWorkRepository
{
    Task PersistCreationOfAsync(IAggregateDataModel entity, CancellationToken cancellationToken = default);
    Task PersistUpdateOfAsync(IAggregateDataModel entity, CancellationToken cancellationToken = default);
    Task PersistDeleteOfAsync(IAggregateDataModel entity, CancellationToken cancellationToken);
}
