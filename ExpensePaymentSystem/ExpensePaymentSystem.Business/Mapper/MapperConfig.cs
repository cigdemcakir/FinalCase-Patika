using AutoMapper;
using ExpensePaymentSystem.Base.Enums;
using ExpensePaymentSystem.Data.Entity;
using ExpensePaymentSystem.Schema;

namespace ExpensePaymentSystem.Business.Mapper;

public class MapperConfig : Profile
{
    public MapperConfig()
    {
        CreateMap<ExpenseRequest, Expense>()
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => ExpenseStatus.Pending));

        CreateMap<Expense, ExpenseResponse>();
        
        CreateMap<PaymentRequest, Payment>();
        CreateMap<Payment, PaymentResponse>();
        
        CreateMap<ReportRequest, Report>();
        CreateMap<Report, ReportResponse>();
        
        CreateMap<UserRequest, User>();
        CreateMap<User, UserResponse>();
    }
}