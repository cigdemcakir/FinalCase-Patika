using AutoMapper;
using ExpensePaymentSystem.Business.Commands.UserCommands.CreateUser;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.UnitTests.Application.UserOperations.Commands.CreateUser
{
    public class CreateUserCommandTests:IClassFixture<CommonTestFixture>
    {
        private readonly ExpensePaymentSystemDbContext _dbContext;
        private readonly IMapper _mapper;
        
        public CreateUserCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
        [Fact]
        public async Task Handle_ValidRequest_ShouldCreateUser()
        {
            // Arrange
            var request = new UserRequest()
            {
                UserName = "testuser",
                Password = "testpassword",
                Email = "test@example.com", 
                FirstName = "John",         
                LastName = "Doe",         
                PhoneNumber = "1234567890" 
            };

            var command = new CreateUserCommand(request);
            var handler = new CreateUserCommandHandler(_dbContext, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Response.Should().NotBeNull();

            var user = await _dbContext.Users.FirstOrDefaultAsync(u => u.UserName == request.UserName);

            user.Should().NotBeNull();
        }
        
    }
}
