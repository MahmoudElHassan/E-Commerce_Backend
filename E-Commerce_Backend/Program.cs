using E;
using E_Commerce_BL;
using E_Commerce_DAL;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();

var connectinString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectinString));

builder.Services.AddAutoMapper(typeof(AutoMapperProfile).Assembly);

builder.Services.AddApplicationServices();

builder.Services.AddSwaggerDocumentation();

#region Allow Cors

//string allowPolicy = "AllowPolicy";

//builder.Services.AddCors(options =>
//{
//    options.AddPolicy(allowPolicy, policy =>
//    {
//        policy.AllowAnyHeader()
//        .AllowAnyOrigin()
//        .AllowAnyMethod();
//    });
//});

#endregion

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();
app.UseStatusCodePagesWithReExecute("/errors/{0}");

app.UseHttpsRedirection();

//app.UseRouting();
app.UseStaticFiles();

app.UseAuthorization();

app.UseSwaggerDocumentation();

app.MapControllers();


//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllers();
//});


//using var scope = app.Services.CreateScope();
//var services = scope.ServiceProvider;
//var context = services.GetRequiredService<ApplicationDbContext>();
//var loggerFactory = services.GetRequiredService<ILoggerFactory>();
//try
//{
//    await context.Database.MigrateAsync();
//    await StoreContextSeed.SeedAsync(context, loggerFactory);
//}
//catch (Exception ex)
//{
//    var logger = loggerFactory.CreateLogger<Program>();
//    logger.LogError(ex, "An error occured during migration");
//}


app.Run();
