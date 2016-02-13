using System;
using System.Web;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate;
using NHibernate.Caches.SysCache;
using NHibernate.Context;
using NHibernate.Tool.hbm2ddl;

namespace LazyLoading.Domain.Infrastructure {
    public class NHibernateSessionManager {
        private static ISessionFactory Factory { get; set; }

        public static string ConnectionStringName { get; set; }


        static NHibernateSessionManager() {
            ConnectionStringName = "DbConnection";
        }


        private static ISessionFactory GetFactory<T>() where T : ICurrentSessionContext {
            return Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                .ConnectionString(c => c.FromConnectionStringWithKey(ConnectionStringName)))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateSessionManager>())
                .Cache(c => c.ProviderClass<SysCacheProvider>().UseQueryCache())
                .CurrentSessionContext<T>().BuildSessionFactory();
        }


        /// <summary>
        /// Gets the current session.
        /// </summary>
        public static ISession GetCurrentSession() {
            if (Factory == null)
                Factory = HttpContext.Current != null ? GetFactory<WebSessionContext>() : GetFactory<ThreadStaticSessionContext>();

            if (CurrentSessionContext.HasBind(Factory))
                return Factory.GetCurrentSession();

            var session = Factory.OpenSession();
            CurrentSessionContext.Bind(session);

            return session;
        }


        /// <summary>
        /// Closes the session.
        /// </summary>
        public static void CloseSession() {
            if (Factory != null && CurrentSessionContext.HasBind(Factory)) {
                var session = CurrentSessionContext.Unbind(Factory);
                session.Close();
            }
        }


        /// <summary>
        /// Commits the session.
        /// </summary>
        /// <param name="session">The session.</param>
        public static void CommitSession(ISession session) {
            try {
                session.Transaction.Commit();
            }
            catch (Exception) {
                session.Transaction.Rollback();
                throw;
            }
        }


        /// <summary>
        /// Creates the database from mapping in this assembly
        /// </summary>
        public static void CreateSchemaFromMappings() {
            var config = Fluently.Configure()
                .Database(MsSqlConfiguration.MsSql2008
                .ConnectionString(c => c.FromConnectionStringWithKey(ConnectionStringName)))
                .Mappings(m => m.FluentMappings.AddFromAssemblyOf<NHibernateSessionManager>());

            new SchemaExport(config.BuildConfiguration()).Create(false, true);
        }
    }
}
