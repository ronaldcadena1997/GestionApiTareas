using GestionApiTareas.Entities;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
//using GestionApiTareas.Entities;
//using GestionApiTareas.Entities.DinamicQuestionEntities;
using static Microsoft.ApplicationInsights.MetricDimensionNames.TelemetryContext;

namespace GestionApiTareas
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        private readonly IConfiguration _configuration;

        public ApplicationDbContext()
        {
        }

        public ApplicationDbContext(DbContextOptions options) : base(options)
        {
        }


        //public virtual DbSet<DetallesTipoPedido> DetallesTipoPedido { get; set; }
        //public virtual DbSet<Ciudades> Ciudades { get; set; }
        //public virtual DbSet<Empresas> Empresas { get; set; }
        //public virtual DbSet<Provincias> Provincias { get; set; }
        //public virtual DbSet<Sucursales> Sucursales { get; set; }
        //public virtual DbSet<Anio> Anios { get; set; }
        //public virtual DbSet<Color> Colores { get; set; }

        //public virtual DbSet<Chasis> Chasis { get; set; }
        //public virtual DbSet<Choferes> Choferes { get; set; }
        //public virtual DbSet<DiseniosLlantas> DiseniosLlantas { get; set; }
        //public virtual DbSet<EjesLlantas> EjesLlantas { get; set; }
        //public virtual DbSet<Llantas> Llantas { get; set; }
        //public virtual DbSet<LlantasInspecciones> LlantasInspecciones { get; set; }
        //public virtual DbSet<PosicionLlantas> PosicionLlantas { get; set; }
        //public virtual DbSet<MarcasLlantas> MarcasLlantas { get; set; }
        //public virtual DbSet<MarcasCabezales> MarcasCabezales { get; set; }
        //public virtual DbSet<MarcasChasis> MarcasChasis { get; set; }
        //public virtual DbSet<MarcasGeneradores> MarcasGeneradores { get; set; }
        //public virtual DbSet<MedidasLlantas> MedidasLlantas { get; set; }
        //public virtual DbSet<ModelosCabezales> ModelosCabezales { get; set; }
        //public virtual DbSet<Vehiculos> Vehiculos { get; set; }
        //public virtual DbSet<Cabezales> Cabezales { get; set; }
        //public virtual DbSet<ConfiguracionParametros> ConfiguracionParametros { get; set; }
        //public virtual DbSet<TipoEstructura> TipoEstructura { get; set; }
        //public virtual DbSet<Generadores> Generadores { get; set; }
        //public virtual DbSet<EstadosVehiculos> EstadosVehiculos { get; set; }
        //public virtual DbSet<EstadosLlantas> EstadosLlantas { get; set; }
        //public virtual DbSet<EstadosInspecciones> EstadosInspecciones { get; set; }
        //public virtual DbSet<MontajeLLantas> MontajeLLantas { get; set; }
        //public virtual DbSet<TiposVehiculos> TiposVehiculos { get; set; }
        //public virtual DbSet<TiposLlantas> TiposLlantas { get; set; }
        //public virtual DbSet<HistoricoInspeccionLlantas> HistoricoInspeccionLlantas { get; set; }
        //public virtual DbSet<HistoricoLlantas> HistoricoLlantas { get; set; }
        //public virtual DbSet<ReencaucheLlantas> ReencaucheLlantas { get; set; }
        //public virtual DbSet<Paises> Paises { get; set; }
        //public virtual DbSet<TipoPedido> TipoPedido { get; set; }
        //public virtual DbSet<TipoSolicitudes> TipoSolicitudes { get; set; }

        //public virtual DbSet<CreacionPedido> CreacionPedido { get; set; }

        //public virtual DbSet<SolicitudPedido> SolicitudPedido { get; set; }

        ////public virtual DbSet<CabeceraMontaje> CabeceraMontaje { get; set; }
        ////public virtual DbSet<HistoricoKilometrajeVehiculo> HistoricoKilometrajeVehiculo { get; set; }

        //public virtual DbSet<PosicionesLLantasPedidos> PosicionesLLantasPedidos { get; set; }

        ////public virtual DbSet<Pedidos> Pedidos { get; set; }

        //public virtual DbSet<ArchivoTMS> ArchivoTMS { get; set; }

        //public virtual DbSet<PedidosImagenes> PedidosImagenes { get; set; }

        //public virtual DbSet<PreguntaInspecciones> PreguntaInspecciones { get; set; }

        //public virtual DbSet<itemsDanioLlantas> itemsDanioLlantas { get; set; }
        public virtual DbSet<Pantallas> Pantallas { get; set; }
        public virtual DbSet<TaskEstudiantes> TaskEstudiantes { get; set; }
        public virtual DbSet<PermisosPantalla> PermisosPantalla { get; set; }

        //public virtual DbSet<ItemSeccionEstrucutra> ItemSeccionEstrucutra { get; set; }


        //public virtual DbSet<ItemAcciones> ItemAcciones { get; set; }

        



        #region INSPECCIONES

        //public virtual DbSet<TipoInspeccion> TipoInspeccion { get; set; }

        //public virtual DbSet<ItemInspeccion> ItemInspeccion { get; set; }

        //public virtual DbSet<Inspeccion> Inspeccion { get; set; }
        //public virtual DbSet<StatusInspeccion> StatusInspeccion { get; set; }

        //public virtual DbSet<InspeccionDetalle> InspeccionDetalle { get; set; }
        //public virtual DbSet<UserFireBaseToken> UserFireBaseToken { get; set; }
        //public virtual DbSet<VersionAppGMA> VersionAppGMA { get; set; }
        //public virtual DbSet<TipoNovedades> TipoNovedades { get; set; }

        //public virtual DbSet<VersionAppConductor> VersionAppConductor { get; set; }


        public virtual DbSet<RolesUsers> RolesUsers { get; set; }

        #endregion

        //#region DinamicQuestion

        //public DbSet<Form> Forms { get; set; }
        //public DbSet<Section> Sections { get; set; }
        //public DbSet<Question> Questions { get; set; }
        //public DbSet<FormSectionQuestion> FormSectionQuestions { get; set; }
        //public DbSet<QuestionType> QuestionTypes { get; set; }
        //public DbSet<Item> Items { get; set; }
        //public DbSet<Catalog> Catalogs { get; set; }
        //public DbSet<CatalogItem> CatalogItems { get; set; }

        //#endregion DinamicQuestion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<ApplicationUser>().ToTable("Users", "SEG");
            modelBuilder.Entity<IdentityUserToken<string>>().ToTable("UsersToken", "SEG");
            modelBuilder.Entity<IdentityRole>().ToTable("Role", "SEG");
            modelBuilder.Entity<IdentityRoleClaim<string>>().ToTable("RoleClaim", "SEG");
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UserRole", "SEG");
            modelBuilder.Entity<IdentityUserClaim<string>>().ToTable("UserClaim", "SEG");
            modelBuilder.Entity<IdentityUserLogin<string>>().ToTable("UserLogin", "SEG");



            //PERMISOS PANTALLA 

            modelBuilder.Entity<PermisosPantalla>()
                .HasOne(x => x.Pantalla)
                .WithMany()
                .HasForeignKey(x => x.idPantalla)
                .OnDelete(DeleteBehavior.ClientCascade);
            modelBuilder.Entity<PermisosPantalla>()
                           .HasOne(x => x.SeccionPantalla)
                           .WithMany()
                           .HasForeignKey(x => x.idSeccionPantalla)
                           .OnDelete(DeleteBehavior.ClientCascade);

            modelBuilder.Entity<ApplicationUser>()
.HasOne(x => x.RolesUsuarios)
.WithMany()
.HasForeignKey(x => x.IdRole)
.OnDelete(DeleteBehavior.ClientCascade);

 
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
       => optionsBuilder
            .UseLazyLoadingProxies()
       .UseSqlServer("Server=localhost;Database=Gestion;Trusted_Connection=True;integrated security=True;MultipleActiveResultSets=True;TrustServerCertificate=True;Encrypt=False");
       //   .UseSqlServer("Server = localhost; Database=QA_GMA_ECU;Trusted_Connection=True;integrated security = False; user id = tms; password=Kl0pp0976@;MultipleActiveResultSets=True;TrustServerCertificate=True;Encrypt=False");
    }
}