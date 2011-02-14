using FluentNHibernate.Mapping;
using Godot.PmsModel.Entities;

namespace Godot.PmsMatrix.Mappings
{
    public class FileModificationDateMap : ClassMap<FileModificationDate>
    {
        public FileModificationDateMap()
        {
            Id(d => d.Id).GeneratedBy.Identity();
            Map(d => d.FileName);
            Map(d => d.LastModified);
        }
    }
}