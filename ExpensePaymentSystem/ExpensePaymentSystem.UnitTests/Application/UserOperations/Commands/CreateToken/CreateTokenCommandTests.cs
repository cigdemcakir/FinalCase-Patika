using AutoMapper;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Base.Token;
using ExpensePaymentSystem.Business.Commands.ReportCommands.CreateCommand;
using ExpensePaymentSystem.Business.Commands.TokenCommands.CreateToken;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Moq;

namespace ExpensePaymentSystem.UnitTests.Application.UserOperations.Commands.CreateToken
{
    public class CreateTokenCommandTests
    {
        [Fact]
        public async Task Handle_WithValidModel_ShouldReturnTokenResponse()
        {
            // Arrange
            var dbContextOptions = new DbContextOptionsBuilder<ExpensePaymentSystemDbContext>()
                .UseInMemoryDatabase(databaseName: "InMemoryDatabase")
                .Options;
            
            var dbContext = new ExpensePaymentSystemDbContext(dbContextOptions);

            var optionsMock = new Mock<IOptionsMonitor<JwtConfig>>();
            
            var jwtConfig = new JwtConfig
            {
                Issuer = "www.ExpensePaymentSystem.com",
                Audience = "www.ExpensePaymentSystem.com",
                Secret = "This is my private secret key that I use for authentication in the expense payment system",
                AccessTokenExpiration = 60 
            };
            optionsMock.Setup(o => o.CurrentValue).Returns(jwtConfig);

            var mediatorMock = new Mock<IMediator>();

            var command = new CreateTokenCommand(new TokenRequest
            {
                UserName = "admin1",
                Password = "password1" 
            });

            var handler = new CreateTokenCommandHandler(dbContext, optionsMock.Object);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
        }
    }
}
