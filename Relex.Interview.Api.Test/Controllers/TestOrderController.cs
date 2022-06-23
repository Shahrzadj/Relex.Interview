using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Moq;
using Relex.Interview.Api.Controllers;
using Relex.Interview.Api.Dtos.Order;
using Relex.Interview.Api.Mappings;
using Relex.Interview.Api.Test.MockData;
using Relex.Interview.Data;
using Relex.Interview.Data.Contracts;
using Relex.Interview.Data.Repositories;
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
        private readonly ApplicationDbContext _context;

        public TestOrderController()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new OrderProfile());
            });
            _mapper = mockMapper.CreateMapper();

            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

            _context = new ApplicationDbContext(options);

            _context.Database.EnsureCreated();
        }

        [Fact]
        public async Task Create_WhenIsMaxSelected_ShouldSelectMaxBatchSize()
        {
            /// Arrange
            var productId = 1;
            var orderRepository = new Mock<IRepository<Order>>();
            var batchRepository = new Mock<IRepository<Batch>>();
            var productRepository = new Mock<IRepository<Product>>();
            var productBatchRepository = new Mock<IProductBatchRepository>();
            productBatchRepository.Setup(_ => _.GetBatchesByProductId(productId)).Returns(ProductBatchMockData.GetBatchesByProductId);
            var newOrder = new CreateOrderDto
            {
                ProductId = productId,
                NumberOfBatches = 10,
                IsBatchMaxSize = true,
            };
            var sut = new OrdersController(orderRepository.Object,batchRepository.Object, productRepository.Object, productBatchRepository.Object, _mapper);

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
            var batchRepository = new Mock<IRepository<Batch>>();
            var productRepository = new Mock<IRepository<Product>>();
            var productBatchRepository = new Mock<IProductBatchRepository>();
            productBatchRepository.Setup(_ => _.GetBatchesByProductId(productId)).Returns(ProductBatchMockData.GetBatchesByProductId);
            var newOrder = new CreateOrderDto
            {
                ProductId = productId,
                NumberOfBatches = 10,
                IsBatchMaxSize = false,
            };
            var sut = new OrdersController(orderRepository.Object, batchRepository.Object, productRepository.Object, productBatchRepository.Object, _mapper);

            /// Act
            var result = await sut.Create(newOrder, CancellationToken.None);


            /// Assert
            Assert.Equal(result.BatchId, 1);
        }

        [Fact]
        public async Task Create_WhenBatchIsEmpty_ShouldGenerateBatch()
        {
            /// Arrange
            var productId = 1;
            _context.AddRange(ProductBatchMockData.GetAll_Empty());
            _context.AddRange(ProductMockData.GetAll());
            _context.SaveChanges();

            var orderRepository = new Repository<Order>(_context);
            var batchRepository = new Repository<Batch>(_context);
            var productRepository = new Repository<Product>(_context);
            var productBatchRepository = new ProductBatchRepository(_context);
                
            var newOrder = new CreateOrderDto
            {
                ProductId = productId,
                NumberOfBatches = 10,
                IsBatchMaxSize = false,
            };
            var sut = new OrdersController(orderRepository, batchRepository, productRepository, productBatchRepository, _mapper);

            /// Act
            var result = await sut.Create(newOrder, CancellationToken.None);


            /// Assert
            Assert.Equal(result.Batch.Code, "B_GENERATED_1");
            Assert.Equal(result.Batch.Size, 1);
        }
    }
}
