using AutoMapper;
using ExpensePaymentSystem.Base.Response;
using ExpensePaymentSystem.Business.Commands.ExpenseCommands.UpdateExpense;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace ExpensePaymentSystem.Business.Commands.PaymentCommands.UpdatePayment;

public class UpdatePaymentCommandHandler : IRequestHandler<UpdatePaymentCommand, ApiResponse<PaymentResponse>>
{
    private readonly ExpensePaymentSystemDbContext _dbContext;
    private readonly IMapper _mapper;

    public UpdatePaymentCommandHandler(ExpensePaymentSystemDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public async Task<ApiResponse<PaymentResponse>> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await _dbContext.Set<Payment>().Where(x => x.PaymentId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (fromdb == null)
            return new ApiResponse<PaymentResponse>("Record not found");

        fromdb.PaymentMethod = request.Model.PaymentMethod;

        var response = _mapper.Map<PaymentResponse>(fromdb);
        
        await _dbContext.SaveChangesAsync(cancellationToken);
        
        return new ApiResponse<PaymentResponse>(response);
    }
}