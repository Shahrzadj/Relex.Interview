using AutoMapper;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Relex.Interview.Api.Controllers;
using Relex.Interview.Api.Dtos.Product;
using Relex.Interview.Api.Mappings;
using Relex.Interview.Api.Test.MockData;
using Relex.Interview.Data.Contracts;
using Relex.Interview.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Relex.Interview.Api.Test.Controllers
{
    public class TestProductsController
    {
        private readonly IMapper _mapper;

        public TestProductsController()
        {
            var mockMapper = new MapperConfiguration(cfg =>
            {
                cfg.AddProfile(new ProductProfile());
            });
            _mapper = mockMapper.CreateMapper();
        }

        [Fact]
        public async Task GetAll_ShouldReturnListOfProductDto()
        {
            /// Arrange
            var productRepository = new Mock<IRepository<Product>>();
            productRepository.Setup(_ => _.GetAllAsync(CancellationToken.None)).ReturnsAsync(ProductMockData.GetAll);
            var sut = new ProductsController(productRepository.Object, _mapper);

            /// Act
            var result = await sut.Get(CancellationToken.None);


            /// Assert
            Assert.IsType<List<ProductDto>>(result);
        }
    }
}
