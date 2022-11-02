using FluentValidation;
using FluentValidation.AspNetCore;
using Siemens.BLL.Service;
using Siemens.DAL.ORM.Context;
using Siemens.Dto.Models;
using Siemens.Mapping.Models;
using Siemens.Validation.Models.Product;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    .AddFluentValidation();

builder.Services.AddAutoMapper(typeof(CreateRequestProductProfile));


builder.Services
    .AddScoped<IValidator<CreateProductRequestDto>, CreateProductRequestValidator>();

builder.Services.AddDbContext<SiemensContext>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

var app = builder.Build();

app.MapControllers();


app.Run();
