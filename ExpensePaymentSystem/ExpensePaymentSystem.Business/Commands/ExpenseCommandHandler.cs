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


public class ExpenseCommandHandler 
    //IRequestHandler<CreateExpenseCommand, ApiResponse<ExpenseResponse>>,
    //IRequestHandler<UpdateExpenseCommand,ApiResponse>,
    //IRequestHandler<DeleteExpenseCommand,ApiResponse>

{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public ExpenseCommandHandler(ExpensePaymentSystemDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    /*public async Task<ApiResponse<ExpenseResponse>> Handle(CreateExpenseCommand request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<ExpenseRequest, Expense>(request.Model);
        await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Expense, ExpenseResponse>(entity);
        return new ApiResponse<ExpenseResponse>(mapped);
     
        /*
        var checkIdentity = await dbContext.Set<Expense>().Where(x => x.ExpenseId == request.Model.UserId)
            .FirstOrDefaultAsync(cancellationToken);
        if (checkIdentity != null)
        {
            return new ApiResponse<ExpenseResponse>($"{request.Model.UserId} is used by another Expense.");
        }
        
        var entity = mapper.Map<ExpenseRequest, Expense>(request.Model);
        var entityResult = await dbContext.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Expense, ExpenseResponse>(entityResult.Entity);
        return new ApiResponse<ExpenseResponse>(mapped);#1#
    }*/

    /*public async Task<ApiResponse> Handle(UpdateExpenseCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Expense>().Where(x => x.ExpenseId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }

        fromdb.Category = request.Model.Category;
        fromdb.Date = request.Model.Date;
        fromdb.Amount = request.Model.Amount;
        
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
        
        dbContext.Expenses.Remove(fromdb);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }*/
}