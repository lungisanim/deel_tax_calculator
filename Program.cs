using Microsoft.EntityFrameworkCore;
using TaxCalculator.Data;
using TaxCalculator.Service.Contract;
using TaxCalculator.Service.Implementation;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddMvc();
builder.Services.AddDbContext<TaxCalculationContext>(x => x.UseSqlServer(builder.Configuration.GetConnectionString("Default")));
builder.Services.AddScoped<ITaxCalculator, TaxCalculator.Service.Implementation.TaxCalculator>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
