using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json.Serialization;
using POC_Project.BL.Interface;
using POC_Project.BL.Mapper;
using POC_Project.BL.Repository;
using POC_Project.DAL.DataBase;
using POC_Project.PL.Languages;
using System.Globalization;
using POC_Project.DAL.Extend;

var builder = WebApplication.CreateBuilder(args);

//Add services to the container.
//Add Newtosoftjson for format problem.
//Add Localization Configuration.
builder.Services.AddControllersWithViews()
    .AddViewLocalization(LanguageViewLocationExpanderFormat.Suffix)
    .AddDataAnnotationsLocalization(opt =>
    {
        opt.DataAnnotationLocalizerProvider = (type, factory) =>
            factory.Create(typeof(SharedResource));
    })
    .AddNewtonsoftJson(Opt =>
    {
        Opt.SerializerSettings.ContractResolver = new DefaultContractResolver();
    });

//Add Configuration For Dependency Injection.
builder.Services.AddScoped<IDepartment, DepartmentRep>();
builder.Services.AddScoped<IEmployee, EmployeeRep>();
builder.Services.AddScoped<ICountry, CountryRep>();
builder.Services.AddScoped<ICity, CityRep>();
builder.Services.AddScoped<IDistrict, DistrictRep>();

//Add ConnectionString Configuration.
var ConnectionString = builder.Configuration.GetConnectionString("MyConnectionString");
builder.Services.AddDbContext<ApplicationContext>(opt => opt.UseSqlServer(ConnectionString));

//Add Automapper Configuration.
builder.Services.AddAutoMapper(x => x.AddProfile(new DomainProfile()));

//Add Configuration For Identity.
builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
    option =>
    {
        option.LoginPath = new PathString("Account/Login");
        option.AccessDeniedPath = new PathString("Account/Login");
    }
    );

builder.Services.AddIdentityCore<ApplicationUser>(option => option.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationContext>()
    .AddTokenProvider<DataProtectorTokenProvider<ApplicationUser>>(TokenOptions.DefaultProvider);


//Password and userName Configration.
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(option =>
{
    //Default Password settings.
    option.User.RequireUniqueEmail = true; 
    option.Password.RequireDigit = true;
    option.Password.RequireNonAlphanumeric = true;
    option.Password.RequireLowercase = true;
    option.Password.RequireUppercase = true;
    option.Password.RequiredLength = 6;
    option.Password.RequiredUniqueChars = 0;

}).AddEntityFrameworkStores<ApplicationContext>();

/*************************************************************************************************/
var app = builder.Build();


//MiddelWare For Languages.
var supportedCultures = new[]
{
    new CultureInfo("ar-EG"),
    new CultureInfo("en-US")
};

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

//Endpoint for localization.
app.UseRequestLocalization(new RequestLocalizationOptions
{
    DefaultRequestCulture = new RequestCulture("en-US"),
    SupportedCultures = supportedCultures,
    SupportedUICultures = supportedCultures,
    RequestCultureProviders = new List<IRequestCultureProvider> 
    {
        new QueryStringRequestCultureProvider(),
        new CookieRequestCultureProvider()
    }
});

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();    

app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Account}/{action=Login}/{id?}");

app.Run();
