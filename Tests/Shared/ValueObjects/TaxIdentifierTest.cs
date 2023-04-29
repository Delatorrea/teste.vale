using Shared.ValueObjects;

namespace Tests.Shared.ValueObjects
{
    [TestClass]
    public class TaxIdentifierTest
    {
        [TestMethod]
        public void Should_return_a_valid_cpf()
        {
            TaxIdentifier cpf = new TaxIdentifier("665.776.146-52");
            foreach (var item in cpf.Notifications)
            {
                Console.WriteLine($"{item.Key} - {item.Message}");
            }
            Assert.IsTrue(cpf.IsValid);
        }
    }
}