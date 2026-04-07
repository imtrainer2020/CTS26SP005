using System;
using System.Collections.Generic;
using System.Text;

namespace DbLayer.Models
{
    public class UserDetail
    {
        public int UDId { get; set; }

        public int UserId { get; set; }

        public string? Address { get; set; }

        public string? City { get; set; }

        public byte[]? Photo { get; set; }
       
        public string? PostCode { get; set; }

        public string? Phone { get; set; }


        //public Users User { get; set; } = null!;
    }
}
