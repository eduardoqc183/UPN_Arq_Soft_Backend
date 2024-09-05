using Dto.Model.MemoryDb;
using MediatR;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Persistencia.Assemblers;
using Persistencia.Models;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Disable CORS

builder.Services.AddCors(options => options.AddPolicy("CorsPolicy",
       builder =>
       {
           builder.AllowAnyHeader()
                  .AllowAnyMethod()
                  .SetIsOriginAllowed((host) => true)
                  .AllowCredentials();

       }));
// Add services to the container.
builder.Services.AddSingleton<IConfiguration>(builder.Configuration);

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediatR(Assembly.Load("Service.Command"));
builder.Services.AddMediatR(Assembly.Load("Service.Query"));
builder.Services.AddSingleton<ProductDictionary>();

builder.Services.AddDbContext<JorplastContext>(options =>
#if DEBUG
               options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"))
#endif
#if !DEBUG
               options.UseSqlServer(builder.Configuration.GetConnectionString("ProductionConnection"))
#endif
           );

// Enable Gzip compression
builder.Services.AddResponseCompression(options =>
{
    options.EnableForHttps = true;
    options.Providers.Add<GzipCompressionProvider>();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
//if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

using (var scope = app.Services.CreateScope())
{
    var dbContext = scope.ServiceProvider.GetRequiredService<JorplastContext>();
    var productDictionary = scope.ServiceProvider.GetRequiredService<ProductDictionary>();
    var products = dbContext.productos.ToList();
    productDictionary.LoadProducts(products.ToDTOs());
}


app.UseResponseCompression();
app.UseHttpsRedirection();
app.UseAuthorization();
app.UseCors("CorsPolicy");
app.MapControllers();
app.Run();
