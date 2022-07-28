using System;
using System.Collections.Generic;

namespace testNavigation.Models
{
    public partial class Active
    {
        public Active()
        {
            Users = new HashSet<User>();
        }

        public int Id { get; set; }
        public string? Status { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
