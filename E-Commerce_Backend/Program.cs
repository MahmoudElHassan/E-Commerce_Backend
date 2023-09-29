using E_Commerce_BL;
using E_Commerce_BL.ManagerDTOs;
using E_Commerce_DAL;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectinString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectinString));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);


#region Reposatories
builder.Services.AddScoped<IProductRepo, ProductRepo>();
builder.Services.AddScoped<IProductBrandRepo, ProductBrandRepo>();
builder.Services.AddScoped<IProductTypeRepo, ProductTypeRepo>();
#endregion

#region Managers
builder.Services.AddScoped<IProductManager, ProductManager>();
builder.Services.AddScoped<IProductBrandManager, ProductBrandManager>();
builder.Services.AddScoped<IProductTypeManager, ProductTypeManager>();
#endregion

#region Allow Cors

string allowPolicy = "AllowPolicy";

builder.Services.AddCors(options =>
{
    options.AddPolicy(allowPolicy, policy =>
    {
        policy.AllowAnyHeader()
        .AllowAnyOrigin()
        .AllowAnyMethod();
    });
});

#endregion

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
