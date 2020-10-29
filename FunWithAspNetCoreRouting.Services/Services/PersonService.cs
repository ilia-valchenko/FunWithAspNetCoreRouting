using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FunWithAspNetCoreRouting.Domain.Entities;
using FunWithAspNetCoreRouting.Services.Services.Interfaces;

namespace FunWithAspNetCoreRouting.Services.Services
{
    public class PersonService : IPersonService
    {
        private readonly IEnumerable<Person> persons = new[]
        {
            new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "Ilya",
                LastName = "Valchanka",
                Age = 25
            },
            new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Doe",
                Age = 30
            },
            new Person
            {
                Id = Guid.NewGuid(),
                FirstName = "John",
                LastName = "Smith",
                Age = 31
            }
        };

        public async Task<IEnumerable<Person>> GetAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(() => persons, cancellationToken);
        }

        public async Task<IEnumerable<Person>> GetAsync(string firstName, CancellationToken cancellationToken = default(CancellationToken))
        {
            return string.IsNullOrWhiteSpace(firstName)
                ? await Task.Run(() => this.GetAsync(cancellationToken))
                : await Task.Run(() => persons.Where(p => string.Equals(p.FirstName, firstName, StringComparison.OrdinalIgnoreCase)), cancellationToken);
        }

        public async Task<Person> GetAsync(Guid id, CancellationToken cancellationToken = default(CancellationToken))
        {
            return await Task.Run(
                () => persons.FirstOrDefault(p => p.Id == id),
                cancellationToken);
        }
    }
}