using AutoMapper;
using ExpensePaymentSystem.Business.Queries.ReportQueries.GetReportsByParameter;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;

namespace ExpensePaymentSystem.UnitTests.Application.UserOperations.Queries.GetUsersByParameter;

public class GetUsersByParameterQueryHandlerTests : IClassFixture<CommonTestFixture>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetUsersByParameterQueryHandlerTests(CommonTestFixture fixture)
    {
        _dbContext = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task Handle_WhenQueriedWithParameters_ShouldReturnFilteredReports()
    {
        // Arrange
        var handler = new GetReportsByParameterQueryHandler(_dbContext, _mapper);
        var query = new GetReportsByParameterQuery(new DateTime(2024, 01, 01), new DateTime(2024, 02, 01));

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Response.Should().NotBeNull();
        result.Response.Should().OnlyContain(e => e.StartDate == new DateTime(2024, 01, 01) && e.EndDate ==  new DateTime(2024, 02, 01));
    }
}
