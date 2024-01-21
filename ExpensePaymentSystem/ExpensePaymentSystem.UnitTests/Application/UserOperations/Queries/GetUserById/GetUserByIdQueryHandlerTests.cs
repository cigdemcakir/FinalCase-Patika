using AutoMapper;
using ExpensePaymentSystem.Business.Queries.ReportQueries.GetReportById;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;

namespace ExpensePaymentSystem.UnitTests.Application.UserOperations.Queries.GetUserById;

public class GetUserByIdQueryHandlerTests : IClassFixture<CommonTestFixture>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandlerTests(CommonTestFixture fixture)
    {
        _dbContext = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task Handle_WhenReportExists_ShouldReturnReport()
    {
        // Arrange
        var handler = new GetReportByIdQueryHandler(_dbContext, _mapper);
        var query = new GetReportByIdQuery(1);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Response.Should().NotBeNull();
        result.Response.ReportId.Should().Be(1);
    }
}
