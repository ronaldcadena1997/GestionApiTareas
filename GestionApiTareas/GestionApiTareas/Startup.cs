using Amazon.Runtime;
using Amazon.S3;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Serilog;
using Serilog.Events;
using System.IdentityModel.Tokens.Jwt;
using System.Reflection;
using System.Text;
using System.Text.Json.Serialization;
using GestionApiTareas.Entities;
using GestionApiTareas.Filtros;
using GestionApiTareas.Middlewares;
using GestionApiTareas.Servicios;
using GestionApiTareas.Servicios.Interfaces;
using GestionApiTareas.Utilidades;

[assembly: ApiConventionType(typeof(DefaultApiConventions))]

namespace GestionApiTareas
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            string projectName = Assembly.GetEntryAssembly().GetName().Name;

            services.AddControllers(opciones =>
            {
                //Log para captar todos los exeptions no capturados
                opciones.Filters.Add(typeof(ExceptionFilter));
                //opciones.Conventions.Add(new SwaggerAgrupaPorVersion());
            }).AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles).AddNewtonsoftJson();

            Log.Logger = new LoggerConfiguration()
               .MinimumLevel.Override("Microsoft", LogEventLevel.Fatal)
               .WriteTo.File($"logs/{projectName}-.txt", rollingInterval: RollingInterval.Day, outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}")
               .CreateLogger();

            services.AddLogging(loggingBuilder =>
            {
                loggingBuilder.AddSerilog(Log.Logger);
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("defaultConnection"))
            );

            //Autenticacion
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).
                AddJwtBearer(opciones => opciones.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["llavejwt"])),
                    ClockSkew = TimeSpan.Zero
                });

         

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Title = "GestionApiTareas",
                    Version = "v1",
                    Description = "Este es un Proyecto de Gestion de tarea de API REST en .NET Core",
                    Contact = new OpenApiContact
                    {
                        Email = "melany@gmail.com",
                        Name = "Melany",
                        Url = new Uri("https://Melany.blog")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "MIT"
                    },
                });
                //c.SwaggerDoc("v2", new OpenApiInfo
                //{
                //    Title = "WebAPIAutores",
                //    Version = "v2"
                //});
                //c.OperationFilter<AgregarParametroHATEOAS>();
                //c.OperationFilter<AgregarParametroXVersion>();

                c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            }
                        },
                        new string[]{}
                    }
                });

                var archivoXML = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var rutaXML = Path.Combine(AppContext.BaseDirectory, archivoXML);
                c.IncludeXmlComments(rutaXML);
            });

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IExcelTemplateInterface, WebHostServices>();
            services.AddSingleton<IWebHostEnvironment>(provider => (IWebHostEnvironment)provider.GetService<Microsoft.AspNetCore.Hosting.IHostingEnvironment>());
            //services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<ApplicationDbContext>()

            services.AddAuthorization(opciones =>
            {
                opciones.AddPolicy("Administrador", politica => politica.RequireClaim("administrador"));
                //opciones.AddPolicy("EsVendedor", politica => politica.RequireClaim("esVendedor"));
            });

            //services.Configure<IISServerOptions>(options =>
            //{
            //    options.MaxRequestBodySize = 2147483648888888888;
            //});

            services.AddDataProtection();

            services.AddTransient<HashService>();

            var awsOptions = Configuration.GetAWSOptions();
            awsOptions.Credentials = new BasicAWSCredentials(Configuration["AWS:AccessKey"], Configuration["AWS:SecretKey"]);
            services.AddDefaultAWSOptions(awsOptions);


            services.AddAWSService<IAmazonS3>();

            //CORS es relevante para navegadores y proyectos hechos en react, angular, etc, para aplicaciones moviles y de mas no tiene sentido realizarlo
            services.AddCors(opciones =>
            {
                opciones.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins("https://www.apirequest.io").AllowAnyMethod().AllowAnyHeader().WithExposedHeaders(new string[] { "cantidadTotalRegistros" });
                });
            });

            services.AddTransient<GeneradorEnlaces>();
            services.AddTransient<HATEOASAutorFilterAttribute>();
            services.AddSingleton<IActionContextAccessor, ActionContextAccessor>();

            services.AddIdentity<ApplicationUser, IdentityRole>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>().AddDefaultTokenProviders(); ;
            // services.AddApplicationInsightsTelemetry(Configuration["ApplicationInsights:ConnectionStrings"]);
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env, ILogger<Startup> logger)
        {
            //Captar todas las peticiones en logs y en terminal
            //app.UseMiddleware<LoguearRespuestaHTTPMiddleware>();
            app.UseLoguearRespuestaHTTP();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "GestionApiTareas v1");
                //c.SwaggerEndpoint("/swagger/v2/swagger.json", "GestionApiTareas v2");
            });

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseStaticFiles();
            //CORS es relevante para navegadores y proyectos hechos en react, angular, etc, para aplicaciones moviles y de mas no tiene sentido realizarlo
            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}