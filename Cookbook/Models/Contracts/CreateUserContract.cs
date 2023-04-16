namespace Cookbook.Models.Contracts
{
    public class CreateUserContract
    {
        public class CreateUserRequest 
        {
            public string Email { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class CreateUserResponse
        {
            public long UserID { get; set; }
            public ErrorResponse Error { get; set; }
        }
    }
}
