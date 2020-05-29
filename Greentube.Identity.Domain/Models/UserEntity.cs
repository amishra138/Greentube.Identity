namespace Greentube.Identity.Domain.Entities
{
    /// <summary>
    /// Represents user entity details
    /// </summary>
    public class UserEntity : BaseEntity
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string PasswordHash { get; set; }
        public bool IsDeleted { get; set; }
    }
}
