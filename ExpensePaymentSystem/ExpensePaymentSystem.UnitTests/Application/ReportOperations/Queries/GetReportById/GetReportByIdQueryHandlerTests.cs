using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetExpenseById;
using ExpensePaymentSystem.Business.Queries.ReportQueries.GetReportById;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.UnitTests.Application.ReportOperations.Queries.GetReportById;

public class GetReportByIdQueryHandlerTests : IClassFixture<CommonTestFixture>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetReportByIdQueryHandlerTests(CommonTestFixture fixture)
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
