using Company.Interface;
using Company.Queries.GetAllCompanies;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using companyModel = Company.Model.Company;

namespace CompanyApi.Tests.HandlerTests
{
    public class GetAllCompaniesQueryHandlerTests
    {
        private readonly Mock<ICompanyRepository> _companyRepositoryMock;
        private readonly GetAllCompaniesQueryHandler _handler;
        public GetAllCompaniesQueryHandlerTests()
    {
        _companyRepositoryMock = new Mock<ICompanyRepository>();
        _handler = new GetAllCompaniesQueryHandler(_companyRepositoryMock.Object);
    }
        [Fact]
        public async Task Handle_ShouldReturnListOfCompanies()
        {
            // Arrange
            var companies = new List<companyModel>
        {
            new companyModel { Id = 1, Name = "Apple", Ticker = "AAPL", Isin = "US0378331005", Exchange = "NASDAQ" },
            new companyModel { Id = 2, Name = "Heineken", Ticker = "HEIA", Isin = "NL0000009165", Exchange = "Euronext Amsterdam" }
        };

            _companyRepositoryMock.Setup(repo => repo.GetAllCompanies())
                .ReturnsAsync(companies);

            var request = new GetAllCompaniesQuery();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().HaveCount(2);
            result[0].Name.Should().Be("Apple");
            result[1].Name.Should().Be("Heineken");
        }

        [Fact]
        public async Task Handle_ShouldReturnEmptyListIfNoCompaniesFound()
        {
            // Arrange
            _companyRepositoryMock.Setup(repo => repo.GetAllCompanies())
                .ReturnsAsync(new List<companyModel>());

            var request = new GetAllCompaniesQuery();

            // Act
            var result = await _handler.Handle(request, CancellationToken.None);

            // Assert
            result.Should().BeEmpty();
        }
    }
}
