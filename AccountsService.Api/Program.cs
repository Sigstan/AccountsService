using AccountsService.Api.Infrastructure.Exceptions;
using AccountsService.Api.Infrastructure.Swagger;
using AccountsService.Core.Repositories.Accounts;
using AccountsService.Core.Repositories.Cashbacks;
using AccountsService.Core.Repositories.Operations;
using AccountsService.Core.Services.Accounts;
using AccountsService.Core.Services.Operations;
using AccountsService.Storage.Data;
using Microsoft.EntityFrameworkCore;

namespace AccountsService.Api
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = builder.Configuration.GetConnectionString("DatabaseConnection")
                                   ?? throw new InvalidOperationException("Connection string 'DatabaseConnection' not found.");
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            
            builder.Services.AddControllers();
            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen(options =>
            {
                options.SchemaFilter<EnumSchemaFilter>();
            });

            builder.Services.AddScoped<IAccountsRepository, AccountsRepository>();
            builder.Services.AddScoped<ICashbacksRepository, CashbacksRepository>();
            builder.Services.AddScoped<IOperationsRepository, OperationsRepository>();

            builder.Services.AddScoped<IAccountsService, Core.Services.Accounts.AccountsService>();
            builder.Services.AddScoped<IOperationsService, OperationsService>();

            builder.Services.AddMvc(config =>
                config.Filters.Add(typeof(DomainExceptionFilter)));
            
            var app = builder.Build();

            using (var scope = app.Services.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
                db.Database.Migrate();
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}