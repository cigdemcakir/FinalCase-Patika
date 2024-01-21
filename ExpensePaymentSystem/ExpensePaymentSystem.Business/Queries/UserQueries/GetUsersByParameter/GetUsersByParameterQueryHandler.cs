using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using LinqKit;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Queries.UserQueries.GetUsersByParameter;


public class GetUsersByParameterQueryHandler : IRequestHandler<GetUsersByParameterQuery, ApiResponse<List<UserResponse>>>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetUsersByParameterQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<UserResponse>>> Handle(GetUsersByParameterQuery request, CancellationToken cancellationToken)
    {
        var predicate = PredicateBuilder.New<User>(true);
        
        if (!string.IsNullOrEmpty(request.UserName))
            predicate = predicate.And(x => x.UserName.ToUpper().Contains(request.UserName.ToUpper()));

        if (!string.IsNullOrEmpty(request.FirstName))
            predicate = predicate.And(x => x.FirstName.ToUpper().Contains(request.FirstName.ToUpper()));
        
        if (!string.IsNullOrEmpty(request.LastName))
            predicate.And(x => x.LastName.ToUpper().Contains(request.LastName.ToUpper()));
        
        var list =  await _dbContext.Set<User>()
            .Include(x=>x.Expenses)
            .Include(x=>x.Reports)
            .Where(predicate).ToListAsync(cancellationToken);
        
        var mappedList = _mapper.Map<List<User>, List<UserResponse>>(list);
       
        return new ApiResponse<List<UserResponse>>(mappedList);
    }
}