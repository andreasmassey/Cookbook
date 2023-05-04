using System;

namespace Cookbook.Models.Entities
{
    public class UserEntity
    {
        public long User_ID { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordHash { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateCreated { get; set; }
    }
}
