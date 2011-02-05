using System.Linq;
using Godot.Model;
using Godot.PmsModel.Entities;
using NHibernate;
using NHibernate.Linq;

namespace Godot.PmsModel.Queries
{
    public class FileModificationQuery : IDomainQuery<FileModificationDate>
    {
        readonly string _fileName;

        public FileModificationQuery(string fileName)
        {
            _fileName = fileName;
        }

        public FileModificationDate Execute(ISession session)
        {
            return session.Linq<FileModificationDate>().Where(fn => fn.FileName == _fileName).FirstOrDefault();
        }
    }
}