using System;
using System.Collections.Generic;

namespace DBO.Models;

public partial class User
{
    public int UserId { get; set; }

    public string Username { get; set; } = null!;

    public string Password { get; set; } = null!;

    public int RoleId { get; set; }

    public DateTime CreatedDate { get; set; }

    public DateTime ModifiedDate { get; set; }

    public virtual UserRole Role { get; set; } = null!;

    public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
}
