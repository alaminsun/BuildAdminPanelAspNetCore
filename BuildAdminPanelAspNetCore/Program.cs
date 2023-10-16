using BuildAdminPanelAspNetCore.ActionFilter;
using Microsoft.CodeAnalysis.Options;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
//builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages().AddRazorRuntimeCompilation();
builder.Services.AddTransient<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddTransient<ActionAuth>();
builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath = "/SA/LoginRegistration/Login";
    options.LogoutPath = "/SA/LoginRegistration/Logout";
    options.AccessDeniedPath = "/SA/LoginRegistration/AccessDenied";

});

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(100);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


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
//StripeConfiguration.ApiKey = builder.Configuration.GetSection("Stripe:SecretKey").Get<string>();
app.UseAuthentication(); ;

app.UseAuthorization();
app.UseSession();
app.MapRazorPages();
//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller=LoginRegistration}/{action=Login}/{id?}");
//app.UseEndpoints(endpoints =>
//{
//    endpoints.MapControllerRoute(
//        name: "default",
//        pattern: "{controller=Home}/{action=Index}/{id?}");
//});
app.UseEndpoints(endpoints =>
{
    //endpoints.MapAreaControllerRoute(
    // name: "Inventory",
    // areaName: "Inventory",

    // pattern: "Inventory/{controller=FGReceive}/{action=ReceiveFromProduction}");
    //endpoints.MapAreaControllerRoute(
    //  name: "SalesAndDistribution",
    //  areaName: "SalesAndDistribution",

    //  pattern: "SalesAndDistribution/{controller=Division}/{action=DivisionInfo}");
    //endpoints.MapAreaControllerRoute(
    //  name: "Security",
    //  areaName: "Security",
    //  pattern: "Security/{controller=Login}/{action=Index}");

    //endpoints.MapControllerRoute(
    //    name: "default",
    //    pattern: "{controller=Home}/{action=Index}/{id?}");
    endpoints.MapAreaControllerRoute(
      name: "areaRoute",
      areaName: "SA",
      pattern: "{area}/{controller}/{action}/{id?}",
      defaults: new { area = "SA", controller = "LoginRegistration", action = "Login" });
    endpoints.MapControllerRoute(
        name: "default",
        pattern: "{controller=Home}/{action=Index}/{id?}");
});


app.Run();

//app.UseStaticFiles();

//app.UseRouting();

//app.MapControllerRoute(
//    name: "default",
//    pattern: "{controller}/{action}/{id?}");

////app.MapGet("/", () => "Hello World!");

//app.Run();
