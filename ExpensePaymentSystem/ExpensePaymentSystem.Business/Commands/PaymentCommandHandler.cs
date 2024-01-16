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

public class PaymentCommandHandler:
    IRequestHandler<CreatePaymentCommand, ApiResponse<PaymentResponse>>,
    IRequestHandler<UpdatePaymentCommand,ApiResponse>,
    IRequestHandler<DeletePaymentCommand,ApiResponse>

{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public PaymentCommandHandler(ExpensePaymentSystemDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<PaymentResponse>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {
        var checkIdentity = await dbContext.Set<Payment>().Where(x => x.PaymentId == request.Model.ExpenseId)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkIdentity != null)
        {
            return new ApiResponse<PaymentResponse>($"{request.Model.ExpenseId} is used by another Payment.");
        }
        
        var entity = mapper.Map<PaymentRequest, Payment>(request.Model);
        // entity.PaymentNumber = new Random().Next(1000000, 9999999);
        //
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
      

        var mapped = mapper.Map<Payment, PaymentResponse>(entityResult.Entity);
        return new ApiResponse<PaymentResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Payment>().Where(x => x.PaymentId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        // fromdb.FirstName = request.Model.FirstName;
        // fromdb.LastName = request.Model.LastName;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Payment>().Where(x => x.PaymentId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}