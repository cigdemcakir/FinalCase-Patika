using AutoMapper;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.CreateExpense;
using ExpensePaymentSystem.Business.Commands.ReportCommands.DeleteReport;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Schema;
using ExpensePaymentSystem.UnitTests.TestsSetup;
using FluentAssertions;

namespace ExpensePaymentSystem.UnitTests.Application.ReportOperations.Commands.DeleteReport
{
    public class DeleteReportCommandTests: IClassFixture<CommonTestFixture>
    {
        private readonly ExpensePaymentSystemDbContext _dbContext;
        private readonly IMapper _mapper;

        public DeleteReportCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }
        
        [Fact]
        public async Task Handle_WithInvalidId_ShouldReturnRecordNotFound()
        {
            // Arrange
            var invalidReportId = 999; 
            var command = new DeleteReportCommand(invalidReportId);
            var handler = new DeleteReportCommandHandler(_dbContext, _mapper);

            // Act
            var response = await handler.Handle(command, CancellationToken.None);

            // Assert
            response.Should().NotBeNull();
            response.Message.Should().Be("Record not found");
        }
    }
}
