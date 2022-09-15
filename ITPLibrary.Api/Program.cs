using ITPLibrary.Api.Core.Configurations;
using ITPLibrary.Api.Core.GenericConstants;
using ITPLibrary.Api.Core.Services.Implementations;
using ITPLibrary.Api.Core.Services.Interfaces;
using ITPLibrary.Api.Data.Configurations;
using ITPLibrary.Api.Data.Data;
using ITPLibrary.Api.Data.Data.Data_Provider.Implementations;
using ITPLibrary.Api.Data.Data.Data_Provider.Interfaces;
using ITPLibrary.Api.Data.Repositories.Implementations;
using ITPLibrary.Api.Data.Repositories.Interfaces;
using ITPLibrary.Api.OperationFilters;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSwaggerGen(option =>
{
    option.SwaggerDoc(GenericConstant.SwaggerDocVersion, new OpenApiInfo { Title = GenericConstant.SwaggerDocTitle, Version = GenericConstant.SwaggerDocVersion });
    option.OperationFilter<SwaggerFileOperationFilter>();

    option.AddSecurityDefinition(GenericConstant.SecurityDef, new OpenApiSecurityScheme
    {
        In = ParameterLocation.Header,
        Description = GenericConstant.SecurityDefDescription,
        Name = GenericConstant.SecurityDefName,
        Type = SecuritySchemeType.Http,
        BearerFormat = GenericConstant.SecurityDefBearerFormat,
        Scheme = GenericConstant.SecurityDefScheme,
    });

    option.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type=ReferenceType.SecurityScheme,
                    Id=GenericConstant.SecurityReqId
                }
            },
            new string[]{}
        }

});
});

var jwtConfig = new JwtConfiguration();
builder.Configuration.Bind(nameof(JwtConfiguration), jwtConfig);
builder.Services.AddSingleton(jwtConfig);

var passwordRecoveryConfig = new PasswordRecoveryConfiguration();
builder.Configuration.Bind(nameof(PasswordRecoveryConfiguration), passwordRecoveryConfig);
builder.Services.AddSingleton(passwordRecoveryConfig);

var portAndHostConfig = new PortAndHostConfiguration();
builder.Configuration.Bind(nameof(PortAndHostConfiguration), portAndHostConfig);
builder.Services.AddSingleton(portAndHostConfig);

builder.Services.AddScoped<IUserLoginService, UserLoginService>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IBookRepository, BookRepository>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<IBookDataProvider, BookDataProvider>();

builder.Services.AddScoped<IRecoveryCodeRepository, RecoveryCodeRepository>();
builder.Services.AddScoped<IRecoveryCodeService, RecoveryCodeService>();


builder.Services.AddScoped<IShoppingCartService, ShoppingCartService>();
builder.Services.AddScoped<IShoppingCartRepository, ShoppingCartRepository>();

builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(
    builder.Configuration.GetConnectionString("DefaultConnection")));

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
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.Key))
    };
});

builder.Services.AddAuthorization();

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
    var db = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    db.Database.Migrate();
}

app.Run();


