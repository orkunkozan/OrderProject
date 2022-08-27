using AktifBank.CustomerOrder.Api.Middleware;
using AktifBank.CustomerOrder.Business.Services.Abstract;
using AktifBank.CustomerOrder.Business.Services.CustomerOrderService;
using AktifBank.CustomerOrder.DataAccess.Contexts;
using FluentValidation.AspNetCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "AktifBank",
        Description = "Customer Order"
    });
});

// Add services to the container.
builder.Services.AddDbContext<ProjectDbContext>(ServiceLifetime.Scoped);
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<Program>());
builder.Services.AddFluentValidation(x => x.RegisterValidatorsFromAssemblyContaining<CustomerOrderService>());
builder.Services.AddTransient<ICustomerOrderService, CustomerOrderService>();
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var context = services.GetRequiredService<ProjectDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseSwagger();
app.UseSwaggerUI(c => { c.SwaggerEndpoint("v1/swagger.json", "Aktif Bank"); });

app.UseHttpsRedirection();
app.UseRouting();
app.UseAuthorization();


app.UseEndpoints(endpoints => { endpoints.MapControllers(); });

app.Run();
