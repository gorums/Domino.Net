using Domino.Net.Core.Entities;
using Domino.Net.Infrestructure.Persistence;
using System;

namespace Domino.Net.Sql.DataModels;

public class GameDataModel : BaseEntity<Guid>, IAggregateDataModel
{
    public string Name { get; internal set; } = string.Empty;
}
