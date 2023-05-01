using Shared.ValueObjects;

namespace Tests.Shared.ValueObjects
{
    [TestClass]
    public class TaxIdentifierTest
    {
        private readonly TaxIdentifier _cpfValid;
        private readonly TaxIdentifier _cpfInvalid;
        private readonly TaxIdentifier _cnpjValid;
        private readonly TaxIdentifier _cnpjInvalid;

        public TaxIdentifierTest()
        {
            _cpfValid = new("05841241710");
            _cpfInvalid = new("any_cpf");
            _cnpjValid = new("15105766000108");
            _cnpjInvalid = new("any_cnpj");
        }

        [TestMethod]
        public void Should_return_true_when_valid_cpf()
        {
            Assert.IsTrue(_cpfValid.IsValid);
        }

        [TestMethod]
        public void Should_return_false_when_invalid_cpf()
        {
            Assert.IsFalse(_cpfInvalid.IsValid);
        }

        [TestMethod]
        public void Should_return_true_when_valid_cnpj()
        {
            Assert.IsTrue(_cnpjValid.IsValid);
        }

        [TestMethod]
        public void Should_return_false_when_invalid_cnpj()
        {
            Assert.IsFalse(_cnpjInvalid.IsValid);
        }

        [TestMethod]
        public void Should_return_false_when_blank()
        {
            TaxIdentifier taxIdentifier = new("");
            Assert.IsFalse(taxIdentifier.IsValid);
        }
    }
}