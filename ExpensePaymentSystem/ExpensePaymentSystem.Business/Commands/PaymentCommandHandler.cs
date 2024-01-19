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

public class PaymentCommandHandler/*:
    IRequestHandler<CreatePaymentCommand, ApiResponse<PaymentResponse>>,
    IRequestHandler<UpdatePaymentCommand,ApiResponse>,
    IRequestHandler<DeletePaymentCommand,ApiResponse>*/

{
    private readonly ExpensePaymentSystemDbContext dbContext;
    private readonly IMapper mapper;

    public PaymentCommandHandler(ExpensePaymentSystemDbContext dbContext,IMapper mapper)
    {
        this.dbContext = dbContext;
        this.mapper = mapper;
    }
/*
    public async Task<ApiResponse<PaymentResponse>> Handle(CreatePaymentCommand request, CancellationToken cancellationToken)
    {

        var entity = mapper.Map<PaymentRequest, Payment>(request.Model);
        await dbContext.Payments.AddAsync(entity, cancellationToken);
        await dbContext.SaveChangesAsync(cancellationToken);

        var mapped = mapper.Map<Payment, PaymentResponse>(entity);
        return new ApiResponse<PaymentResponse>(mapped);
    }

    public async Task<ApiResponse> Handle(UpdatePaymentCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Payment>().Where(x => x.PaymentId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        fromdb.PaymentMethod = request.Model.PaymentMethod;
        fromdb.PaymentDate = request.Model.PaymentDate;
        fromdb.Amount = request.Model.Amount;
        
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    }

    public async Task<ApiResponse> Handle(DeletePaymentCommand request, CancellationToken cancellationToken)
    {
        var fromdb = await dbContext.Set<Payment>().Where(x => x.PaymentId == request.Id)
            .FirstOrDefaultAsync(cancellationToken);
        
        if (fromdb == null)
        {
            return new ApiResponse("Record not found");
        }
        
        dbContext.Payments.Remove(fromdb);
        await dbContext.SaveChangesAsync(cancellationToken);
        return new ApiResponse();
    } */
}