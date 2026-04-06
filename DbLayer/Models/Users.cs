using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace DbLayer.Models
{
    public class Users
    {
        public int UserId { get; set; }

        public string Email { get; set; } = null!;

        public string Password { get; set; } = null!;

        public string Role { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;

        public DateTime ModifiedDate { get; set; } = DateTime.Now;

        //public virtual UserRole Role { get; set; } = null!;

        //public virtual ICollection<UserDetail> UserDetails { get; set; } = new List<UserDetail>();
    }
}
