using App.Core;
using System;

namespace Users.Domain
{
    public class User : AggregateRoot
    {
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public DateTimeOffset RegisteredAt { get; set; }
    }
}
