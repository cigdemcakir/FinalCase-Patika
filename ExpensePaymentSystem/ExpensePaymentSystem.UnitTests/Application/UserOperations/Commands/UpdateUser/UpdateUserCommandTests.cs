using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Commands.ReportCommands.UpdateReport;
using ExpensePaymentSystem.Business.Commands.UserCommands.UpdateUser;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;
using MediatR;

namespace ExpensePaymentSystem.UnitTests.Application.UserOperations.Commands.UpdateUser;

public class UpdateUserCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdateUserCommandTests(CommonTestFixture testFixture)
    {
        _dbContext = testFixture.Context;
        _mapper = testFixture.Mapper;
    }
        
    [Fact]
    public async Task GivenValidInputs_ReportShouldBeUpdated()
    {
        // Arrange
        var testUser = new User
        {
            UserName = "testuser",
            Password = "testpassword",
            Email = "test@example.com", 
            FirstName = "John",        
            LastName = "Doe",        
            PhoneNumber = "1234567890" 

        };
        await _dbContext.Users.AddAsync(testUser);
        await _dbContext.SaveChangesAsync();

        var updateRequest = new UserRequest()
        {
            FirstName = "Johny",
            LastName =  "Doey"
        };

        var command = new UpdateUserCommand(testUser.UserId, updateRequest);
        var handler = new UpdateUserCommandHandler(_dbContext, _mapper);

        // Act
        var response = await handler.Handle(command, CancellationToken.None);

        // Assert
        response.Should().NotBeNull();
        response.Response.Should().NotBeNull();

        var updatedExpense = await _dbContext.Users.FindAsync(testUser.UserId);
        updatedExpense.Should().NotBeNull();
        updatedExpense.FirstName.Should().Be(updateRequest.FirstName);
        updatedExpense.LastName.Should().Be(updateRequest.LastName);
    }
}