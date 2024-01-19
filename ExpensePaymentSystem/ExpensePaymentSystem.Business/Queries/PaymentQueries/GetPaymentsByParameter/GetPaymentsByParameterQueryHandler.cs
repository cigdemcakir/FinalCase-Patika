using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Queries.PaymentQueries.GetPaymentsByParameter;

public class GetPaymentsByParameterQueryHandler : IRequestHandler<GetPaymentsByParameterQuery, ApiResponse<List<PaymentResponse>>>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetPaymentsByParameterQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<PaymentResponse>>> Handle(GetPaymentsByParameterQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<Payment>(true);
        
        if (request.Amount.HasValue)
            predicate = predicate.And(x => x.Amount == request.Amount.Value);

        if (request.PaymentDate.HasValue)
            predicate = predicate.And(x => x.PaymentDate == request.PaymentDate.Value.Date);

        if (string.IsNullOrEmpty(request.PaymentMethod))
            predicate.And(x => x.PaymentMethod.ToUpper().Contains(request.PaymentMethod.ToUpper()));
        
        var list =  await _dbContext.Set<Payment>()
            .Where(predicate).ToListAsync(cancellationToken);
        
        var mappedList = _mapper.Map<List<Payment>, List<PaymentResponse>>(list);
        return new ApiResponse<List<PaymentResponse>>(mappedList);
    }
}