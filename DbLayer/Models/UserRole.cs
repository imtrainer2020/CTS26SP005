using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DbLayer.Models
{
    public class UserRole
    {
        public string RoleName { get; set; } = null!;

        //public virtual ICollection<Users> Users { get; set; } = new List<Users>();
    }
}
