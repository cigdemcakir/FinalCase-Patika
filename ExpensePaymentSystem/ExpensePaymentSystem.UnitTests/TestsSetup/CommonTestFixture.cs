using AutoMapper;
using ExpensePaymentSystem.Business.Mapper;
using ExpensePaymentSystem.Data.DbContext;
using ExpensePaymentSystem.UnitTests.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace ExpensePaymentSystem.UnitTests.TestsSetup
{
    public class CommonTestFixture
    {
        public ExpensePaymentSystemDbContext Context { get; set; }
        public IMapper Mapper { get; set; }
        public IConfiguration Configuration { get; set; }
        
        public CommonTestFixture()
        {
            var inMemorySettings= new Dictionary<string, string>
            {
                {"Token:Issuer", "www.ExpensePaymentSystem.com"},
                {"Token:Audience", "www.ExpensePaymentSystem.com"},
                {"Token:SecurityKey", "This is my private secret key that I use for authentication in the expense payment system"},
            };
            
            Configuration = new ConfigurationBuilder()
                .AddInMemoryCollection(inMemorySettings)
                .Build();

            var options = new DbContextOptionsBuilder<ExpensePaymentSystemDbContext>().UseInMemoryDatabase(databaseName: "InMemoryTestDb").Options;
            Context=new ExpensePaymentSystemDbContext(options);
            Context.Database.EnsureCreated();
            Context.Initialize();

            Mapper = new MapperConfiguration(configure: cfg =>
            {
                cfg.AddProfile<MapperConfig>();
            }).CreateMapper();
        }
    }
}
