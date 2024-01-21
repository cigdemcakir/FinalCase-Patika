using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Queries.UserQueries.GetAllUsers;


public class GetAllUsersQueryHandler : IRequestHandler<GetAllUsersQuery, ApiResponse<List<UserResponse>>>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetAllUsersQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<List<UserResponse>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var list = await _dbContext.Set<User>()
            .Include(x=>x.Expenses)
            .Include(x=>x.Reports)
            .ToListAsync(cancellationToken);
        
        var mappedList = _mapper.Map<List<User>, List<UserResponse>>(list);
       
        return new ApiResponse<List<UserResponse>>(mappedList);
    }
}