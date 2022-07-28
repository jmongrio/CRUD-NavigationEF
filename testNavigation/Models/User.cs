using System;
using System.Collections.Generic;

namespace testNavigation.Models
{
    public partial class User
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? LastName { get; set; }
        public int? IdActive { get; set; }

        public virtual Active? IdActiveNavigation { get; set; }
    }
}
