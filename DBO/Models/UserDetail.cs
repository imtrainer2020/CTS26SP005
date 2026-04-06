using System;
using System.Collections.Generic;

namespace DBO.Models;

public partial class UserDetail
{
    public int UDId { get; set; }

    public int UserId { get; set; }

    public string? UDAddress { get; set; }

    public string? UDCity { get; set; }

    public string? UDPostCode { get; set; }

    public string? UDPhone { get; set; }

    public byte[]? UDPhoto { get; set; }

    public virtual User User { get; set; } = null!;
}
