
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
        private readonly Address _addressValid;
        private readonly Address _addressInvalid;

        public CompanyTest()
        {
            _cpfValid = new("05841241710");
            _cnpjValid = new("15105766000108");
            _cnpjInvalid = new("any_cnpj");
            _addressValid = new("any_street", "any_number", "", "any_neighborhood", "any_city", "RJ", "BR", "44780-000");
            _addressInvalid = new("", "", "", "", "", "", "", "");
        }

        [TestMethod]
        public void Shold_return_true_when_valid_company_with_cnpj()
        {
            var company = new Company(_cnpjValid, "any_trade_name", _addressValid);
            Assert.IsTrue(company.IsValid);
        }

        [TestMethod]
        public void Shold_return_false_when_invalid_company_with_cpf()
        {
            var company = new Company(_cpfValid, "any_trade_name", _addressValid);
            Assert.IsFalse(company.IsValid);
        }

        [TestMethod]
        public void Shold_return_false_when_invalid_company_with_cnpj()
        {
            var company = new Company(_cnpjInvalid, "any_trade_name", _addressValid);
            Assert.IsFalse(company.IsValid);
        }

        [TestMethod]
        public void Shold_return_false_when_invalid_company_with_address()
        {
            var company = new Company(_cnpjValid, "any_trade_name", _addressInvalid);
            Assert.IsFalse(company.IsValid);
        }
    }
}