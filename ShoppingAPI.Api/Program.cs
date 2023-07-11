using Shopping.Business.Abstract;
using Shopping.Business.Concrete;
using Shopping.DAL.Abstract.DataManagement;
using Shopping.DAL.Concrete.EntityFramework.Context;
using Shopping.DAL.Concrete.EntityFramework.DataManagement;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


/*IOC --> Inversion Of Control*/

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHttpContextAccessor();
builder.Services.AddDbContext<ShoppingContext>();
builder.Services.AddScoped<IUnitOfWork, EfUnitOfWork>();
builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<IUserService, UserManager>();

var app = builder.Build();

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
