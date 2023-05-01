using Shared.ValueObjects;

namespace Tests.Shared.ValueObjects
{
    [TestClass]
    public class ZipCodeTest
    {
        [TestMethod]
        public void Should_return_true_when_valid_zipCode()
        {
            ZipCode zipCode = new("44780-000");
            Assert.IsTrue(zipCode.IsValid);
        }

        [TestMethod]
        public void Should_return_false_when_invalid_zipCode()
        {
            Email zipCode = new("any_value");
            Assert.IsFalse(zipCode.IsValid);
        }

        [TestMethod]
        public void Should_return_false_when_blank()
        {
            Email zipCode = new("");
            Assert.IsFalse(zipCode.IsValid);
        }
    }
}