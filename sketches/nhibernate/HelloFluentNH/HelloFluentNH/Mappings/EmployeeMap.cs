using FluentNHibernate.Mapping;
using HelloFluentNH.Entities;

namespace HelloFluentNH.Mappings
{
    public class EmployeeMap : ClassMap<Employee>
    {
        public EmployeeMap()
        {
            Table("Employee");
            Id(d => d.Id).GeneratedBy.Identity();
            Map(d => d.Name).Length(50);
            References(d => d.Manager).Cascade.All().ForeignKey("Manager");
        }
    }
}
