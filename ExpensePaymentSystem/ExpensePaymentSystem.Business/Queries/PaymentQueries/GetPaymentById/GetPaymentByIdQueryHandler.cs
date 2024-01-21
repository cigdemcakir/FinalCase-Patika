using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Queries.ExpenseQueries.GetExpenseById;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Queries.PaymentQueries.GetPaymentById;

public class GetPaymentByIdQueryHandler : IRequestHandler<GetPaymentByIdQuery, ApiResponse<PaymentResponse>>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPaymentByIdQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<PaymentResponse>> Handle(GetPaymentByIdQuery request, CancellationToken cancellationToken)
    {
        var entity =  await _dbContext.Set<Payment>()
            .Include(x => x.Expense)
            .FirstOrDefaultAsync(x => x.PaymentId == request.Id, cancellationToken);

        if (entity == null)
            return new ApiResponse<PaymentResponse>("Record not found");

        var mapped = _mapper.Map<Payment, PaymentResponse>(entity);
        
        return new ApiResponse<PaymentResponse>(mapped);
    }
}