using System;
using System.Linq;
using LazyLoading.Domain.Entities;
using LazyLoading.Domain.Infrastructure;
using NHibernate;
using NHibernate.Linq;

namespace LazyLoading {
    public class Program {
        static void Main(string[] args) {
            using (ISession session = NHibernateSessionManager.GetCurrentSession()) {
                var allUsers = session.Query<User>()
                    .FetchMany(x => x.Projects)
                    .ToFuture();

                session.Query<User>()
                    .FetchMany(x => x.Roles)
                    .ToFuture();

                foreach (User user in allUsers.ToList()) {
                    OutputUserInfo(user);
                }
            }
        }
        
        static void OutputUserInfo(User user) {
            Console.WriteLine($"User: { user.FirstName } { user.LastName }");
            Console.WriteLine("Projects of user: ");
            
            foreach (Project project in user.Projects) {
                Console.WriteLine(project.Name);
            }

            Console.WriteLine("Roles of user: ");
            foreach (Role role in user.Roles) {
                Console.WriteLine(role.Name);
            }

            Console.WriteLine("----------------------------------------------------------------");
        }
    }
}
