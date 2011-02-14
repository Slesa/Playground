using System.Collections.Generic;
using DataAccess;

namespace MatrixAccess
{
    public interface IMatrixFileLoader
    {
        object GetById(object id);
    }

    public interface IMatrixFileLoader<TEntity> : IMatrixFileLoader where TEntity : DomainEntity
    {
        Dictionary<int, TEntity> Elements { get; }
        string FullFileName { get; }
        string FullPathName { get; }
    }

}