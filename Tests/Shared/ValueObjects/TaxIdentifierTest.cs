using Shared.ValueObjects;

namespace Tests.Shared.ValueObjects
{
    [TestClass]
    public class TaxIdentifierTest
    {
        [TestMethod]
        public void Should_return_true_when_valid_cpf()
        {
            TaxIdentifier cpf = new("05841241710");
            Assert.IsTrue(cpf.IsValid);
        }

        [TestMethod]
        public void Should_return_false_when_invalid_cpf()
        {
            TaxIdentifier cpf = new("any_value");
            Assert.IsFalse(cpf.IsValid);
        }

        [TestMethod]
        public void Should_return_false_when_blank()
        {
            TaxIdentifier cpf = new("");
            Assert.IsFalse(cpf.IsValid);
        }
    }
}