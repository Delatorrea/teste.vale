
using Domain.PurchaseContext.Entities;
using Shared.ValueObjects;

namespace Tests.Domain.PurchaseContext.Entities
{
    [TestClass]
    public class CompanyTest
    {
        [TestMethod]
        public void Shold_return_valid_entity()
        {
            var cnpj = new TaxIdentifier("00.427.318/0001-53");
            var cep = new ZipCode("44780-000");
            var company = new Company(cnpj, "any_trade_name", cep);
            Assert.IsTrue(company.IsValid);
        }
    }
}