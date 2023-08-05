using AutoFixture;
using AutoFixture.Xunit2;
using Moq;

namespace EvaFloraStoreTests
{
    public class HomeControllerTests
    {
        private readonly Mock<IEvaStoreRepository> _mockRepository;
        private readonly HomeController _controller;
        private readonly Fixture _fixture;

        public HomeControllerTests()
        {
            _mockRepository = new Mock<IEvaStoreRepository>();
            _controller = new HomeController(_mockRepository.Object);
            _fixture = new Fixture();
        }

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
    }
}