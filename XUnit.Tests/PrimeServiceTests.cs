using Tutorial.Api.Services;
using Xunit;

namespace XUnit.Tests
{
    
    public class PrimeServiceTests
    {
        readonly PrimeService _primeService;
        public PrimeServiceTests() => _primeService = new PrimeService();

        

        [Theory(DisplayName = "Prime number less then 2 return false")]
        [InlineData(-1)]
        [InlineData(0)]
        [InlineData(1)]
        public void IsPrime_ValuesLessThan2_ReturnFalse(int value) => Assert.False(_primeService.IsPrime(value), $"{value} should not be prime");


        [
            Theory(DisplayName = "Is Prime PrimesLessThan10 ReturnTrue"),
            InlineData(2), InlineData(3), InlineData(5), InlineData(7)
        ]
        public void IsPrime_PrimesLessThan10_ReturnTrue(int value) =>
            Assert.True(_primeService.IsPrime(value), $"{value} should be prime");


        [
            Theory(DisplayName = "Is Prime NonPrimesLessThan10 ReturnFalse"),
            InlineData(4), InlineData(6), InlineData(8), InlineData(9)
        ]
        public void IsPrime_NonPrimesLessThan10_ReturnFalse(int value) =>
            Assert.False(_primeService.IsPrime(value), $"{value} should not be prime");

    }
}
