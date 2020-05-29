namespace Greentube.Identity.Domain.Models
{
    /// <summary>
    /// System user details, to be used as DTO
    /// </summary>
    public class UserModel
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
    }
}
