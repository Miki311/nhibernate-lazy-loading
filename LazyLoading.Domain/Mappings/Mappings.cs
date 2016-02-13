using FluentNHibernate.Mapping;
using LazyLoading.Domain.Entities;

namespace LazyLoading.Domain.Mappings {
    public class RoleMap : ClassMap<Role> {
        public RoleMap() {
            this.Table("Role");

            this.Id(x => x.Id);

            this.Map(x => x.Name);
        }
    }

    public class UserMap : ClassMap<User> {
        public UserMap() {
            this.Table("[User]");

            this.Id(x => x.Id);

            this.Map(x => x.FirstName);
            this.Map(x => x.LastName);

            this.HasManyToMany(x => x.Roles)
                .Table("UserRole")
                .ParentKeyColumn("UserId")
                .ChildKeyColumn("RoleId");

            this.HasManyToMany(x => x.Projects)
                .Table("UserProject")
                .ParentKeyColumn("UserId")
                .ChildKeyColumn("ProjectId");
        }
    }

    public class ProjectMap : ClassMap<Project> {
        public ProjectMap() {
            this.Table("Project");

            this.Id(x => x.Id);

            this.Map(x => x.Name);
        }
    }
}
