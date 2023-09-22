using Customer.Service;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRefitClient<ICustomerWebService>(provider => new RefitSettings
{
    CollectionFormat = CollectionFormat.Multi,

}).ConfigureHttpClient(c => c.BaseAddress = new Uri(builder.Configuration.GetSection("Services")["External:UserService:URI"]));

builder.Services.AddSingleton<ICustomerService, CustomerService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
