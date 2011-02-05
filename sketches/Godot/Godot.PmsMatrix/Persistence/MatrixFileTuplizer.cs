using NHibernate.Mapping;
using NHibernate.Tuple;
using NHibernate.Tuple.Entity;

namespace Godot.PmsMatrix.Persistence
{
    public class MatrixFileTuplizer : PocoEntityTuplizer
    {
        public MatrixFileTuplizer(EntityMetamodel entityMetamodel, PersistentClass mappedEntity)
            : base(entityMetamodel, mappedEntity)
        {
        }

        protected override IInstantiator BuildInstantiator(PersistentClass persistentClass)
        {
            return new MatrixFileInstantiator(persistentClass.MappedClass);
        }
        
    }
}