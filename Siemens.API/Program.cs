using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Siemens.API.Models.Filters;
using Siemens.BLL.Service;
using Siemens.DAL.ORM.Context;
using Siemens.Dto.Models;
using Siemens.Mapping.Models;
using Siemens.Validation.Models.Product;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers(options => options.Filters.Add(typeof(ExceptionFilter)))
    .AddFluentValidation();


builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidIssuer = "cagatay@mail.com",
        ValidAudience = "cagatay1@mail.com",
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("ironmaidenpentagramslipknotironmaidenpentagramslipknot")),
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero
    };
});



builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Version = "v1",
        Title = "Siemens API Document",
        Description = "lorem ipsum",
        TermsOfService = new Uri("https://siemens.com.tr"),
        Contact = new OpenApiContact
        {
            Name = "Siemens Concact",
            Url = new Uri("https://siemens.com.tr")
        }
    });
});

builder.Services.AddAutoMapper(typeof(CreateRequestProductProfile));
builder.Services.AddMemoryCache();

builder.Services
    .AddScoped<IValidator<CreateProductRequestDto>, CreateProductRequestValidator>();

builder.Services.AddDbContext<SiemensContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();


app.UseSwagger();
app.UseSwaggerUI();

app.Run();
