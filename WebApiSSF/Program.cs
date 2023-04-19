using Domain.Ports.Driven;
using Domain.Ports.Driven.Repositories;
using Domain.Ports.Driving;
using DomainServices.DomServ;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
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

    setup.SwaggerDoc("v1", new OpenApiInfo 
    { 
        Version = "v1",
        Title= "Sistema de Patrullaje Aéreo y Terrestre de la CFE",
        Description = "ASP.NET Core Web API para el Sistema de Patrullaje",    
    });
    //TODO Eliminar esto posterior a dar resolución de conflicto de operaciones Para eliminar conflictos de operaciones múltiples
  /*  setup.ResolveConflictingActions(apiDescriptions => apiDescriptions.First());
    setup.IgnoreObsoleteActions();
    setup.IgnoreObsoleteProperties();
    setup.CustomSchemaIds(type => type.FullName);*/
});

builder.Services.AddCors(policyBuilder =>
    policyBuilder.AddDefaultPolicy(policy =>
        policy.WithOrigins("*").AllowAnyHeader().AllowAnyMethod())
);

/*builder.Services.AddCors(options =>
{
    options.AddPolicy("MyAllowedOrigins",
        policy =>
        {
            policy.WithOrigins("https://localhost:8081") // Agregar todos los urls permitidos 
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});*/

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
builder.Services.AddScoped<IUsuariosRepo, UsuariosRepository>();
builder.Services.AddScoped<IUsuariosService, UsuariosService>();
builder.Services.AddScoped<IEstructurasRepo, EstructurasRepository>();
builder.Services.AddScoped<IEstructurasService, EstructurasService>();
builder.Services.AddScoped<IUsuariosConfiguradorQuery, UsuariosRepository>(); // para consultas de usuario configurador
builder.Services.AddScoped<IEvidenciasRepo, EvidenciasRepository>();
builder.Services.AddScoped<IEvidenciasService, EvidenciasService>();
builder.Services.AddScoped<IVehiculosPatrullajeRepo, VehiculoPatrullajeRepository>();
builder.Services.AddScoped<IVehiculoService, VehiculoService>();
builder.Services.AddScoped<IUsoVehiculoRepo, UsoVehiculoRepository>();
builder.Services.AddScoped<IUsoVehiculoService, UsoVehiculoService>();
builder.Services.AddScoped<IDocumentosRepo, DocumentosRepository>();
builder.Services.AddScoped<IDocumentoService, DocumentosService>();
builder.Services.AddScoped<IIncidenciasRepo, IncidenciasRepository>();
builder.Services.AddScoped<IIncidenciasService, IncidenciasService>();
builder.Services.AddScoped<IItinerariosRepo, ItinerarioRepository>();
builder.Services.AddScoped<IItinerariosService, ItinerariosService>();
builder.Services.AddScoped<IPersonalParticipanteRepo, PersonalParticipanteRepository>();
builder.Services.AddScoped<IPersonalParticipanteService, PersonalParticipanteService>();
builder.Services.AddScoped<IProgramaMensualRepo, ProgramaMensualRepository>();
builder.Services.AddScoped<IProgramaMensualService, ProgramaMensualService>();
builder.Services.AddScoped<ILineasRepo, LineasRepository>();
builder.Services.AddScoped<ILineasService, LineasService>();
builder.Services.AddScoped<IAfectacionesRepo, AfectacionIncidenciaRepository>();
builder.Services.AddScoped<IAfectacionesService, AfectacionesService>();
builder.Services.AddScoped<IPermisosConduccionRepo, PermisosEdicionConduccionRepository>();
builder.Services.AddScoped<IPermisoEdicionConduccionService, PermisosEdicionConduccionService>();
builder.Services.AddScoped<IEstadisticasRepo, EstadisticasRepository>();
builder.Services.AddScoped<IEstadisticasService, EstadisticasService>();

builder.Services.AddDbContext<PatrullajeContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<ProgramaContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<RutaContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<TarjetaInformativaContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<CatalogosConsultaContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<UsuariosContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<EstructurasContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<EvidenciasContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<VehiculosPatrullajeContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<UsoVehiculoContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<DocumentosContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<IncidenciaContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<ItinerarioContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<PersonalParticipanteContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<ProgramaMensualContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<LineasContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<AfectacionIncidenciasContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<PermisosEdicionConduccionContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));
builder.Services.AddDbContext<EstadisticasContext>(dbCtxtOptions => dbCtxtOptions.UseSqlServer(builder.Configuration["ConnectionStrings:SsfInfoDB"]));

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

app.UseCors();

//app.UseCors("MyAllowedOrigins");
//app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
