using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Siemens.BLL.Service;
using Siemens.DAL.ORM.Context;
using Siemens.Dto.Models;
using Siemens.Mapping.Models;
using Siemens.Validation.Models.Product;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
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


builder.Services.AddAutoMapper(typeof(CreateRequestProductProfile));

builder.Services
    .AddScoped<IValidator<CreateProductRequestDto>, CreateProductRequestValidator>();

builder.Services.AddDbContext<SiemensContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

app.MapControllers();

app.UseAuthentication();
app.UseAuthorization();


app.Run();
