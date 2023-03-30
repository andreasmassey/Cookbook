namespace Cookbook.Models.Contracts
{
    public class SaveUserContract
    {
        public class Request {
            public string Email { get; set; }
            public string Password { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class Response {
            public long UserID { get; set; }
        }
    }
}
