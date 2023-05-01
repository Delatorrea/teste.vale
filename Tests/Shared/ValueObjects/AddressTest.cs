using Domain.PurchaseContext.Entities;
using Shared.ValueObjects;

namespace Tests.Shared.ValueObjects
{
    [TestClass]
    public class AddressTest
    {
        private readonly Address _addressValid;
        private readonly Address _addressInvalid;

        public AddressTest()
        {
            _addressValid = new("any_street", "any_number", "", "any_neighborhood", "any_city", "RJ", "BR", "44780-000");
            _addressInvalid = new("", "", "", "", "", "", "", "");
        }

        [TestMethod]
        public void Should_return_true_when_valid_address()
        {
            Assert.IsTrue(_addressValid.IsValid);
        }

        [TestMethod]
        public void Should_return_false_and_messages_when_invalid_address()
        {
            List<string> actual = new();
            List<string> expected = new() { "Street", "Number", "Neighborhood", "City", "State", "State", "Country", "Country", "PostalCode", "PostalCode" };
            foreach (var item in _addressInvalid.Notifications)
            {
                actual.Add(item.Key);
            }
            Assert.IsFalse(_addressInvalid.IsValid);
            CollectionAssert.AreEquivalent(expected, actual);
        }
    }
}
