using Company.Interface;
using Company.Queries.GetCompanyById;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using companyModel = Company.Model.Company;

namespace CompanyApi.Tests.HandlerTests
{
    public class GetCompanyByIdQueryHandlerTests
    {
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;
        private readonly GetCompanyByIdQueryHandler _handler;
        public GetCompanyByIdQueryHandlerTests()
        {
            _companyRepositoryMock = new Mock<ICompanyRepository>();
            _handler = new GetCompanyByIdQueryHandler(_companyRepositoryMock.Object);
        }
        [Fact]
        public async Task Handle_ShouldReturnCompany_WhenCompanyExists()
        {
            // Arrange
            var company = new companyModel
            {
                Id = 1,
                Name = "Apple Inc.",
                Ticker = "AAPL",
                Exchange = "NASDAQ",
                Isin = "US0378331005",
                WebsiteUrl = "http://www.apple.com"
            };

            _companyRepositoryMock.Setup(repo => repo.GetCompanyByIdAsync(1))
                .ReturnsAsync(company);

            var query = new GetCompanyByIdQuery { Id = 1 };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().NotBeNull();
            result.Id.Should().Be(1);
            result.Name.Should().Be("Apple Inc.");
            result.Ticker.Should().Be("AAPL");
            result.Isin.Should().Be("US0378331005");
        }

        [Fact]
        public async Task Handle_ShouldReturnNull_WhenCompanyDoesNotExist()
        {
            // Arrange
            _companyRepositoryMock.Setup(repo => repo.GetCompanyByIdAsync(1))
                .ReturnsAsync((companyModel)null);

            var query = new GetCompanyByIdQuery { Id = 1 };

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            result.Should().BeNull();
        }

    }
}
