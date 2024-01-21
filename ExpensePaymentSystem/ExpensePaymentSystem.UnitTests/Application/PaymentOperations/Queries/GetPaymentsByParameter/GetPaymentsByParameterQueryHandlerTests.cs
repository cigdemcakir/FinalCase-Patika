using AutoMapper;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetExpensesByParameter;
using ExpensePaymentSystem.Business.Queries.PaymentQueries.GetPaymentsByParameter;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.UnitTests.Application.PaymentOperations.Queries.GetPaymentsByParameter;

public class GetPaymentsByParameterQueryHandlerTests : IClassFixture<CommonTestFixture>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPaymentsByParameterQueryHandlerTests(CommonTestFixture fixture)
    {
        _dbContext = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task Handle_WhenQueriedWithParameters_ShouldReturnFilteredExpenses()
    {
        // Arrange
        var handler = new GetPaymentsByParameterQueryHandler(_dbContext, _mapper);
        var query = new GetPaymentsByParameterQuery(null, null, paymentMethod:PaymentMethod.CreditCard);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Response.Should().NotBeNull();
        result.Response.Should().OnlyContain(e => e.PaymentMethod == PaymentMethod.CreditCard);
    }
}