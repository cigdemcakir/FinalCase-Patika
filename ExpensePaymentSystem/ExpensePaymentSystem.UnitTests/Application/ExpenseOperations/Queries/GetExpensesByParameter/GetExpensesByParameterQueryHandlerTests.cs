using AutoMapper;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetExpensesByParameter;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.UnitTests.Application.ExpenseOperations.Queries.GetExpensesByParameter;

public class GetExpensesByParameterQueryHandlerTests : IClassFixture<CommonTestFixture>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetExpensesByParameterQueryHandlerTests(CommonTestFixture fixture)
    {
        _dbContext = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task Handle_WhenQueriedWithParameters_ShouldReturnFilteredExpenses()
    {
        // Arrange
        var handler = new GetExpensesByParameterQueryHandler(_dbContext, _mapper);
        var query = new GetExpensesByParameterQuery(userId: 1, status: ExpenseStatus.Pending);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Response.Should().NotBeNull();
        result.Response.Should().OnlyContain(e => e.UserId == 1 && e.Status == ExpenseStatus.Pending);
    }
}
