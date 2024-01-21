using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetExpenseById;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.UnitTests.Application.ExpenseOperations.Queries.GetExpenseById;

public class GetExpenseByIdQueryHandlerTests : IClassFixture<CommonTestFixture>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetExpenseByIdQueryHandlerTests(CommonTestFixture fixture)
    {
        _dbContext = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task Handle_WhenExpenseExists_ShouldReturnExpense()
    {
        // Arrange
        var handler = new GetExpenseByIdQueryHandler(_dbContext, _mapper);
        var query = new GetExpenseByIdQuery(1);
        
        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Response.Should().NotBeNull();
        result.Response.ExpenseId.Should().Be(1);
    }
}
