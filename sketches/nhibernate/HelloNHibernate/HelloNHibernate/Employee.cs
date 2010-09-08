namespace HelloNHibernate
{
    public class Employee
    {
        public int Id;
        public string Name;
        public Employee Manager;

        public string SayHello()
        {
            var result = string.Format("Hello, this is {0}.", Name);
            if (Manager != null)
                result += string.Format(" My Manager is {0}", Manager.Name);
            return result;
        }
    }
}