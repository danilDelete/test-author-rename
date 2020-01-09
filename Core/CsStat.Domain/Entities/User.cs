using CsStat.Domain.Definitions;
using MongoRepository;

namespace CsStat.Domain.Entities
{
    public class User : Entity
    {
        public string Name { get; set; }
        public string PasswordHash { get; set; }
        public GrantsEnum Grants { get; set; }
    }
}