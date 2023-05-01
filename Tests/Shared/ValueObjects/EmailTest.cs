using Shared.ValueObjects;

namespace Tests.Shared.ValueObjects
{
    [TestClass]
    public class EmailTest
    {
        [TestMethod]
        public void Should_return_true_when_valid_email()
        {
            Email email = new("any@any.com");
            Assert.IsTrue(email.IsValid);
        }

        [TestMethod]
        public void Should_return_false_when_invalid_email()
        {
            Email email = new("any_value");
            Assert.IsFalse(email.IsValid);
        }

        [TestMethod]
        public void Should_return_false_when_blank()
        {
            Email email = new("");
            Assert.IsFalse(email.IsValid);
        }
    }
}