using System.Collections.Generic;

namespace LazyLoading.Domain.Entities {
    public class User {
        public virtual int Id { get; set; }
        public virtual string FirstName { get; set; }
        public virtual string LastName { get; set; }
        public virtual ICollection<Role> Roles { get; set; }
        public virtual ICollection<Project> Projects { get; set; } 
    }

    public class Role {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }

    public class Project {
        public virtual int Id { get; set; }
        public virtual string Name { get; set; }
    }
}
