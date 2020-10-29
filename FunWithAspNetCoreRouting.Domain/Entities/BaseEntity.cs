using System;

namespace FunWithAspNetCoreRouting.Domain.Entities
{
    public abstract class BaseEntity
    {
        public Guid Id { get; set; }
    }
}