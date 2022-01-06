using System;

namespace Domino.Net.Core.Entities;

public class User : BaseEntity<Guid>
{
    public string UserName { get; private set; }

    public User(string userName)
    {
        UserName = userName;
    }
}

