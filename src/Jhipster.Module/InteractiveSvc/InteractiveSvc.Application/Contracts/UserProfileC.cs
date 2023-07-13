using System.Runtime.Serialization;

namespace InteractiveSvc.Application.Contract.Contracts
{
   
    public class UserProfileRequest
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string? Avatar { get; set; }
        public string? Fullname { get; set; }
        public string? Address { get; set; }
        public string? Major { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Type { get; set; }

    }

    public class UserProfileResponse
    {
        public string Id { get; set; }
        public string Username { get; set; }
        public string? Avatar { get; set; }
        public string Fullname { get; set; }
        public string? Address { get; set; }
        public string? Major { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string Type { get; set; }
    }
}
