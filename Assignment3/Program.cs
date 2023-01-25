/* Assignment3.sln
 * 
 * PROG2230 - Section 1
 * 
 * Created by Daniela Onici
 * 
 * Revision History:
 * 
 *      2022/12/02: created the project, entities, relationships and set DB
 *      2022/12/04: fix DB style patterns; created the folders/views
 *      2022/12/05: controllers created and fixed
 *      2022/12/06: fixed model views, partial view and component
 *      2022/12/07: finished
 * 
 */


using Assignment3.DataAccess;
using Assignment3.Services;
using Microsoft.EntityFrameworkCore;
using Vendors.Service;
using Vendors.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var connStr = builder.Configuration.GetConnectionString("DOniciVendor");
builder.Services.AddDbContext<VendorDbContext>(options => options.UseSqlServer(connStr));

builder.Services.AddScoped<IVendorManager, VendorManager>();

builder.Services.AddScoped<IInvoiceManager, InvoiceManager>();

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
