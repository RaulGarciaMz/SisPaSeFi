using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;
using DomainServices.DomServ;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serilog;
using SqlServerAdapter;
using SqlServerAdapter.Data;
using System.Reflection;
using System.Text;

Log.Logger = new LoggerConfiguration()
    .MinimumLevel.Information()
    .WriteTo.File("SSF.log", rollingInterval: RollingInterval.Day)
    .CreateLogger();

var builder = WebApplication.CreateBuilder(args);

builder.Host.UseSerilog();

// Add services to the container.

builder.Services.AddControllers(options =>
{
    options.ReturnHttpNotAcceptable = true;
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup => {
    var xmlCommFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
    var xmlCommPath = Path.Combine(AppContext.BaseDirectory, xmlCommFile);
    setup.IncludeXmlComments(xmlCommPath);

    //TODO Eliminar esto posterior a dar resolución de conflicto de operaciones Para eliminar conflictos de operaciones múltiples
  /*  setup.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    setup.IgnoreObsoleteActions();
    setup.IgnoreObsoleteProperties();
    setup.CustomSchemaIds(type => type.FullName);*/
});


builder.Services.AddScoped<IRutasRepo, RutaRepository>();
builder.Services.AddScoped<IRutaService, RutaService>();
builder.Services.AddScoped<IPuntoPatrullajeRepo, PuntoPatrullajeRepository>();
builder.Services.AddScoped<IPuntosService, PuntosService>();
builder.Services.AddScoped<IProgramaPatrullajeRepo, ProgramaPatrullajeRepository>();
builder.Services.AddScoped<IProgramaService, ProgramasService>();
builder.Services.AddScoped<ITarjetaInformativaRepo, TarjetaInformativaRepository>();
builder.Services.AddScoped<ITarjetaService, TarjetasService>();
builder.Services.AddScoped<ICatalogosConsultaRepo, CatalogoConsultasRepository>();
builder.Services.AddScoped<ICatalogosConsultaService, CatalogoConsultasService>();

builder.Services.AddDbContext<PatrullajeContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<ProgramaContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<RutaContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<TarjetaInformativaContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<CatalogosConsultaContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));

/*builder.Services.AddAuthentication("Bearer")
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new() 
        { 
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateIssuerSigningKey= true,
            ValidIssuer = builder.Configuration["Authentication:Issuer"],
            ValidAudience = builder.Configuration["Authentication:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(builder.Configuration["Authentication:SecretForKey"]))
        };
    }
    ); */

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
