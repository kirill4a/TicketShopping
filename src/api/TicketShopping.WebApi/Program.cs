using TicketShopping.Application.DI;
using TicketShopping.Persistence.Repositories.DI;
using Microsoft.OpenApi.Models;
using TicketShopping.Integration.Aviationstack.DI;

const string AppVersion = "v1";

var builder = WebApplication.CreateBuilder(args);

/* Register application stuff */
builder.Services.RegisterRepositories(builder.Configuration.GetConnectionString("Default"));
builder.Services.RegisterAviationstackIntegration();

/* Configure MediatR */
builder.Services.ConfigureMediator();

builder.Services.AddControllers();
/* Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle */
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(opt =>
    {
        opt.SupportNonNullableReferenceTypes();
        opt.SwaggerDoc(AppVersion, new OpenApiInfo()
        {
            Title = "TicketShopping.WebApi",
            Version = AppVersion
        });
    });

/* Configure the HTTP request pipeline. */
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.Use(async (ctx, next) =>
{
    ctx.Request.EnableBuffering();
    await next();
});

app.UseExceptionHandler("/error");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
