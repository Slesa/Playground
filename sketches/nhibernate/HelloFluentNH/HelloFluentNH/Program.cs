using System;
using HelloFluentNH.Entities;
using NHibernate;

namespace HelloFluentNH
{
    static class Program
    {
        private static ISessionFactory _sessionFactory;

        static void Main()
        {
            CreateEmployeeAndSave();
            LoadEmployees();

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
                Console.WriteLine("{0} employees found", employees.Count);
                foreach (var employee in employees)
                {
                    Console.Write("Hello {0}", employee.Name);
                    if( employee.Manager!=null )
                        Console.Write(", your Manager is {0}", employee.Manager.Name);
                    Console.WriteLine();
                }
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
            var tobin = new Employee {Name = "Tobin Harris"};
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
                _sessionFactory = NhConfiguration.CreateSessionFactory();
            return _sessionFactory.OpenSession();
        }
    }
}
