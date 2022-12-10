using LoginWebApp;
using LoginWebApp.Context;
using LoginWebApp.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
byte[] chave = Encoding.ASCII.GetBytes(Settings.Segredo);
// Add services to the container.

builder.Services.AddControllersWithViews();
builder.Services.AddTransient<IAutenticacaoService, AutenticacaoService>();
builder.Services.AddTransient<IUsuarioService, UsuarioService>();
builder.Services.AddDbContext<LoginContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAuthentication(auth =>
{
    auth.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    auth.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(auth =>
    {
        auth.RequireHttpsMetadata = false;
        auth.SaveToken = true;
        auth.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(chave),
            ValidateIssuer = false,
            ValidateAudience = false
        };
    });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

/*
app.MapControllerRoute(
    name: "default",
    pattern: "{controller}/{action=Index}/{id?}");
*/

app.MapFallbackToFile("index.html"); ;

app.Run();
