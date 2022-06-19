using FluentAssertions;
using Moq;
using Relex.Interview.Api.Test.MockData;
using Relex.Interview.Data.Contracts;
using Relex.Interview.Entities;

namespace Relex.Interview.Api.Test.Services
{
    public class TestProductRepository
    {

        [Fact]
        public async Task GetAll_ShouldReturnListOfProductDto()
        {
            /// Arrange
            var productRepository = new Mock<IRepository<Product>>();
            productRepository.Setup(_ => _.GetAllAsync(CancellationToken.None)).ReturnsAsync(ProductMockData.GetAll);

            /// Act
            var result = await productRepository.Object.GetAllAsync(CancellationToken.None);


            /// Assert
            result.Should().BeOfType<List<Product>>();
            result.Should().HaveCount(3);
        }
    }
}
