using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using FunWithAspNetCoreRouting.Domain.Entities;

namespace FunWithAspNetCoreRouting.Services.Services.Interfaces
{
    public interface IPersonService
    {
        Task<IEnumerable<Person>> GetAsync(CancellationToken cancellationToken = default(CancellationToken));

        Task<IEnumerable<Person>> GetAsync(string firstName, CancellationToken cancellationToken = default(CancellationToken));

        Task<Person> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken));
    }
}