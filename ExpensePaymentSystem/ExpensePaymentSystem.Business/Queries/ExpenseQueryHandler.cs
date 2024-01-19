using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Cqrs;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Queries;

public class ExpenseQueryHandler//:
    //IRequestHandler<GetAllExpenseQuery, ApiResponse<List<ExpenseResponse>>>,
    //IRequestHandler<GetExpenseByIdQuery, ApiResponse<ExpenseResponse>>,
    //IRequestHandler<GetExpenseByParameterQuery, ApiResponse<List<ExpenseResponse>>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public ExpenseQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    /*public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetAllExpenseQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Expense>()
            .ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<Expense>, List<ExpenseResponse>>(list);
         return new ApiResponse<List<ExpenseResponse>>(mappedList);
    }

    /*public async Task<ApiResponse<ExpenseResponse>> Handle(GetExpenseByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<Expense>()
            .Include(x => x.Payment)
            .Include(x => x.User)
            .FirstOrDefaultAsync(x => x.ExpenseId == request.Id, cancellationToken);

        if (entity == null)
        {
            return new ApiResponse<ExpenseResponse>("Record not found");
        }
        
        var mapped = mapper.Map<Expense, ExpenseResponse>(entity);
        return new ApiResponse<ExpenseResponse>(mapped);
    }#1#

    public async Task<ApiResponse<List<ExpenseResponse>>> Handle(GetExpenseByParameterQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Expense>(true);
        
        var list =  await dbContext.Set<Expense>()
            .Include(x => x.Payment)
            .Include(x => x.User)
            .Where(predicate).ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<Expense>, List<ExpenseResponse>>(list);
        return new ApiResponse<List<ExpenseResponse>>(mappedList);
    }*/
}