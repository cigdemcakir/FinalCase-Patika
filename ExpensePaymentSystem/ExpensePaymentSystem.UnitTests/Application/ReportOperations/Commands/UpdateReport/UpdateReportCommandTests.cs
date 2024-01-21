using AutoMapper;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Business.Commands.ReportCommands.CreateCommand;
using ExpensePaymentSystem.Business.Commands.ReportCommands.UpdateReport;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;

namespace ExpensePaymentSystem.UnitTests.Application.ReportOperations.Commands.UpdateReport
{
    public class CreateExpenseCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly ExpensePaymentSystemDbContext _dbContext;
        private readonly IMapper _mapper;

        public CreateExpenseCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
        [Fact]
        public async Task GivenValidInputs_ReportShouldBeUpdated()
        {
            // Arrange
            var testReport = new Report
            {
                UserId = 1, 
                StartDate = new DateTime(2024, 1, 1), 
                EndDate = new DateTime(2024, 1, 31), 
                TotalPayment = 250, 
                TotalExpense = 300

            };
            await _dbContext.Reports.AddAsync(testReport);
            await _dbContext.SaveChangesAsync();

            var updateRequest = new ReportRequest()
            {
                StartDate = new DateTime(2024, 1, 2), 
                EndDate = new DateTime(2024, 1, 30), 
            };

            var command = new UpdateReportCommand(testReport.ReportId, updateRequest);
            var handler = new UpdateReportCommandHandler(_dbContext, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Response.Should().NotBeNull();

            var updatedExpense = await _dbContext.Reports.FindAsync(testReport.ReportId);
            updatedExpense.Should().NotBeNull();
            updatedExpense.StartDate.Should().Be(updateRequest.StartDate);
            updatedExpense.EndDate.Should().Be(updateRequest.EndDate);
        }
    }
}
