using AutoFixture;
using AutoFixture.Xunit2;
using Microsoft.EntityFrameworkCore.Migrations;
using Moq;
using System;
using System.Drawing.Text;
using System.Linq;
using System.Security.Cryptography.X509Certificates;

namespace EvaFloraStoreTests
{

    public class HomeControllerTests
    {
        #region injecting
        private readonly Mock<IEvaStoreRepository> _mockRepository;
        private readonly HomeController _controller;
        private readonly Fixture _fixture;

        public HomeControllerTests()
        {
            _mockRepository = new Mock<IEvaStoreRepository>();
            _controller = new HomeController(_mockRepository.Object);
            _fixture = new Fixture();
        }
        #endregion

        [Fact]
        public async Task CanUseRepository()
        {
            _fixture.Customize<Product>(c => c
                    .With(p => p.Price, _fixture.Create<decimal>() + 1)
                    .With(p => p.IsVisible, true));
            var products = _fixture.CreateMany<Product>(2).OrderBy(p =>  p.Name).ToList();
            _mockRepository.Setup(m => m.GetProductsAsync()).ReturnsAsync(products.AsQueryable());
           
            var result = await _controller.Index(null);
           
            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ProductListViewModel>(viewResult.Model);

            var prodArray = model.Products.OrderBy(p => p.Name).ToArray();

            Assert.Equal(2, prodArray.Length);
            Assert.Equal(products[0].Name, prodArray[0].Name);
            Assert.Equal(products[1].Name, prodArray[1].Name);
        }

        [Fact]
        public async void CanPaginate()
        {
            _fixture.Customize<Product>(c => c
                    .With(p => p.Price, _fixture.Create<decimal>() + 1)
                    .With(p => p.IsVisible, true));
            var products = _fixture.CreateMany<Product>(16).OrderBy(p => p.Name).ToList();
            _mockRepository.Setup(m => m.GetProductsAsync()).ReturnsAsync(products.AsQueryable());
            _controller.PageSize = 6;

            var result = await _controller.Index(null, 3);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ProductListViewModel>(viewResult.Model);

            var prodArray = model.Products.OrderBy(p => p.Name).ToArray();

            Assert.True(prodArray.Length == 4);
            Assert.Equal(products[12].Name, prodArray[0].Name);
            Assert.Equal(products[13].Name, prodArray[1].Name);
            Assert.Equal(products[14].Name, prodArray[2].Name);
            Assert.Equal(products[15].Name, prodArray[3].Name);
        }

        [Fact]
        public async Task CanFilterProducts()
        {
            var possibleCategoryNames = new List<string> { "Cat1", "Cat2", "Cat3" };
            Random random = new Random();
            
            _fixture.Customize<Product>(c => c
                    .With(p => p.Price, _fixture.Create<decimal>() + 1)
                    .With(p => p.IsVisible, true)
                    .Without(p => p.Category)
                    .Do(p =>
                    {
                        var categoryId = new Guid();
                        var categoryName = possibleCategoryNames[random.Next(possibleCategoryNames.Count)];
                        p.Category = new Category {Id = categoryId, Name = categoryName };
                    }));

            var products = _fixture.CreateMany<Product>(16).OrderBy(p => p.Name).ToList();
            _mockRepository.Setup(m => m.GetProductsAsync()).ReturnsAsync(products.AsQueryable());
            _controller.PageSize = 16;

            var result = await _controller.Index("Cat1", 1);

            var viewResult = Assert.IsType<ViewResult>(result);
            var model = Assert.IsType<ProductListViewModel>(viewResult.Model);
            var prodArray = model.Products.ToArray();

            Assert.True(prodArray.Length == products.Where(p => p.Category.Name == "Cat1").Count());
        }
        
    }
}