
using Domain.PurchaseContext.Entities;
using Shared.ValueObjects;

namespace Tests.Domain.PurchaseContext.Entities
{
    [TestClass]
    public class CompanyTest
    {
        private readonly TaxIdentifier _cpfValid;
        private readonly TaxIdentifier _cnpjValid;
        private readonly TaxIdentifier _cnpjInvalid;
        private readonly ZipCode _cepValid;

        public CompanyTest()
        {
            this._cpfValid = new("05841241710");
            this._cnpjValid = new("15105766000108");
            this._cnpjInvalid = new("any_cnpj");
            this._cepValid = new("44780-000");
        }

        [TestMethod]
        public void Shold_return_true_when_valid_company_with_cnpj()
        {
            var company = new Company(this._cnpjValid, "any_trade_name", this._cepValid);
            Assert.IsTrue(company.IsValid);
        }

        [TestMethod]
        public void Shold_return_false_when_invalid_company_with_cpf()
        {
            var company = new Company(this._cpfValid, "any_trade_name", this._cepValid);
            Assert.IsFalse(company.IsValid);
        }

        [TestMethod]
        public void Shold_return_false_when_invalid_company_with_cnpj()
        {
            var company = new Company(this._cnpjInvalid, "any_trade_name", this._cepValid);
            Assert.IsFalse(company.IsValid);
        }
    }
}