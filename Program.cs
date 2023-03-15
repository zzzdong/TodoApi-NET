using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;

using Base.Authorizations;

using TodoApi.Models;
using TodoApi.Filters;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<TodoContext>(opt =>
    opt.UseInMemoryDatabase("TodoList"));

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
    });

    var schema = new OpenApiSecurityScheme() 
    {
        Reference = new OpenApiReference() 
        {
            Type = ReferenceType.SecurityScheme,
            Id = "Bearer",
        },
    };
    options.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        [schema] = new string[0],
    });
}
);


builder.Services.AddMvc(options =>
{
    options.Filters.Add<ResponseWrapFilter>();
});

builder.Services.AddAuthentication();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer();
// builder.Services.AddAuthentication("Bearer").AddJwtBearer();

// builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
//     .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme,
//         options => builder.Configuration.Bind("JwtSettings", options))
//     .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme,
//         options => builder.Configuration.Bind("CookieSettings", options));


builder.Services.AddSingleton<IAuthorizationHandler, PermissionAuthorizationHandler>();

builder.Services.AddSingleton<IAuthorizationPolicyProvider, PermissionPolicyProvider>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
