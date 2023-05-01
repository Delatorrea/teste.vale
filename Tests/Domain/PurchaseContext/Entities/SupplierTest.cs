using Domain.PurchaseContext.Entities;
using Shared.ValueObjects;

namespace Tests.Domain.PurchaseContext.Entities
{
    [TestClass]
    public class SupplierTest
    {
        private readonly TaxIdentifier _cpfValid;
        private readonly TaxIdentifier _cnpjValid;
        private readonly Address _addressValid;
        private readonly Email _emailValid;
        private readonly Supplier _supplierLegalEntityValid;
        private readonly Supplier _supplierLegalEntityInvalid;
        private readonly Supplier _supplierAnIndividualValid;
        private readonly Supplier _supplierAnIndividualInvalid;

        public SupplierTest()
        {
            _cpfValid = new("05841241710");
            _cnpjValid = new("15105766000108");
            _addressValid = new("any_street", "any_number", "", "any_neighborhood", "any_city", "RJ", "BR", "44780-000");
            _emailValid = new("any@any.com");
            _supplierLegalEntityValid = new(_cnpjValid, "any_trade_name", _addressValid, _emailValid);
            _supplierLegalEntityInvalid = new(_cnpjValid, "", _addressValid, _emailValid);
            _supplierAnIndividualValid = new(_cpfValid, "any_trade_name", _addressValid, _emailValid, new DateTime(1987, 03,01), "2020288822 DIC/RJ");
            _supplierAnIndividualInvalid = new(_cpfValid, "any_trade_name", _addressValid, _emailValid);
        }

        [TestMethod]
        public void Shold_return_true_when_valid_supplier_with_cnpj()
        {
            Assert.IsTrue(_supplierLegalEntityValid.IsValid);
        }

        [TestMethod]
        public void Shold_return_false_when_invalid_supplier_with_cnpj()
        {
            Assert.IsFalse(_supplierLegalEntityInvalid.IsValid);
        }

        [TestMethod]
        public void Shold_return_true_when_valid_supplier_with_cpf()
        {
            Assert.IsTrue(_supplierAnIndividualValid.IsValid);
        }

        [TestMethod]
        public void Shold_return_false_when_invalid_supplier_with_cpf()
        {
            Assert.IsFalse(_supplierAnIndividualInvalid.IsValid);
        }

        [TestMethod]
        public void Shold_return_true_when_individual_supplier_is_adult_in_parana()
        {
            Address address = new("any_street", "any_number", "", "any_neighborhood", "any_city", "PR", "BR", "44780-000");
            Supplier actual = new(_cpfValid, "any_trade_name", address, _emailValid, new DateTime(1987, 03, 01), "2020288822 DIC/RJ");
            Assert.IsTrue(actual.IsValid);
        }

        [TestMethod]
        public void Shold_return_false_when_individual_supplier_is_not_adult_in_parana()
        {
            Address address = new("any_street", "any_number", "", "any_neighborhood", "any_city", "PR", "BR", "44780-000");
            Supplier actual = new(_cpfValid, "any_trade_name", address, _emailValid, new DateTime(2015, 01, 01), "2020288822 DIC/RJ");
            Assert.IsFalse(actual.IsValid);
        }


    }
}
