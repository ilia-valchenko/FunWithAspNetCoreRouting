namespace FunWithAspNetCoreRouting.Domain.Entities
{
    public class PersonEntity : BaseEntity
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int Age { get; set; }
    }
}