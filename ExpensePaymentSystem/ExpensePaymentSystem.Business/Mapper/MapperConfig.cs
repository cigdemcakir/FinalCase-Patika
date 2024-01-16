using AutoMapper;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;

namespace ExpensePaymentSystem.Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<ExpenseRequest, Expense>();
        CreateMap<Expense, ExpenseResponse>();
        
        CreateMap<PaymentRequest, Payment>();
        CreateMap<Payment, PaymentResponse>();
        
        CreateMap<ReportRequest, Report>();
        CreateMap<Report, ReportResponse>();
        
        CreateMap<UserRequest, User>();
        CreateMap<User, UserResponse>();
    }
}