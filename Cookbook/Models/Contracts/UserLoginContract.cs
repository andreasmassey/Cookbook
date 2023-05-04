using Cookbook.Models.Entities;
using System.Collections;
using System.Collections.Generic;

namespace Cookbook.Models.Contracts
{
    public class UserLoginContract
    {
        public class UserLoginRequest{
            public string Email { get; set; }
            public string Password { get; set; }
        }

        public class UserLoginResponse
        {
            public long UserID { get; set; }
            public string Email { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Recipes Recipes { get; set; }
            public ErrorResponse Error { get; set; }
        }

    }
}
