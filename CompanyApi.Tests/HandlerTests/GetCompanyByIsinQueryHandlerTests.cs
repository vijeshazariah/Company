using Company.Interface;
using Company.Queries.GetCompanyByIsin;
using Moq;
using companyModel = Company.Model.Company;

namespace CompanyApi.Tests.HandlerTests
{
    public class GetCompanyByIsinQueryHandlerTests
    {
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;
        private readonly GetCompanyByIsinQueryHandler _handler;

        public GetCompanyByIsinQueryHandlerTests()
        {
            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _handler = new GetCompanyByIsinQueryHandler(_companyRepositoryMock.Object);
        }
        [Fact]
        public async Task Handle_ExistingIsin_ReturnsCompany()
        {
            // Arrange
            var isin = "US0378331005";
            var expectedCompany = new companyModel
            {
                Id = 1,
                Name = "Apple Inc.",
                Ticker = "AAPL",
                Exchange = "NASDAQ",
                Isin = isin,
                WebsiteUrl = "http://www.apple.com"
            };

            _companyRepositoryMock.Setup(repo => repo.GetByIsin(isin))
                .ReturnsAsync(expectedCompany);

            var query = new GetCompanyByIsinQuery{ InIs = expectedCompany.Isin};

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(expectedCompany.Id, result.Id);
            Assert.Equal(expectedCompany.Name, result.Name);
            Assert.Equal(expectedCompany.Ticker, result.Ticker);
            Assert.Equal(expectedCompany.Isin, result.Isin);
        }

        [Fact]
        public async Task Handle_NonExistingIsin_ReturnsNull()
        {
            // Arrange
            var isin = "US1234567890";
            _companyRepositoryMock.Setup(repo => repo.GetByIsin(isin))
                .ReturnsAsync((companyModel)null);

            var query = new GetCompanyByIsinQuery { InIs = isin };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }

        [Fact]
        public async Task Handle_InvalidIsin_ReturnsNull()
        {
            // Arrange
            var invalidIsin = "1234567890"; // Invalid ISIN (first two characters should be letters)
            _companyRepositoryMock.Setup(repo => repo.GetByIsin(invalidIsin))
                .ReturnsAsync((companyModel)null);

            var query = new GetCompanyByIsinQuery { InIs = invalidIsin }; ;

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.Null(result);
        }
    }
}
