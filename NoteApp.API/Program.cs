using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NoteApp.Application.Services;
using NoteApp.Application.UseCases.Lembretes;
using NoteApp.Application.UseCases.Usuarios;
using NoteApp.Domain.Repositories;
using NoteApp.Infraestructure.Data;
using NoteApp.Infraestructure.Models;
using NoteApp.Infraestructure.Repositories;
using NoteApp.Infraestructure.Seeds;
using NoteApp.Infraestructure.Services;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
builder.Services.AddScoped<ILembreteRepository, LembreteRepository>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();

builder.Services.AddScoped<CriarLembreteUseCase>();
builder.Services.AddScoped<AtualizarLembreteUseCase>();
builder.Services.AddScoped<RemoverLembreteUseCase>();
builder.Services.AddScoped<ObterLembretePorIdUseCase>();
builder.Services.AddScoped<ObterLembretesComVencimentoEmUmaSemanaUseCase>();
builder.Services.AddScoped<ObterLembretesComVencimentoHojeUseCase>();
builder.Services.AddScoped<ObterLembretesUseCase>();

builder.Services.AddScoped<CriarUsuarioUseCase>();
builder.Services.AddScoped<AtualizarUsuarioUseCase>();
builder.Services.AddScoped<RemoverUsuarioUseCase>();
builder.Services.AddScoped<ObterUsuarioPorIdUseCase>();
builder.Services.AddScoped<ObterUsuariosUseCase>();

builder.Services.AddDbContext<NoteAppContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("Database")));

builder.Services.AddIdentity<Usuarios, IdentityRole<Guid>>()
                .AddEntityFrameworkStores<NoteAppContext>()
                .AddDefaultTokenProviders();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
})
.AddJwtBearer(options =>
{
    options.SaveToken = true;
    options.RequireHttpsMetadata = false;
    options.TokenValidationParameters = new TokenValidationParameters()
    {
        RequireExpirationTime = true,
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidAudience = builder.Configuration["JWT:ValidAudience"],
        ValidIssuer = builder.Configuration["JWT:ValidIssuer"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Secret"]!)),
        ClockSkew = TimeSpan.Zero
    };
});

builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "NoteApp.API", Version = "v1" });
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Autorização JWT usando o esquema Bearer. \r\n\r\nInsira 'Bearer' [space] e seu token logo em seguida.\r\n\r\n Exemplo: 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement()
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                },
                Scheme = "oauth2",
                Name = "Bearer",
                In = ParameterLocation.Header,
            },
            new List<string>()
        }
    });
});

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;

    await RoleSeeder.Seed(services);
    await UsuarioSeeder.Seed(services);
}

app.MapControllers();

app.Run();
