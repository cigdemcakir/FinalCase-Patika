using AutoMapper;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetActiveExpenseClaims;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;

namespace ExpensePaymentSystem.UnitTests.Application.ExpenseOperations.Queries.GetActiveExpenseClaims;

public class GetActiveExpenseClaimsQueryHandlerTests : IClassFixture<CommonTestFixture>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetActiveExpenseClaimsQueryHandlerTests(CommonTestFixture fixture)
    {
        _dbContext = fixture.Context;
        _mapper = fixture.Mapper;
    }

    [Fact]
    public async Task Handle_ShouldReturnOnlyPendingExpenses()
    {
        // Arrange
        var handler = new GetActiveExpenseClaimsQueryHandler(_dbContext, _mapper);
        var query = new GetActiveExpenseClaimsQuery(); // Assuming this is correctly defined

        // Act
        var response = await handler.Handle(query, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Response.Should().AllSatisfy(expense => expense.Status.Should().Be(ExpenseStatus.Pending));
    }
}
