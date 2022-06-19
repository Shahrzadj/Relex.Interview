using AutoMapper;
using Moq;
using Relex.Interview.Api.Controllers;
using Relex.Interview.Api.Dtos.Order;
using Relex.Interview.Api.Mappings;
using Relex.Interview.Api.Test.MockData;
using Relex.Interview.Data.Contracts;
using Relex.Interview.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Relex.Interview.Api.Test.Controllers
{
    public class TestOrderController
    {
        private readonly IMapper _mapper;

        public TestOrderController()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new OrderProfile());
            });
            _mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public async Task Create_WhenIsMaxSelected_ShouldSelectMaxBatchSize()
        {
            /// Arrange
            var productId = 1;
            var orderRepository = new Mock<IRepository<Order>>();
            var productBatchRepository = new Mock<IProductBatchRepository>();
            productBatchRepository.Setup(_ => _.GetBatchesByProductId(productId)).Returns(ProductBatchMockData.GetBatchesByProductId);
            var newOrder = new CreateOrderDto
            {
                ProductId = productId,
                NumberOfBatches = 10,
                IsBatchMaxSize = true,
            };
            var sut = new OrdersController(orderRepository.Object, productBatchRepository.Object, _mapper);

            /// Act
            var result = await sut.Create(newOrder, CancellationToken.None);


            /// Assert
            Assert.Equal(result.BatchId, 3);
        }

        [Fact]
        public async Task Create_WhenIsMaxNotSelected_ShouldSelectMinBatchSize()
        {
            /// Arrange
            var productId = 1;
            var orderRepository = new Mock<IRepository<Order>>();
            var productBatchRepository = new Mock<IProductBatchRepository>();
            productBatchRepository.Setup(_ => _.GetBatchesByProductId(productId)).Returns(ProductBatchMockData.GetBatchesByProductId);
            var newOrder = new CreateOrderDto
            {
                ProductId = productId,
                NumberOfBatches = 10,
                IsBatchMaxSize = false,
            };
            var sut = new OrdersController(orderRepository.Object, productBatchRepository.Object, _mapper);

            /// Act
            var result = await sut.Create(newOrder, CancellationToken.None);


            /// Assert
            Assert.Equal(result.BatchId, 1);
        }
    }
}
