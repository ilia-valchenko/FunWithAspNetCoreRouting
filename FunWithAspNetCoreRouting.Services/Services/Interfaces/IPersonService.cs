using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FunWithAspNetCoreRouting.Domain.Entities;

namespace FunWithAspNetCoreRouting.Services.Services.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<PersonEntity>> GetAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<PersonEntity>> GetAsync(string firstName, CancellationToken cancellationToken = default(CancellationToken));

        Task<PersonEntity> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    }
}