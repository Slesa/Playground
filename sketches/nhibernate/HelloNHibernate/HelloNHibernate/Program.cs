using System;
using System.Reflection;
using NHibernate;
using NHibernate.Cfg;

namespace HelloNHibernate
{
    static class Program
    {
        private static ISessionFactory _sessionFactory;

        static void Main()
        {
            CreateEmployeeAndSave();
            UpdateTobinAndAssignPierre();
            LoadEmployees();
            Console.WriteLine("Press key to continue...");
            Console.ReadKey();
        }

        private static void LoadEmployees()
        {
            using (var session = OpenSession())
            {
                var query = session.CreateQuery("from Employee as emp order by emp.Name asc");
                var employees = query.List<Employee>();
                Console.WriteLine("\n{0} employees found", employees.Count);
                foreach(var employee in employees)
                    Console.WriteLine(employee.SayHello());
            }
        }

        private static void UpdateTobinAndAssignPierre()
        {
            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    var query = session.CreateQuery("from Employee where Name='Tobin Harris'");
                    var tobin = query.List<Employee>()[0];
                    tobin.Name = "Tobin David Harris";

                    var pierre = new Employee {Name = "Pierre Henri Kuate"};
                    tobin.Manager = pierre;
                    transaction.Commit();
                    Console.WriteLine("Updated Tobin and added Pierre");
                }
            }
        }

        private static void CreateEmployeeAndSave()
        {
            var tobin = new Employee { Name = "Tobin Harris" };
            using (var session = OpenSession())
            {
                using (var transaction = session.BeginTransaction())
                {
                    session.Save(tobin);
                    transaction.Commit();
                }
            }
        }

        private static ISession OpenSession()
        {
            if (_sessionFactory == null)
            {
                var configuration = new Configuration();
                configuration.AddAssembly(Assembly.GetCallingAssembly());
                _sessionFactory = configuration.BuildSessionFactory();
            }
            return _sessionFactory.OpenSession();
        }
    }
}
