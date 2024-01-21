using AutoMapper;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetPastExpenseClaims;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.UnitTests.Application.ExpenseOperations.Queries.GetPastExpenseClaims;

public class GetPastExpenseClaimsQueryHandlerTests : IClassFixture<CommonTestFixture>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPastExpenseClaimsQueryHandlerTests(CommonTestFixture fixture)
    {
        _dbContext = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task Handle_ShouldReturnApprovedOrRejectedExpenses()
    {
        // Arrange
        var handler = new GetPastExpenseClaimsQueryHandler(_dbContext, _mapper);
        var query = new GetPastExpenseClaimsQuery();

        // Act
        var result = await handler.Handle(query, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Response.Should().NotBeNull();
        result.Response.Should().OnlyContain(e => e.Status == ExpenseStatus.Approved || e.Status == ExpenseStatus.Rejected);
    }
}
