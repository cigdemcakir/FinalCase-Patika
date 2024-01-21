using AutoMapper;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.CreateExpense;
using ExpensePaymentSystem.Business.Commands.ReportCommands.CreateCommand;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.UnitTests.Application.ReportOperations.Commands.CreateReport
{
    public class CreateReportCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly ExpensePaymentSystemDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateReportCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
        [Fact]
        public async Task Handle_GivenValidReportRequest_ShouldCreateReportAndReturnResponse()
        {
            // Arrange
            var startDate = DateTime.Now.AddDays(-10);
            var endDate = DateTime.Now;
            var userId = 1;
            
            var model = new ReportRequest
            {
                StartDate = startDate,
                EndDate = endDate,
                UserId = userId
            };

            var command = new CreateReportCommand(model);
            var handler = new CreateReportCommandHandler(_dbContext, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Success.Should().BeTrue();
            response.Response.Should().NotBeNull();

            var reportFromDb = await _dbContext.Reports.FirstOrDefaultAsync(r => r.UserId == userId && r.StartDate == startDate && r.EndDate == endDate);
            reportFromDb.Should().NotBeNull();

        }
    }
}
