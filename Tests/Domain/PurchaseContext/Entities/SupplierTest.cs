using Domain.PurchaseContext.Entities;
using Shared.ValueObjects;

namespace Tests.Domain.PurchaseContext.Entities
{
    [TestClass]
    public class SupplierTest
    {
        private readonly TaxIdentifier _cpfValid;
        private readonly TaxIdentifier _cpfInvalid;
        private readonly TaxIdentifier _cnpjValid;
        private readonly TaxIdentifier _cnpjInvalid;
        private readonly ZipCode _cepValid;
        private readonly Email _emailValid;

        public SupplierTest()
        {
            this._cpfValid = new("05841241710");
            this._cpfInvalid = new("any_cpf");
            this._cnpjValid = new("15105766000108");
            this._cnpjInvalid = new("any_cnpj");
            this._cepValid = new("44780-000");
            this._emailValid = new("any@any.com");
        }

        [TestMethod]
        public void Shold_return_true_when_valid_supplier_with_cnpj()
        {
            var company = new Supplier(this._cnpjValid, "any_trade_name", this._emailValid, this._cepValid);
            Assert.IsTrue(company.IsValid);
        }

        [TestMethod]
        public void Shold_return_false_when_invalid_supplier_with_cnpj()
        {
            var company = new Supplier(this._cnpjInvalid, "any_trade_name", this._emailValid, this._cepValid);
            Assert.IsTrue(company.IsValid);
        }

        [TestMethod]
        public void Shold_return_true_when_valid_supplier_with_cpf()
        {
            var company = new Supplier(this._cpfValid, "any_trade_name", this._emailValid, this._cepValid);
            Assert.IsTrue(company.IsValid);
        }


    }
}
