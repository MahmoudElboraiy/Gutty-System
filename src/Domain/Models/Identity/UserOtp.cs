using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Models.Identity
{
    public class UserOtp
    {
        public int Id { get; set; }
        public string PhoneNumber { get; set; } = null!;
        public string Code { get; set; } = null!;
        public DateTime ExpirationTime { get; set; }
        public bool IsVerified { get; set; } = false;
    }
}
