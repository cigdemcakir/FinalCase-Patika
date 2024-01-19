using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Business.Interfaces;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using Hangfire;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Commands;

public class ReportCommandHandler/*: 
    IRequestHandler<CreateReportCommand, ApiResponse<ReportResponse>>,
    IRequestHandler<UpdateReportCommand,ApiResponse>,
    IRequestHandler<DeleteReportCommand,ApiResponse>*/

{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public ReportCommandHandler(ExpensePaymentSystemDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
/*
    public async Task<ApiResponse<ReportResponse>> Handle(CreateReportCommand request, CancellationToken cancellationToken)
    {
        var checkIdentity = await dbContext.Set<Report>().Where(x => x.ReportId == request.Model.UserId)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkIdentity != null)
        {
            return new ApiResponse<ReportResponse>($"{request.Model.UserId} is used by another Report.");
        }
        
        var entity = mapper.Map<ReportRequest, Report>(request.Model);
        //entity.ReportNumber = new Random().Next(1000000, 9999999);

        // if (entity.Accounts.Any())
        // {
        //     entity.Accounts.ForEach(x =>
        //     {
        //         x.AccountNumber = new Random().Next(10000000, 99999999);
        //     });
        // }
        
        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);


        // if (entity.Contacts.Any())
        // {
        //     var email = entity.Contacts.FirstOrDefault(x => x.IsDefault && x.ContactType == "Email");
        //     if (email != null)
        //     {
        //         BackgroundJob.Schedule(() => notificationService.SendEmail("Welcome " + entity.FirstName ,email.Information,"Welcome on board!"), TimeSpan.FromSeconds(50));
        //     }
        // }
      

        var mapped = mapper.Map<Report, ReportResponse>(entityResult.Entity);
        return new ApiResponse<ReportResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdateReportCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Report>().Where(x => x.ReportId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        fromdb.StartDate= request.Model.StartDate;
        fromdb.EndDate = request.Model.EndDate;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeleteReportCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Report>().Where(x => x.ReportId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        dbContext.Reports.Remove(fromdb);

        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }*/
}