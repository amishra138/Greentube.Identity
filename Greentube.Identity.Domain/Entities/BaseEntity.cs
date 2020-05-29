using Microsoft.AspNetCore.Identity;
using System;

namespace Greentube.Identity.Domain.Entities
{
    /// <summary>
    /// To be used as a base class for all the entities
    /// </summary>
    public abstract class BaseEntity  : IdentityUser
    {
        public Guid Id { get; set; }

        /// <summary>
        /// Record created at, use UTC time
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// Record modified at, use UTC time
        /// </summary>
        public DateTime ModifiedAt { get; set; }
    }
}
