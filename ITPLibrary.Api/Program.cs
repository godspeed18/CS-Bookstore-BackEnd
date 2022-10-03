using Constants;
using ITPLibrary.Api.OperationFilters;
using ITPLibrary.Application;
using ITPLibrary.Common;
using ITPLibrary.Infrastructure.InfrastructureService;
using ITPLibrary.Infrastructure.Persistance;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc(CommonConstants.SwaggerDocVersion, new OpenApiInfo { Title = CommonConstants.SwaggerDocTitle, Version = CommonConstants.SwaggerDocVersion });
    option.OperationFilter<SwaggerFileOperationFilter>();

    option.AddSecurityDefinition(CommonConstants.SecurityDef, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = CommonConstants.SecurityDefDescription,
        Name = CommonConstants.SecurityDefName,
        Type = SecuritySchemeType.Http,
        BearerFormat = CommonConstants.SecurityDefBearerFormat,
        Scheme = CommonConstants.SecurityDefScheme,
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=CommonConstants.SecurityReqId
                }
            },
            new string[]{}
        }

});
});

var stripeConfig = new PaymentConfiguration();
builder.Configuration.Bind(nameof(PaymentConfiguration), stripeConfig);
builder.Services.AddSingleton(stripeConfig);

var jwtConfig = new JwtConfiguration();
builder.Configuration.Bind(nameof(JwtConfiguration), jwtConfig);
builder.Services.AddSingleton(jwtConfig);

var passwordRecoveryConfig = new PasswordRecoveryConfiguration();
builder.Configuration.Bind(nameof(PasswordRecoveryConfiguration), passwordRecoveryConfig);
builder.Services.AddSingleton(passwordRecoveryConfig);

var portAndHostConfig = new PortAndHostConfiguration();
builder.Configuration.Bind(nameof(PortAndHostConfiguration), portAndHostConfig);
builder.Services.AddSingleton(portAndHostConfig);

builder.Services.AddApplicationServices(builder.Configuration);
builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddPersistanceServices(builder.Configuration);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
{
    options.RequireHttpsMetadata = false;
    options.SaveToken = true;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidAudience = jwtConfig.Audience,
        ValidIssuer = jwtConfig.Issuer,
        IssuerSigningKey = new SymmetricSecurityKey(Convert.FromBase64String(jwtConfig.Key))
    };
});

builder.Services.AddAuthorization();

builder.Services.AddHttpContextAccessor();

//Registering AutoMapper
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<ITPLibraryDbContext>();
    db.Database.Migrate();
}

app.Run();


