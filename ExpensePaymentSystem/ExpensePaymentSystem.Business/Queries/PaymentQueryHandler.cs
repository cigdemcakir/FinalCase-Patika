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

public class PaymentQueryHandler:
    IRequestHandler<GetAllPaymentQuery, ApiResponse<List<PaymentResponse>>>,
    IRequestHandler<GetPaymentByIdQuery, ApiResponse<PaymentResponse>>,
    IRequestHandler<GetPaymentByParameterQuery, ApiResponse<List<PaymentResponse>>>
{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public PaymentQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }

    public async Task<ApiResponse<List<PaymentResponse>>> Handle(GetAllPaymentQuery request,
        CancellationToken cancellationToken)
    {
        var list = await dbContext.Set<Payment>()
            .Include(x => x.Expense)
            .ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<Payment>, List<PaymentResponse>>(list);
         return new ApiResponse<List<PaymentResponse>>(mappedList);
    }

    public async Task<ApiResponse<PaymentResponse>> Handle(GetPaymentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var entity =  await dbContext.Set<Payment>()
            .Include(x => x.Expense)
            .FirstOrDefaultAsync(x => x.PaymentId == request.Id, cancellationToken);

        if (entity == null)
        {
            return new ApiResponse<PaymentResponse>("Record not found");
        }
        
        var mapped = mapper.Map<Payment, PaymentResponse>(entity);
        return new ApiResponse<PaymentResponse>(mapped);
    }

    public async Task<ApiResponse<List<PaymentResponse>>> Handle(GetPaymentByParameterQuery request,
        CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Payment>(true);
        
        var list =  await dbContext.Set<Payment>()
            .Include(x => x.Expense)
            .Where(predicate).ToListAsync(cancellationToken);
        
        var mappedList = mapper.Map<List<Payment>, List<PaymentResponse>>(list);
        return new ApiResponse<List<PaymentResponse>>(mappedList);
    }
}