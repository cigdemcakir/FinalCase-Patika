using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetAllExpenses;
using ExpensePaymentSystem.Business.Queries.ReportQueries.GetAllReports;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.UnitTests.Application.ReportOperations.Queries.GetAllReports;

public class GetAllExpensesQueryHandlerTests : IClassFixture<CommonTestFixture>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllExpensesQueryHandlerTests(CommonTestFixture fixture)
    {
        _dbContext = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task Handle_ShouldReturnAllReports()
    {
        // Arrange
        var handler = new GetAllReportsQueryHandler(_dbContext, _mapper);
        var query = new GetAllReportsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Response.Should().NotBeNull();
    }
}