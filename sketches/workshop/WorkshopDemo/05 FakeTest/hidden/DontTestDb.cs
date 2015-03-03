using System;

namespace FakeTest
{
    public class TestSinglePriceEuro
    {
        const int PriceId = 24; // Magic Number!
        const string TableName = "europrices";
        DbTable _tbl;
        SinglePriceEuro _singlePriceEuro = new SinglePriceEuro { Id = new Guid()};

        /// <summary>
        /// Ein Test für GetBindingName
        /// </summary>
        [TestMethod]
        public void GetBindingNameTest()
        {
            var singlePriceEuro = new SinglePriceEuro();
            Assert.IsTrue(singlePriceEuro.GetBindingName("Price")!=string.Empty);
        }

        /// <summary>
        /// Ein Test für GetByDataRow
        /// </summary>
        [TestMethod]
        public void GetByDataRowTest()
        {
            InsertSinglePriceEuro();
            var singlePriceEuro = new SinglePriceEuro();
            using (var dbService = new DbService())
            {
                _tbl = dbService.ExecSqlReturnDataTable("SELECT * FROM " + TableName + "WHERE PRICE_ID=" + PriceId);
                singlePriceEuro.GetByDataRow(_tbl.Rows[0]);
            }
            Assert.IsTrue(singlePriceEuro!=null && singlePriceEuro.Id!=new Guid());
            DeleteSinglePriceEuro();
        }

        /// <summary>
        /// Ein Test für GetById
        /// </summary>
        [TestMethod]
        public void GetByIdTest()
        {
            InsertSinglePriceEuro();
            var singlePriceEuro = new SinglePriceEuro();
            singlePriceEuro.GetById(_singlePriceEuro.Id);
            Assert.IsTrue(singlePriceEuro!=null && singlePriceEuro.Id!=new Guid());
            DeleteSinglePriceEuro();
        }






        #region Helpers
        void DeleteSinglePriceEuro()
        {
        }

        void InsertSinglePriceEuro()
        {
        }
        #endregion
    }

    #region Hide me
    public class DbService : IDisposable
    {
        public void Dispose()
        {
        }

        public DbTable ExecSqlReturnDataTable(string s)
        {
            return null;
        }
    }

    public class DbTable
    {
        public string[] Rows
        {
            get { return new string[1]; }
        }
    }

    class SinglePriceEuro
    {
        public string GetBindingName(string price)
        {
            return string.Empty;
        }

        public void GetByDataRow(string str)
        {
        }

        public Guid Id { get; set; }

        public void GetById(Guid id)
        {
        }
    }

    class TestMethodAttribute : Attribute
    {
    }

    public static class Assert
    {
        public static void IsTrue(bool value)
        {
        }
    }
    #endregion
}