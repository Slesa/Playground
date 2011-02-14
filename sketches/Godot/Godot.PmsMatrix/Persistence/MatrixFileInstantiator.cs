using System;
using Microsoft.Practices.ServiceLocation;
using NHibernate.Tuple;

namespace Godot.PmsMatrix.Persistence
{
    public class MatrixFileInstantiator : IInstantiator
    {
        private readonly Type _entityType;

        public MatrixFileInstantiator(Type entityType)
        {
            _entityType = entityType;
        }

        public object Instantiate(object id)
        {
            var loaderType = typeof(IMatrixFileLoader<>).MakeGenericType(_entityType);

            var loader = (IMatrixFileLoader)ServiceLocator.Current.GetInstance(loaderType);
            return loader.GetById(id);
        }

        public object Instantiate()
        {
            return Instantiate(null);
        }

        public bool IsInstance(object obj)
        {
            return _entityType.IsInstanceOfType(obj);
        }
    }
}