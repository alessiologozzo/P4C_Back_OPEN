using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.Negotiate;
using Microsoft.EntityFrameworkCore;
using P4C_Back.Data;
using P4C_Back.Exceptions;
using P4C_Back.Middlewares;
using P4C_Back.Services;
using P4C_Back.Validators.Report;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication(NegotiateDefaults.AuthenticationScheme)
   .AddNegotiate();

builder.Services.AddAuthorization(options =>
{
    // By default, all incoming requests will be authorized according to the default policy.
    options.FallbackPolicy = options.DefaultPolicy;
});

var connectionString = builder.Configuration.GetConnectionString("AppDbConnectionString");
builder.Services.AddDbContext<AppDbContext>(options => options.UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services.AddTransient<AuthMiddleware>();

builder.Services.AddScoped<CriterioService>();
builder.Services.AddScoped<KpiService>();
builder.Services.AddScoped<ReportService>();
builder.Services.AddScoped<AbilitazioneService>();
builder.Services.AddScoped<DashboardService>();

builder.Services.AddValidatorsFromAssemblyContaining<CreateReportValidator>();
builder.Services.AddFluentValidationAutoValidation(fv =>
{
    fv.DisableDataAnnotationsValidation = true;
});
builder.Services.AddFluentValidationClientsideAdapters();

var corsFrontApp = "P4C_Front";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsFrontApp,
                      policy =>
                      {
                          policy.WithOrigins("http://localhost:4200").AllowCredentials().AllowAnyMethod().AllowAnyHeader();
                      });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors(corsFrontApp);

app.UseAuthorization();

app.MapControllers();

app.UseMiddleware<ExceptionMiddleware>();
app.UseMiddleware<AuthMiddleware>();

app.Run();
