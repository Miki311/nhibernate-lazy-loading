using LazyLoading.Domain.Infrastructure;
using NHibernate;

namespace LazyLoading.Domain.Repositories {
    /// <summary>
    /// Base NHibernate repository.
    /// </summary>
    /// <typeparam name="T">Type of entity the repository is for.</typeparam>
    /// <typeparam name="TID">Type of the ID for the entity.</typeparam>
    public abstract class Repository<T> where T : class {
        protected Repository() {
        }


        protected ISession Session
        {
            get { return NHibernateSessionManager.GetCurrentSession(); }
        }
    }
}
