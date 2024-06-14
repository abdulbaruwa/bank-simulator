using Bogus;

namespace BankSimulator.Tests
{
    public class Tests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void Test1()
        {
            var faker = new Faker();
            var domain = faker.Internet.DomainName();
            
            Assert.Pass();
        }
    }
}