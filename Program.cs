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
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(5);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddCors(options => options.AddPolicy(name: "LoginOrigins",
    policy =>
    {
    policy.WithOrigins("https://localhost:44470").AllowAnyMethod().AllowAnyHeader().AllowCredentials();
    }));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.UseSession();
app.Use(async (context, next) =>
{
    string token = context.Session.GetString("Token");
    if (token != null)
        context.Request.Headers.Add("Authorization", "Bearer " + token);

    await next();
});
app.UseCors("LoginOrigins");
app.UseAuthentication();
app.UseAuthorization();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.MapControllers();
app.MapFallbackToFile("index.html"); ;

app.Run();
