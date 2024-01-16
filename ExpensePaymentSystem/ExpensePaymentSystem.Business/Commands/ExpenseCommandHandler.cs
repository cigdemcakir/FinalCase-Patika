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


public class ExpenseCommandHandler :
    IRequestHandler<CreateExpenseCommand, ApiResponse<ExpenseResponse>>,
    IRequestHandler<UpdateExpenseCommand,ApiResponse>,
    IRequestHandler<DeleteExpenseCommand,ApiResponse>

{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public ExpenseCommandHandler(ExpensePaymentSystemDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var checkIdentity = await dbContext.Set<Expense>().Where(x => x.ExpenseId == request.Model.UserId)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkIdentity != null)
        {
            return new ApiResponse<ExpenseResponse>($"{request.Model.UserId} is used by another Expense.");
        }
        
        var entity = mapper.Map<ExpenseRequest, Expense>(request.Model);
        // entity.ExpenseNumber = new Random().Next(1000000, 9999999);
        //
        // if (entity.Accounts.Any())
        // {
        //     entity.Accounts.ForEach(x =>
        //     {
        //         x.AccountNumber = new Random().Next(10000000, 99999999);
        //     });
        // }
        //
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
      

        var mapped = mapper.Map<Expense, ExpenseResponse>(entityResult.Entity);
        return new ApiResponse<ExpenseResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Expense>().Where(x => x.ExpenseId == request.Id)
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

    public async Task<ApiResponse> Handle(DeleteExpenseCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Expense>().Where(x => x.ExpenseId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }
}