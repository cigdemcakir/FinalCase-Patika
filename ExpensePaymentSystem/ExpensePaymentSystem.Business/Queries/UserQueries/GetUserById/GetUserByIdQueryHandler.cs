using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Queries.UserQueries.GetUserById;


public class GetUserByIdQueryHandler : IRequestHandler<GetUserByIdQuery, ApiResponse<UserResponse>>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetUserByIdQueryHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<UserResponse>> Handle(GetUserByIdQuery request, CancellationToken cancellationToken)
    {
        var entity =  await _dbContext.Set<User>()
            .Include(x=>x.Expenses)
            .Include(x=>x.Reports)
            .FirstOrDefaultAsync(x => x.UserId == request.Id, cancellationToken);

        if (entity == null)
            return new ApiResponse<UserResponse>("Record not found");
        
        var mapped = _mapper.Map<User, UserResponse>(entity);
        
        return new ApiResponse<UserResponse>(mapped);
    }
}