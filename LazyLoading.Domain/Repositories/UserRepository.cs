using System.Linq;
using LazyLoading.Domain.Entities;
using NHibernate.Linq;

namespace LazyLoading.Domain.Repositories {
    public class UserRepository : Repository<User> {
        public IQueryable<User> GetAll() {
            return this.Session.Query<User>();
        }
    }
}
