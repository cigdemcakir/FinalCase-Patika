using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Commands.ReportCommands.DeleteReport;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;
using MediatR;

namespace ExpensePaymentSystem.UnitTests.Application.UserOperations.Commands.DeleteUser;

public class DeleteUserCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public DeleteUserCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }
        
    [Fact]
    public async Task Handle_WithInvalidId_ShouldReturnRecordNotFound()
    {
        // Arrange
        var invalidUserId = 999; 
        var command = new DeleteReportCommand(invalidUserId);
        var handler = new DeleteReportCommandHandler(_dbContext, _mapper);

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Message.Should().Be("Record not found");
    }
}