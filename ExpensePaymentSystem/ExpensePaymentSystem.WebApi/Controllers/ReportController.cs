using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Commands.ReportCommands.CreateCommand;
using ExpensePaymentSystem.Business.Commands.ReportCommands.DeleteReport;
using ExpensePaymentSystem.Business.Commands.ReportCommands.UpdateReport;
using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Business.Queries.ReportQueries.GetAllReports;
using ExpensePaymentSystem.Business.Queries.ReportQueries.GetReportById;
using ExpensePaymentSystem.Business.Queries.ReportQueries.GetReportsByParameter;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ExpensePaymentSystem.WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ReportController : ControllerBase
{
    private readonly IMediator _mediator;
    private readonly IExcelService _excelService;

    public ReportController(IMediator mediator, IExcelService excelService)
    {
        _mediator = mediator;
        _excelService = excelService;
    }

    [HttpGet]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ReportResponse>>> Get()
    {
        var operation = new GetAllReportsQuery();
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpGet("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<ReportResponse>> GetById(int id)
    {
        var operation = new GetReportByIdQuery(id);
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpGet("ByParameters")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<List<ReportResponse>>> GetByParameter(
        [FromQuery] DateTime? startDate,
        [FromQuery] DateTime? endDate)
    {
        var operation = new GetReportsByParameterQuery(startDate, endDate);
        var result = await _mediator.Send(operation);
        return result;
    }
    
    [HttpGet("download-report")]
    [Authorize(Roles = "Admin")]
    public async Task<IActionResult> DownloadReport()
    {
        var operation = new GetAllReportsQuery();
        var result = await _mediator.Send(operation);
        var excelData = _excelService.CreateExcelReport(result.Response);

        return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "report.xlsx");
    }

    [HttpPost]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<ReportResponse>> Post([FromBody] ReportRequest Report)
    {
        var operation = new CreateReportCommand(Report);
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpPut("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse<ReportResponse>> Put(int id, [FromBody] ReportRequest Report)
    {
        var operation = new UpdateReportCommand(id, Report);
        var result = await _mediator.Send(operation);
        return result;
    }

    [HttpDelete("{id}")]
    [Authorize(Roles = "Admin")]
    public async Task<ApiResponse> Delete(int id)
    {
        var operation = new DeleteReportCommand(id);
        var result = await _mediator.Send(operation);
        return result;
    }
}
