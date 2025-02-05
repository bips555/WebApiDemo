using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using WebApiDemo.Data;
using WebApiDemo.Filters.OperationFilter;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<ApplicationDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddApiVersioning(options=>
{
    options.ReportApiVersions = true;
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.DefaultApiVersion = new Microsoft.AspNetCore.Mvc.ApiVersion(1, 0);
   // options.ApiVersionReader = new HeaderApiVersionReader("X-API-Version");
});
builder.Services.AddVersionedApiExplorer(options=>

    options.GroupNameFormat = "'v'VVV"
);
builder.Services.AddSwaggerGen(c=>
{
    c.OperationFilter<AuthorizationHeaderOperationFilter>();
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Scheme = "Bearer",
        Type = SecuritySchemeType.Http,
        BearerFormat = "JWT",
        In= ParameterLocation.Header,

    });
});
var app = builder.Build();

// Configure the HTTP request pipeline.

if(app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options=>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
    });
}
app.UseHttpsRedirection();

app.MapControllers();
app.Run();

