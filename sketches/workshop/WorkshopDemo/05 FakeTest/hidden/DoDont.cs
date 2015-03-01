using System.Collections.Generic;
using System.Linq;

namespace FakeTest
{
    public class DataManager
    {
        DbContext _dbContext;

        public DataManager()
        {
            _dbContext = new DbContext();
        }

        public Datum findById(int id)
        {
            return _dbContext.Find(id);
        }
    }


    public class Datum {
        public int Id { get; set; }
    }

    class DbContext
    {
        List<Datum> _dates;

        public Datum Find(int id)
        {
            var result = from date in _dates where date.Id==id select date;
            return result.FirstOrDefault();
        }
    }
}