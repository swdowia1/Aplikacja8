using Microsoft.AspNetCore.Authentication.Cookies;
using System.Configuration;
using WebApp.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddSingleton<MailSetting>(builder.Configuration.GetSection("MailSetting").Get<MailSetting>());
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                 .AddCookie(item => item.LoginPath = new PathString("/account/login"))



                 // Way 2
                 .AddMicrosoftAccount(option =>
                 {
                     option.ClientId = builder.Configuration.GetValue<string>("Authentication:Microsoft:clientid");
                     option.ClientSecret = builder.Configuration.GetValue<string>("Authentication:Microsoft:ClientSecret");
                     option.SaveTokens = true;
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

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
