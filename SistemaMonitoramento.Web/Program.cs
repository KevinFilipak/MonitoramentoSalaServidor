using SistemaMonitoramento.Model.IoC;
using Microsoft.AspNetCore.Authentication.Cookies;
using SistemaMonitoramento.Model;
using System.Security.Claims;
using SistemaMonitoramento.Utill.Servicos;

IConfiguration configuration = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.json")
                            .Build();

IConfiguration configuration_dev = new ConfigurationBuilder()
                            .AddJsonFile("appsettings.Development.json")
                            .Build();

var builder = WebApplication.CreateBuilder(new WebApplicationOptions
{
    Args = args,
    ContentRootPath = Directory.GetCurrentDirectory(),
    WebRootPath = "wwwroot",
});


IHostEnvironment environment = builder.Environment;

// Add services to the container.

builder.Services.AddControllersWithViews();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
                    .AddCookie(options =>
                    {
                        options.LoginPath = "/Home/Account/Login";
                        options.AccessDeniedPath = "/Denied";
                        options.Events = new CookieAuthenticationEvents()
                        {
                            OnSigningIn = async context =>
                            {
                                await Task.CompletedTask;
                            },
                            OnSignedIn = async context =>
                            {
                                await Task.CompletedTask;
                            },
                            OnValidatePrincipal = async context =>
                            {
                                await Task.CompletedTask;
                            },
                        };
                    });


builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

builder.Services.AddDistributedSqlServerCache(options =>
{
    if (builder.Environment.EnvironmentName == "Development")
    {
        options.ConnectionString = configuration_dev.GetSection("ConnectionStrings:Connection").Value;
    }
    else
    {
        options.ConnectionString = configuration.GetSection("ConnectionStrings:Connection").Value;
    }


    options.SchemaName = "dbo";
    options.TableName = "tb_session";
});

builder.Services.AddSession(options => {
    options.IdleTimeout = TimeSpan.FromMinutes(10);
});

builder.Services.AddScoped<IContext, Context>();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Dashboard/Error");

    app.UseHsts();

    AppSettingsHelper.AppSettingsConfigure(configuration);
}
else
{
    app.UseExceptionHandler("/Home/Dashboard/Error");

    AppSettingsHelper.AppSettingsConfigure(configuration_dev);
}

app.Use(async (ctx, next) =>
{
    await next();

    if (ctx.Response.StatusCode == 404 && !ctx.Response.HasStarted)
    {
        //Re-execute the request so the user gets the error page
        string originalPath = ctx.Request.Path.Value;
        ctx.Items["originalPath"] = originalPath;
        ctx.Request.Path = "/Home/Dashboard/PageNotFound";
        await next();
    }
});

//app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{path=Home}/{controller=Dashboard}/{action=Index}/{id?}");
app.UseSession();



app.Run();
