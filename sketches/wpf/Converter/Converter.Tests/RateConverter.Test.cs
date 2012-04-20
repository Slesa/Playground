using System.Globalization;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Converter.Tests
{
    [TestClass]
    public class ConverterTest
    {
        RateConverter _sut;

        [TestInitialize]
        public void TestInitialize()
        {
            _sut = new RateConverter();
        }

        [TestMethod]
        public void convert_back_uses_inverted_rate()
        {
            // TODO: decide, if it should throw NotSupportedException or return null
            //var result = _sut.ConvertBack(null, typeof(string), null, null);

            var result = _sut.ConvertBack("1", typeof (string), 0.5, null);
            Assert.AreEqual(2.0, result);
        }

        [TestMethod]
        public void target_type_is_ignored()
        {
            var result = _sut.Convert("1", null, 1.0, null);
            Assert.AreEqual(1.0, result);
        }

        [TestMethod]
        public void null_string_results_in_0()
        {
            var result = _sut.Convert(null, typeof (string), 1.0, null);
            Assert.AreEqual(0.0, result);
        }

        [TestMethod]
        public void empty_string_results_in_0()
        {
            var result = _sut.Convert("", typeof (string), 1.0, null);
            Assert.AreEqual(0.0, result);
        }

        [TestMethod]
        public void non_empty_string_results_in_0()
        {
            var result = _sut.Convert("HelloWorld", typeof (string), 1.0, null);
            Assert.AreEqual(0.0, result);
        }

        [TestMethod]
        public void null_rate_results_in_0()
        {
            var result = _sut.Convert("1", typeof (string), null, null);
            Assert.AreEqual(0.0, result);
        }

        [TestMethod]
        public void rate_as_int_results_in_0()
        {
            var result = _sut.Convert("1", typeof (string), 2, null);
            Assert.AreEqual(0.0, result);
        }

        [TestMethod]
        public void one_returns_rate()
        {
            var result = _sut.Convert("1", typeof(string), 0.375, null);
            Assert.AreEqual(0.375, result);
        }

        [TestMethod]
        public void rate_of_two_doubles_value()
        {
            var result = _sut.Convert("0,75", typeof(string), 2.0, null);
            Assert.AreEqual(1.5, result);
        }

        [TestMethod]
        public void half_returns_half_of_rate()
        {
            var result = _sut.Convert("0,5", typeof(string), 0.5, null);
            Assert.AreEqual(0.25, result);
        }

        [TestMethod]
        public void half_in_german_format_returns_half_of_rate()
        {
            var result = _sut.Convert("0,5", typeof(string), 0.5, null);
            Assert.AreEqual(0.25, result);
        }

        [TestMethod]
        public void given_culture_info_for_convert_is_ignored()
        {
            var result = _sut.Convert("0.5", typeof(string), 0.5, new CultureInfo("en-US"));
            Assert.AreEqual(0.25, result);
        }

        [TestMethod]
        public void a_number_greater_1_multiplies_rate()
        {
            var result = _sut.Convert("9,11", typeof(string), 0.375, null);
            Assert.AreEqual(9.11*0.375, result);
        }

        [TestMethod]
        public void be_aware_of_rounding_errors()
        {
            var result = _sut.Convert("0,333", typeof(string), 3.0, null);
            Assert.AreNotEqual(0.999, result);
        }
    }
}
