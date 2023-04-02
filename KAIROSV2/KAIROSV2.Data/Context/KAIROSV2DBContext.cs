using System;
using KAIROSV2.Business.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace KAIROSV2.Data
{
    //TODO Change DbContext by IdentityDbContext<IdentityUser>
    public partial class KAIROSV2DBContext : DbContext
    {
        public KAIROSV2DBContext()
        {
        }

        public KAIROSV2DBContext(DbContextOptions<KAIROSV2DBContext> options)
            : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //optionsBuilder.UseSqlServer("Data Source=BOGSQL000;Initial Catalog=KAIROS2;User ID=kairos2;Password=Appl1c@t10n;");
                optionsBuilder.UseSqlServer("Data Source=localhost;Initial Catalog=KAIROS2;User ID=sa;Password=STMS.2017;");
            }
        }

        public virtual DbSet<TApiCorreccion5b> TApiCorreccion5bSet { get; set; }
        public virtual DbSet<TApiCorreccion6b> TApiCorreccion6bSet { get; set; }
        public virtual DbSet<TApiCorreccion6cAlcohol> TApiCorreccion6cAlcoholSet { get; set; }
        public virtual DbSet<TArea> TAreaSet { get; set; }
        public virtual DbSet<TAtributo> TAtributoSet { get; set; }
        public virtual DbSet<TAtributosGrupo> TAtributosGrupoSet { get; set; }
        public virtual DbSet<TAtributosTipo> TAtributosTipoSet { get; set; }
        public virtual DbSet<TCabezote> TCabezoteSet { get; set; }
        public virtual DbSet<TClasesPermiso> TClasesPermisoSet { get; set; }
        public virtual DbSet<TCompañia> TCompañiaSet { get; set; }
        public virtual DbSet<TCompañiasTransportadora> TCompañiasTransportadoraSet { get; set; }
        public virtual DbSet<TConductor> TConductorSet { get; set; }
        public virtual DbSet<TContador> TContadorSet { get; set; }
        public virtual DbSet<TContadoresTipo> TContadoresTipoSet { get; set; }
        public virtual DbSet<TContadoresEstados> TContadoresEstadosSet { get; set; }
        public virtual DbSet<TDespacho> TDespachoSet { get; set; }
        public virtual DbSet<TDespachosComponente> TDespachoComponentesSet { get; set; }        
        public virtual DbSet<TLinea> TLineaSet { get; set; }
        public virtual DbSet<TLineasEstado> TLineaEstadoSet { get; set; }
        public virtual DbSet<TLog> TLogSet { get; set; }
        public virtual DbSet<TLogPrioridades> TLogPrioridadesSet { get; set; }
        public virtual DbSet<TLogAcciones> TLogAccionesSet { get; set; }
        public virtual DbSet<TProducto> TProductoSet { get; set; }
        public virtual DbSet<TProductosAtributo> TProductosAtributoSet { get; set; }
        public virtual DbSet<TProductosClase> TProductosClaseSet { get; set; }
        public virtual DbSet<TProductosReceta> TProductosRecetaSet { get; set; }
        public virtual DbSet<TProductosRecetasComponente> TProductosRecetasComponenteSet { get; set; }
        public virtual DbSet<TProductosTipo> TProductosTipoSet { get; set; }
        public virtual DbSet<TProveedor> TProveedorSet { get; set; }
        public virtual DbSet<TProveedoresPlanta> TProveedoresPlantaSet { get; set; }
        public virtual DbSet<TProveedoresProducto> TProveedoresProductoSet { get; set; }
        public virtual DbSet<TRecibosBase> TRecibosBaseSet { get; set; }
        public virtual DbSet<TRecibosContador> TRecibosContadorSet { get; set; }
        public virtual DbSet<TRecibosContadorAlcohol> TRecibosContadorAlcoholSet { get; set; }
        public virtual DbSet<TRecibosContadorMezclado> TRecibosContadorMezcladoSet { get; set; }
        public virtual DbSet<TRecibosFacturacion> TRecibosFacturacionSet { get; set; }
        public virtual DbSet<TRecibosMedidaTanque> TRecibosMedidaTanqueSet { get; set; }
        public virtual DbSet<TRecibosMedidaTanqueAgua> TRecibosMedidaTanqueAguaSet { get; set; }
        public virtual DbSet<TRecibosMedidaTanquePantallaFlotante> TRecibosMedidaTanquePantallaFlotanteSet { get; set; }
        public virtual DbSet<TRecibosSalidasDuranteBombeo> TRecibosSalidasDuranteBombeoSet { get; set; }
        public virtual DbSet<TRecibosTipo> TRecibosTipoSet { get; set; }
        public virtual DbSet<TRecibosTransporte> TRecibosTransporteSet { get; set; }
        public virtual DbSet<TRecibosVolumen> TRecibosVolumenSet { get; set; }
        public virtual DbSet<TSoldTo> TSoldToSet { get; set; }
        public virtual DbSet<TShipTo> TShipToSet { get; set; }
        public virtual DbSet<TTASCortes> TTASCortesSet { get; set; }
        public virtual DbSet<TTanque> TTanqueSet { get; set; }
        public virtual DbSet<TTanquesAforo> TTanquesAforoSet { get; set; }
        public virtual DbSet<TTanquesAforoResponsable> TTanquesAforoResponsableSet { get; set; }
        public virtual DbSet<TTanquesEstado> TTanquesEstadoSet { get; set; }
        public virtual DbSet<TTanquesPantallaFlotante> TTanquesPantallaFlotanteSet { get; set; }
        public virtual DbSet<TTerminalCompañia> TTerminalCompañiaSet { get; set; }
        public virtual DbSet<TTerminalContador> TTerminalContadorSet { get; set; }
        public virtual DbSet<TTerminalCompañiasProducto> TTerminalCompañiasProductoSet { get; set; }
        public virtual DbSet<TTerminal> TTerminalSet { get; set; }
        public virtual DbSet<TTerminalesEstado> TTerminalesEstadoSet { get; set; }
        public virtual DbSet<TTerminalesProductosReceta> TTerminalesProductosRecetaSet { get; set; }

        public virtual DbSet<TTrailer> TTrailerSet { get; set; }
        public virtual DbSet<TUPermiso> TUPermisoSet { get; set; }
        public virtual DbSet<TURole> TURoleSet { get; set; }
        public virtual DbSet<TURolesPermiso> TURolesPermisoSet { get; set; }
        public virtual DbSet<TUUsuario> TUUsuarioSet { get; set; }
        public virtual DbSet<TUUsuariosImagen> TUUsuarioImagenSet { get; set; }
        public virtual DbSet<TUUsuariosTerminalCompañia> TUUsuariosTerminalCompañiaSet { get; set; }
        public virtual DbSet<TProcesamientoArchivosMst> TProcesamientoArchivosMstSet { get; set; }
        public virtual DbSet<TProcesamientoArchivosDet> TProcesamientoArchivosDetSet { get; set; }
        public virtual DbSet<VDbColumna> VDbColumnas { get; set; }
        public virtual DbSet<VDbTabla> VDbTablas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<TApiCorreccion5b>(entity =>
            {
                entity.HasKey(e => new { e.ApiObservado, e.Temperatura });

                entity.ToTable("T_API_Correccion_5B");

                entity.Property(e => e.ApiObservado).HasColumnName("API_Observado");

                entity.Property(e => e.ApiCorregido).HasColumnName("API_Corregido");
            });

            modelBuilder.Entity<TApiCorreccion6b>(entity =>
            {
                entity.HasKey(e => new { e.ApiCorregido, e.Temperatura });

                entity.ToTable("T_API_Correccion_6B");

                entity.Property(e => e.ApiCorregido).HasColumnName("API_Corregido");

                entity.Property(e => e.FactorCorreccion).HasColumnName("Factor_Correccion");
            });

            modelBuilder.Entity<TApiCorreccion6cAlcohol>(entity =>
            {
                entity.HasKey(e => new { e.ApiCorregido, e.Temperatura });

                entity.ToTable("T_API_Correccion_6C_Alcohol");

                entity.Property(e => e.ApiCorregido).HasColumnName("API_Corregido");

                entity.Property(e => e.FactorCorreccion).HasColumnName("Factor_Correccion");
            });

            modelBuilder.Entity<TArea>(entity =>
            {
                entity.HasKey(e => e.IdArea);

                entity.ToTable("T_Areas");

                entity.Property(e => e.IdArea)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Area");

                entity.Property(e => e.Area)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TAtributo>(entity =>
            {
                entity.HasKey(e => e.IdAtributo);

                entity.ToTable("T_Atributos");

                entity.Property(e => e.IdAtributo)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Atributo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Restricciones)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.TipoDato).HasColumnName("Tipo_Dato");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.GrupoNavigation)
                   .WithMany(p => p.TAtributos)
                   .HasForeignKey(d => d.Grupo)
                   .OnDelete(DeleteBehavior.ClientSetNull)
                   .HasConstraintName("FK_T_Atributos_T_Atributos_Grupos");

                entity.HasOne(d => d.TipoDatoNavigation)
                    .WithMany(p => p.TAtributos)
                    .HasForeignKey(d => d.TipoDato)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Atributos_T_Atributos_Tipos");
            });

            modelBuilder.Entity<TAtributosGrupo>(entity =>
            {
                entity.HasKey(e => e.IdGrupo);

                entity.ToTable("T_Atributos_Grupos");

                entity.Property(e => e.IdGrupo).HasColumnName("Id_Grupo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<TAtributosTipo>(entity =>
            {
                entity.HasKey(e => e.IdTipo)
                    .HasName("PK_T_Atributos_Tipo");

                entity.ToTable("T_Atributos_Tipos");

                entity.Property(e => e.IdTipo).HasColumnName("Id_Tipo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);
            });
            modelBuilder.Entity<TCabezote>(entity =>
            {
                entity.HasKey(e => e.PlacaCabezote);

                entity.ToTable("T_Cabezotes");

                entity.Property(e => e.PlacaCabezote)
                    .HasMaxLength(10)
                    .HasColumnName("Placa_Cabezote");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TClasesPermiso>(entity =>
            {
                entity.HasKey(e => e.IdClase);

                entity.ToTable("T_Clases_Permiso");

                entity.Property(e => e.IdClase)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Clase");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TCompañia>(entity =>
            {
                entity.HasKey(e => e.IdCompañia);

                entity.ToTable("T_Compañias");

                entity.Property(e => e.IdCompañia)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Compañia");

                entity.Property(e => e.CodigoSicom)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Codigo_SICOM");

                entity.Property(e => e.DistributionChannel)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Distribution_Channel");

                entity.Property(e => e.Division)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.SalesOrganization)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Sales_Organization");

                entity.Property(e => e.SupplierType)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Supplier_Type");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TCompañiasTransportadora>(entity =>
            {
                entity.HasKey(e => e.IdTransportadora)
                    .HasName("PK_T_Compañias_Transportadores");

                entity.ToTable("T_Compañias_Transportadoras");

                entity.Property(e => e.IdTransportadora)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Transportadora");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.NombreTransportadora)
                    .IsRequired()
                    .HasMaxLength(100)
                    .HasColumnName("Nombre_Transportadora");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TConductor>(entity =>
            {
                entity.HasKey(e => e.Cedula);

                entity.ToTable("T_Conductores");

                entity.Property(e => e.Cedula).ValueGeneratedNever();

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TContador>(entity =>
            {
                entity.HasKey(e => e.IdContador);

                entity.ToTable("T_Contadores");

                entity.Property(e => e.IdContador)
                    .HasMaxLength(20)
                    .HasColumnName("Id_Contador");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

            });

            modelBuilder.Entity<TContadoresTipo>(entity =>
            {
                entity.HasKey(e => e.IdTipo);

                entity.ToTable("T_Contadores_Tipo");

                entity.Property(e => e.IdTipo)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Tipo");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ColorHex)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Icono)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");


            });

            modelBuilder.Entity<TContadoresEstados>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("T_Contadores_Estados");

                entity.Property(e => e.IdEstado).HasColumnName("Id_Estado");

                entity.Property(e => e.ColorHex)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.Icono)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TDespacho>(entity =>
            {
                entity.HasKey(e => e.Id_Despacho);

                entity.ToTable("T_Despachos");

                entity.Property(e => e.Id_Despacho)
                    .HasMaxLength(80)
                    .HasColumnName("Id_Despacho");

                entity.Property(e => e.Id_Terminal)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Id_Terminal");

                entity.Property(e => e.Id_Compañia)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Id_Compañia");                

                entity.Property(e => e.No_Orden)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("No_Orden");

                entity.Property(e => e.Id_Producto_Despacho)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("Id_Producto_Despacho");

                entity.Property(e => e.Compartimento)
                .HasColumnName("Compartimento");

                entity.Property(e => e.Fecha_Final_Despacho)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Final_Despacho");

                entity.Property(e => e.Id_Corte)
                .HasColumnName("Id_Corte");

                entity.Property(e => e.Sold_To)
                .HasColumnName("Sold_To");

                entity.Property(e => e.Cedula_Conductor)
                .HasColumnName("Cedula_Conductor");

                entity.Property(e => e.Placa_Cabezote)
                    .HasMaxLength(10)
                    .HasColumnName("Placa_Cabezote");

                entity.Property(e => e.Placa_Trailer)
                    .HasMaxLength(10)
                    .HasColumnName("Placa_Trailer");

                entity.Property(e => e.Volumen_Ordenado)
                .HasColumnName("Volumen_Ordenado");

                entity.Property(e => e.Volumen_Cargado)
                .HasColumnName("Volumen_Cargado");

                entity.Property(e => e.Modo)
                .HasColumnName("Modo");

                entity.Property(e => e.Estado_Kairos)
                .HasColumnName("Estado_Kairos");

                entity.Property(e => e.Observaciones)
                    .HasMaxLength(25)
                    .HasColumnName("Observaciones");

                entity.Property(e => e.Ultima_Edicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.Property(e => e.Editado_Por)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.HasOne(d => d.IdTerminalNavigation)
                    .WithMany(p => p.TDespacho)
                    .HasForeignKey(d => d.Id_Terminal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Despachos_T_Terminales");

                entity.HasOne(d => d.IdModoNavigation)
                    .WithMany(p => p.TDespacho)
                    .HasForeignKey(d => d.Modo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Despachos_T_Despachos_Modos");
            });

            modelBuilder.Entity<TDespachosComponente>(entity =>
            {
                entity.HasKey(e => new { e.No_Orden, e.Ship_To , e.Id_Producto_Componente, e.Compartimento , e.Tanque , e.Contador });

                entity.ToTable("T_Despachos_Componentes");

                entity.Property(e => e.Id_Despacho)
                    .HasMaxLength(80)
                    .HasColumnName("Id_Despacho");

                entity.Property(e => e.No_Orden)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("No_Orden");

                entity.Property(e => e.Ship_To)
                    .IsRequired()                    
                    .HasColumnName("Ship_To");

                entity.Property(e => e.Id_Producto_Componente)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("Id_Producto_Componente");
                
                entity.Property(e => e.Compartimento)
                .HasColumnName("Compartimento");

                entity.Property(e => e.Tanque)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("Tanque");

                entity.Property(e => e.Contador)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("Contador");

                entity.Property(e => e.Volumen_Bruto)
                .HasColumnName("Volumen_Bruto");

                entity.Property(e => e.Volumen_Neto)
                .HasColumnName("Volumen_Neto");

                entity.Property(e => e.Temperatura)
                .HasColumnName("Temperatura");

                entity.Property(e => e.Densidad)
                .HasColumnName("Densidad");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.HasOne(d => d.IdDespachoNavigation)
                    .WithMany(p => p.TDespachosComponentes)
                    .HasForeignKey(d => d.Id_Despacho)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Despachos_Componentes_T_Despachos");
                
            });

            modelBuilder.Entity<TDespachosModos>(entity =>
            {
                entity.HasKey(e => e.Id_Modo);

                entity.ToTable("T_Despachos_Modos");

                entity.Property(e => e.Id_Modo)
                    .HasColumnName("Id_Modo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(50)
                    .HasColumnName("Descripcion");               

            });

            modelBuilder.Entity<TDespachosTAS>(entity =>
            {
                entity.HasKey(e => e.Id_Despacho);

                entity.ToTable("T_Despachos_TAS");

                entity.Property(e => e.Id_Despacho)
                    .HasMaxLength(80)
                    .HasColumnName("Id_Despacho");

                entity.Property(e => e.Id_TAS)
                    .HasColumnName("Id_TAS");

                entity.HasOne(d => d.IdDespachoNavigation)
                    .WithOne(p => p.IdDespachosTASNavigation)
                    .HasForeignKey<TDespacho>(d => d.Id_Despacho)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Despachos_TAS_T_Despachos");

                entity.HasOne(d => d.IdTASNavigation)
                    .WithMany(p => p.TDespachosTAS)
                    .HasForeignKey(d => d.Id_TAS)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Despachos_TAS_T_TAS");

            });


            modelBuilder.Entity<TLinea>(entity =>
            {
                entity.HasKey(e => new { e.IdLinea, e.IdTerminal });

                entity.ToTable("T_Lineas");

                entity.Property(e => e.IdLinea)
                    .HasMaxLength(25)
                    .HasColumnName("Id_Linea");

                entity.Property(e => e.IdTerminal)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Terminal");

                entity.Property(e => e.DensidadAforo).HasColumnName("Densidad_Aforo");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.Id_Estado)
                    .IsRequired()
                    .HasMaxLength(10);


                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.IdProducto)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("Id_Producto");

                entity.Property(e => e.Observaciones).HasMaxLength(254);

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.TLineas)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Lineas_T_Productos");

                entity.HasOne(d => d.IdTerminalNavigation)
                    .WithMany(p => p.TLineas)
                    .HasForeignKey(d => d.IdTerminal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Lineas_T_Terminales");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TLineas)
                    .HasForeignKey(d => d.Id_Estado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Lineas_T_Lineas_Estados");

            });

            modelBuilder.Entity<TLineasEstado>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("T_Lineas_Estados");

                entity.Property(e => e.IdEstado).HasColumnName("Id_Estado");

                entity.Property(e => e.ColorHex)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.Icono)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TLog>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("T_Log");

                entity.Property(e => e.Accion)
                    .IsRequired();

                entity.Property(e => e.Aplicacion)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Area)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Comentario).HasMaxLength(254);

                entity.Property(e => e.Entidad)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.FechaEvento)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Evento");

                entity.Property(e => e.IdUsuario)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Id_Usuario");

                entity.Property(e => e.Objetivo)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.Seccion)
                    .IsRequired()
                    .HasMaxLength(40);

                entity.Property(e => e.Prioridad)
                    .IsRequired();

                entity.Property(e => e.Comentario)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.HasOne(d => d.IdLogAccionesNavigation)
                    .WithMany(p => p.TLogs)
                    .HasForeignKey(d => d.Accion)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Log_T_Log_Acciones");

                entity.HasOne(d => d.IdLogPrioridadesNavigation)
                    .WithMany(p => p.TLogs)
                    .HasForeignKey(d => d.Prioridad)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Log_T_Log_Prioridades");

            });


            modelBuilder.Entity<TLogAcciones>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("T_Log_Acciones");

                entity.Property(e => e.Accion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TLogPrioridades>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.ToTable("T_Log_Prioridades");

                entity.Property(e => e.Prioridad)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TProcesamientoArchivosDet>(entity =>
            {
                entity.HasKey(e => new { e.IdCampo, e.IdMapeo, e.IdTabla })
                    .HasName("PK_T_Procesameinto_Archivos_Det");

                entity.ToTable("T_Procesamiento_Archivos_Det");

                entity.Property(e => e.IdColumna)
                    .HasMaxLength(50)
                    .HasColumnName("Id_Columna");

                entity.Property(e => e.IdMapeo)
                    .HasMaxLength(50)
                    .HasColumnName("Id_Mapeo");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por")
                    .IsFixedLength(true);

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.IdCampo)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IdTabla)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.IndiceColumna).HasColumnName("Indice_Columna");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.IdMapeoNavigation)
                    .WithMany(p => p.ProcesamientoArchivosDets)
                    .HasForeignKey(d => d.IdMapeo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Procesamiento_Archivos_Det_T_Procesamiento_Archivos_Mst");
            });

            modelBuilder.Entity<TProcesamientoArchivosMst>(entity =>
            {
                entity.HasKey(e => e.IdMapeo)
                    .HasName("PK__Mapeo_Ar__2867922431583BD0");

                entity.ToTable("T_Procesamiento_Archivos_Mst");

                entity.Property(e => e.IdMapeo)
                    .HasMaxLength(50)
                    .HasColumnName("Id_Mapeo");

                entity.Property(e => e.Descripcion)
                    .HasMaxLength(200)
                    .IsFixedLength(true);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por")
                    .IsFixedLength(true);

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.NombreArchivo)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Nombre_Archivo");

                entity.Property(e => e.RutaArchivo).HasColumnName("Ruta_Archivo");

                entity.Property(e => e.Separador)
                    .HasMaxLength(1)
                    .IsFixedLength(true);

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });


            modelBuilder.Entity<TProducto>(entity =>
            {
                entity.HasKey(e => e.IdProducto);

                entity.ToTable("T_Productos");

                entity.Property(e => e.IdProducto)
                    .HasMaxLength(40)
                    .HasColumnName("Id_Producto");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.IdClase)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("Id_Clase");

                entity.Property(e => e.IdTipo)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("Id_Tipo");

                entity.Property(e => e.NombreCorto)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Nombre_Corto");

                entity.Property(e => e.NombreErp)
                    .IsRequired()
                    .HasMaxLength(255)
                    .HasColumnName("Nombre_ERP");

                entity.Property(e => e.Sicom)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("SICOM");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.IdClaseNavigation)
                    .WithMany(p => p.TProductos)
                    .HasForeignKey(d => d.IdClase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Productos_T_Productos_Clases");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.TProductos)
                    .HasForeignKey(d => d.IdTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Productos_T_Productos_Tipo");
            });

            modelBuilder.Entity<TProductosAtributo>(entity =>
            {
                entity.HasKey(e => new { e.IdAtributo, e.IdProducto });

                entity.ToTable("T_Productos_Atributos");

                entity.Property(e => e.IdAtributo).HasColumnName("Id_Atributo");

                entity.Property(e => e.IdProducto)
                    .HasMaxLength(40)
                    .HasColumnName("Id_Producto");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Observaciones).HasMaxLength(254);

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.Property(e => e.ValorNumero).HasColumnName("Valor_Numero");

                entity.Property(e => e.ValorTexto)
                    .HasMaxLength(254)
                    .HasColumnName("Valor_Texto");

                entity.HasOne(d => d.IdAtributoNavigation)
                    .WithMany(p => p.TProductosAtributos)
                    .HasForeignKey(d => d.IdAtributo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Productos_Atributos_T_Atributos");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.TProductosAtributos)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Productos_Atributos_T_Productos");
            });

            modelBuilder.Entity<TProductosClase>(entity =>
            {
                entity.HasKey(e => e.IdClase);

                entity.ToTable("T_Productos_Clases");

                entity.Property(e => e.IdClase)
                    .HasMaxLength(40)
                    .HasColumnName("Id_Clase");
                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(10);
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");
                entity.Property(e => e.Icono)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TProductosReceta>(entity =>
            {
                entity.HasKey(e => new { e.IdReceta, e.IdProducto });

                entity.ToTable("T_Productos_Recetas");

                entity.Property(e => e.IdReceta)
                    .HasMaxLength(50)
                    .HasColumnName("Id_Receta");

                entity.Property(e => e.IdProducto)
                    .HasMaxLength(40)
                    .HasColumnName("Id_Producto");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.TProductosReceta)
                    .HasForeignKey(d => d.IdProducto)
                    .HasConstraintName("FK_T_Productos_Recetas_T_Productos");
            });

            modelBuilder.Entity<TProductosRecetasComponente>(entity =>
            {
                entity.HasKey(e => new { e.IdReceta, e.IdProducto, e.Posicion });

                entity.ToTable("T_Productos_Recetas_Componentes");

                entity.Property(e => e.IdReceta)
                    .HasMaxLength(50)
                    .HasColumnName("Id_Receta");

                entity.Property(e => e.IdProducto)
                    .HasMaxLength(40)
                    .HasColumnName("Id_Producto");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");
                entity.Property(e => e.IdComponente)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("Id_Componente");

                entity.Property(e => e.ProporcionComponente).HasColumnName("Proporcion_Componente");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.IdComponenteNavigation)
                    .WithMany(p => p.TProductosRecetasComponentes)
                    .HasForeignKey(d => d.IdComponente)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Productos_Recetas_Componentes_T_Productos_Componente");

                entity.HasOne(d => d.Id)
                     .WithMany(p => p.TProductosRecetasComponentes)
                     .HasForeignKey(d => new { d.IdReceta, d.IdProducto })
                     .HasConstraintName("FK_T_Productos_Recetas_Componentes_T_Productos_Recetas");
            });

            modelBuilder.Entity<TProductosTipo>(entity =>
            {
                entity.HasKey(e => e.IdTipo);

                entity.ToTable("T_Productos_Tipo");

                entity.Property(e => e.IdTipo)
                    .HasMaxLength(40)
                    .HasColumnName("Id_Tipo");
                entity.Property(e => e.Color)
                    .IsRequired()
                    .HasMaxLength(10);
                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");
                entity.Property(e => e.Icono)
                    .IsRequired()
                    .HasMaxLength(20);
                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TProveedor>(entity =>
            {
                entity.HasKey(e => e.IdProveedor);

                entity.ToTable("T_Proveedores");

                entity.Property(e => e.IdProveedor)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Proveedor");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.NombreProveedor)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("Nombre_Proveedor");

                entity.Property(e => e.SicomProveedor)
                    .IsRequired()
                    .HasMaxLength(125)
                    .HasColumnName("SICOM_Proveedor");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TProveedoresPlanta>(entity =>
            {
                entity.HasKey(e => new { e.IdProveedor, e.PlantaProveedor });

                entity.ToTable("T_Proveedores_Plantas");

                entity.Property(e => e.IdProveedor)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Proveedor");

                entity.Property(e => e.PlantaProveedor)
                    .HasMaxLength(50)
                    .HasColumnName("Planta_Proveedor");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.SicomPlantaProveedor)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("SICOM_Planta_Proveedor");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.TProveedoresPlanta)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_T_Proveedores_Terminales_T_Proveedores");
            });

            modelBuilder.Entity<TProveedoresProducto>(entity =>
            {
                entity.HasKey(e => new { e.IdProveedor, e.IdTipoProducto });

                entity.ToTable("T_Proveedores_Productos");

                entity.Property(e => e.IdProveedor)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Proveedor");

                entity.Property(e => e.IdTipoProducto)
                    .HasMaxLength(40)
                    .HasColumnName("Id_Tipo_Producto");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_id")
                    .ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.TProveedoresProductos)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_T_Proveedores_Productos_T_Proveedores");

                entity.HasOne(d => d.IdTipoProductoNavigation)
                    .WithMany(p => p.TProveedoresProductos)
                    .HasForeignKey(d => d.IdTipoProducto)
                    .OnDelete(DeleteBehavior.Cascade)
                    .HasConstraintName("FK_T_Proveedores_Productos_T_Productos_Tipo");
            });

            modelBuilder.Entity<TRecibosBase>(entity =>
            {
                entity.HasKey(e => e.IdRecibo);

                entity.ToTable("T_Recibos_Base");

                entity.Property(e => e.IdRecibo).HasColumnName("Id_Recibo");

                entity.Property(e => e.DatosTransporte).HasColumnName("Datos_Transporte");

                entity.Property(e => e.DocumentoRecibo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("Documento_Recibo");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FechaRecibo)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Recibo");

                entity.Property(e => e.IdProducto)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("Id_Producto");

                entity.Property(e => e.IdTanque)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("Id_Tanque");

                entity.Property(e => e.IdTerminal)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Id_Terminal");

                entity.Property(e => e.IdTipo).HasColumnName("Id_Tipo");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.TRecibosBases)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Base_T_Productos");

                entity.HasOne(d => d.IdTerminalNavigation)
                    .WithMany(p => p.TRecibosBases)
                    .HasForeignKey(d => d.IdTerminal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Base_T_Terminales");

                entity.HasOne(d => d.IdTipoNavigation)
                    .WithMany(p => p.TRecibosBases)
                    .HasForeignKey(d => d.IdTipo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Base_T_Recibos_Tipo");

                entity.HasOne(d => d.IdT)
                    .WithMany(p => p.TRecibosBases)
                    .HasForeignKey(d => new { d.IdTanque, d.IdTerminal })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Base_T_Tanques");
            });

            modelBuilder.Entity<TRecibosContador>(entity =>
            {
                entity.HasKey(e => e.IdRecibo);

                entity.ToTable("T_Recibos_Contador");

                entity.Property(e => e.IdRecibo)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Recibo");

                entity.Property(e => e.DensidadMuestra).HasColumnName("Densidad_Muestra");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FactorCorreccion).HasColumnName("Factor_Correccion");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.IdCompañia)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Id_Compañia");

                entity.Property(e => e.IdContador)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Id_Contador");

                entity.Property(e => e.IdProducto)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("Id_Producto");

                entity.Property(e => e.PorcentajeVariacionTransito).HasColumnName("Porcentaje_Variacion_Transito");

                entity.Property(e => e.Shipment).HasMaxLength(20);

                entity.Property(e => e.TemperaturaPromedioMuestra).HasColumnName("Temperatura_Promedio_Muestra");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.Property(e => e.VolumenContadorBruto).HasColumnName("Volumen_Contador_Bruto");

                entity.Property(e => e.VolumenContadorNeto).HasColumnName("Volumen_Contador_Neto");

                entity.Property(e => e.VolumenDespachado).HasColumnName("Volumen_Despachado");

                entity.Property(e => e.VolumenOrdenado).HasColumnName("Volumen_Ordenado");

                entity.Property(e => e.VolumenVariacionTransito).HasColumnName("Volumen_Variacion_Transito");

                entity.HasOne(d => d.IdCompañiaNavigation)
                    .WithMany(p => p.TRecibosContadors)
                    .HasForeignKey(d => d.IdCompañia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Contador_T_Compañias");

                entity.HasOne(d => d.IdContadorNavigation)
                    .WithMany(p => p.TRecibosContadors)
                    .HasForeignKey(d => d.IdContador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Contador_T_Contadores");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.TRecibosContador)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Contador_T_Productos");

                entity.HasOne(d => d.IdReciboNavigation)
                    .WithOne(p => p.TRecibosContador)
                    .HasForeignKey<TRecibosContador>(d => d.IdRecibo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Contador_T_Recibos_Base");
            });

            modelBuilder.Entity<TRecibosContadorAlcohol>(entity =>
            {
                entity.HasKey(e => e.IdRecibo);

                entity.ToTable("T_Recibos_Contador_Alcohol");

                entity.Property(e => e.IdRecibo)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Recibo");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.PorcentajeAgua).HasColumnName("Porcentaje_Agua");

                entity.Property(e => e.PorcentajeAlcohol).HasColumnName("Porcentaje_Alcohol");

                entity.Property(e => e.PorcentajeDesnaturalizante).HasColumnName("Porcentaje_Desnaturalizante");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.IdReciboNavigation)
                    .WithOne(p => p.TRecibosContadorAlcohol)
                    .HasForeignKey<TRecibosContadorAlcohol>(d => d.IdRecibo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Contador_Alcohol_T_Recibos_Contador");
            });

            modelBuilder.Entity<TRecibosContadorMezclado>(entity =>
            {
                entity.HasKey(e => e.IdRecibo);

                entity.ToTable("T_Recibos_Contador_Mezclados");

                entity.Property(e => e.IdRecibo)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Recibo");

                entity.Property(e => e.ContadorMezclaFinal).HasColumnName("Contador_Mezcla_Final");

                entity.Property(e => e.ContadorMezclaInicial).HasColumnName("Contador_Mezcla_Inicial");

                entity.Property(e => e.DensidadMuestra).HasColumnName("Densidad_Muestra");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FactorCorreccion).HasColumnName("Factor_Correccion");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.IdContadorMezcla)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Id_Contador_Mezcla");

                entity.Property(e => e.IdProductoFinal)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("Id_Producto_Final");

                entity.Property(e => e.IdProductoMezcla)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("Id_Producto_Mezcla");

                entity.Property(e => e.TemperaturaPromedioMuestra).HasColumnName("Temperatura_Promedio_Muestra");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.Property(e => e.VolumenContadorMezclaBruto).HasColumnName("Volumen_Contador_Mezcla_Bruto");

                entity.Property(e => e.VolumenContadorMezclaNeto).HasColumnName("Volumen_Contador_Mezcla_Neto");

                entity.HasOne(d => d.IdContadorMezclaNavigation)
                    .WithMany(p => p.TRecibosContadorMezclados)
                    .HasForeignKey(d => d.IdContadorMezcla)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Contador_Mezclados_T_Contadores");

                entity.HasOne(d => d.IdProductoMezclaNavigation)
                    .WithMany(p => p.TRecibosContadorMezclados)
                    .HasForeignKey(d => d.IdProductoMezcla)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Contador_Mezclados_T_Productos");

                entity.HasOne(d => d.IdReciboNavigation)
                    .WithOne(p => p.TRecibosContadorMezclado)
                    .HasForeignKey<TRecibosContadorMezclado>(d => d.IdRecibo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Contador_Mezclados_T_Recibos_Contador");
            });

            modelBuilder.Entity<TRecibosFacturacion>(entity =>
            {
                entity.HasKey(e => e.IdRecibo);

                entity.ToTable("T_Recibos_Facturacion");

                entity.Property(e => e.IdRecibo)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Recibo");

                entity.Property(e => e.DensidadRecibo).HasColumnName("Densidad_Recibo");

                entity.Property(e => e.DocumentoProveedor)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Documento_Proveedor");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.IdCompañiaFacturar)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Id_Compañia_Facturar");

                entity.Property(e => e.IdProveedor)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Id_Proveedor");

                entity.Property(e => e.Observaciones).HasMaxLength(254);

                entity.Property(e => e.TemperaturaRecibo).HasColumnName("Temperatura_Recibo");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.Property(e => e.VariacionTransitoBruta).HasColumnName("Variacion_Transito_Bruta");

                entity.Property(e => e.VariacionTransitoNeta).HasColumnName("Variacion_Transito_Neta");

                entity.Property(e => e.VolumenFacturadoBruto).HasColumnName("Volumen_Facturado_Bruto");

                entity.Property(e => e.VolumenFacturadoBrutoBarriles).HasColumnName("Volumen_Facturado_Bruto_Barriles");

                entity.Property(e => e.VolumenFacturadoNeto).HasColumnName("Volumen_Facturado_Neto");

                entity.HasOne(d => d.IdCompañiaFacturarNavigation)
                    .WithMany(p => p.TRecibosFacturacions)
                    .HasForeignKey(d => d.IdCompañiaFacturar)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Facturacion_T_Compañias");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.TRecibosFacturacions)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Facturacion_T_Proveedores");

                entity.HasOne(d => d.IdReciboNavigation)
                    .WithOne(p => p.TRecibosFacturacion)
                    .HasForeignKey<TRecibosFacturacion>(d => d.IdRecibo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Facturacion_T_Recibos_Base");
            });

            modelBuilder.Entity<TRecibosMedidaTanque>(entity =>
            {
                entity.HasKey(e => e.IdRecibo);

                entity.ToTable("T_Recibos_Medida_Tanque");

                entity.Property(e => e.IdRecibo)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Recibo");

                entity.Property(e => e.DensidadFinal).HasColumnName("Densidad_Final");

                entity.Property(e => e.DensidadInicial).HasColumnName("Densidad_Inicial");

                entity.Property(e => e.DocumentoRecibo)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("Documento_Recibo");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FactorCoreccionFinal).HasColumnName("Factor_Coreccion_Final");

                entity.Property(e => e.FactorCorreccionInicial).HasColumnName("Factor_Correccion_Inicial");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.H1FinalProducto).HasColumnName("H1_Final_Producto");

                entity.Property(e => e.H1InicialProducto).HasColumnName("H1_Inicial_Producto");

                entity.Property(e => e.H2FinalProducto).HasColumnName("H2_Final_Producto");

                entity.Property(e => e.H2InicialProducto).HasColumnName("H2_Inicial_Producto");

                entity.Property(e => e.H3FinalProducto).HasColumnName("H3_Final_Producto");

                entity.Property(e => e.H3InicialProducto).HasColumnName("H3_Inicial_Producto");

                entity.Property(e => e.HFinalProductoUnificado).HasColumnName("H_Final_Producto_Unificado");

                entity.Property(e => e.HInicialProductoUnificado).HasColumnName("H_Inicial_Producto_Unificado");

                entity.Property(e => e.Observaciones).HasMaxLength(254);

                entity.Property(e => e.PantallaFlotante).HasColumnName("Pantalla_Flotante");

                entity.Property(e => e.PorcentajeVariacionTransito).HasColumnName("Porcentaje_Variacion_Transito");

                entity.Property(e => e.ReciboConAgua).HasColumnName("Recibo_con_Agua");

                entity.Property(e => e.SalidaDuranteRecibo).HasColumnName("Salida_Durante_Recibo");

                entity.Property(e => e.SistemaProveedor)
                    .IsRequired()
                    .HasMaxLength(150)
                    .HasColumnName("Sistema_Proveedor");

                entity.Property(e => e.TemperaturaPromedioFinal).HasColumnName("Temperatura_Promedio_Final");

                entity.Property(e => e.TemperaturaPromedioInicial).HasColumnName("Temperatura_Promedio_Inicial");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.Property(e => e.VolumenBrutoFinal).HasColumnName("Volumen_Bruto_Final");

                entity.Property(e => e.VolumenBrutoInicial).HasColumnName("Volumen_Bruto_Inicial");

                entity.Property(e => e.VolumenBrutoTotal).HasColumnName("Volumen_Bruto_Total");

                entity.Property(e => e.VolumenNetoFinal).HasColumnName("Volumen_Neto_Final");

                entity.Property(e => e.VolumenNetoInicial).HasColumnName("Volumen_Neto_Inicial");

                entity.Property(e => e.VolumenNetoTotal).HasColumnName("Volumen_Neto_Total");

                entity.Property(e => e.VolumenVariacionTransito).HasColumnName("Volumen_Variacion_Transito");

                entity.HasOne(d => d.IdReciboNavigation)
                    .WithOne(p => p.TRecibosMedidaTanque)
                    .HasForeignKey<TRecibosMedidaTanque>(d => d.IdRecibo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Medida_Tanque_T_Recibos_Base");
            });

            modelBuilder.Entity<TRecibosMedidaTanqueAgua>(entity =>
            {
                entity.HasKey(e => e.IdRecibo);

                entity.ToTable("T_Recibos_Medida_Tanque_Agua");

                entity.Property(e => e.IdRecibo)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Recibo");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.H1FinalAgua).HasColumnName("H1_Final_Agua");

                entity.Property(e => e.H1InicialAgua).HasColumnName("H1_Inicial_Agua");

                entity.Property(e => e.H2FinalAgua).HasColumnName("H2_Final_Agua");

                entity.Property(e => e.H2InicialAgua).HasColumnName("H2_Inicial_Agua");

                entity.Property(e => e.H3FinalAgua).HasColumnName("H3_Final_Agua");

                entity.Property(e => e.H3InicialAgua).HasColumnName("H3_Inicial_Agua");

                entity.Property(e => e.HFinalAguaUnificado).HasColumnName("H_Final_Agua_Unificado");

                entity.Property(e => e.HInicialAguaUnificado).HasColumnName("H_Inicial_Agua_Unificado");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.Property(e => e.VolumenBrutoAgua).HasColumnName("Volumen_Bruto_Agua");

                entity.Property(e => e.VolumenFinalBrutoAgua).HasColumnName("Volumen_Final_Bruto_Agua");

                entity.Property(e => e.VolumenFinalBrutoSinAgua).HasColumnName("Volumen_Final_Bruto_SinAgua");

                entity.Property(e => e.VolumenInicialBrutoAgua).HasColumnName("Volumen_Inicial_Bruto_Agua");

                entity.Property(e => e.VolumenInicialBrutoSinAgua).HasColumnName("Volumen_Inicial_Bruto_SinAgua");

                entity.Property(e => e.VolumenTotalBrutoSinAgua).HasColumnName("Volumen_Total_Bruto_SinAgua");

                entity.HasOne(d => d.IdReciboNavigation)
                    .WithOne(p => p.TRecibosMedidaTanqueAgua)
                    .HasForeignKey<TRecibosMedidaTanqueAgua>(d => d.IdRecibo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Medida_Tanque_Agua_T_Recibos_Medida_Tanque");
            });

            modelBuilder.Entity<TRecibosMedidaTanquePantallaFlotante>(entity =>
            {
                entity.HasKey(e => e.IdRecibo);

                entity.ToTable("T_Recibos_Medida_Tanque_Pantalla_Flotante");

                entity.Property(e => e.IdRecibo)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Recibo");

                entity.Property(e => e.DensidadObservadaFinal).HasColumnName("Densidad_Observada_Final");

                entity.Property(e => e.DensidadObservadaInicial).HasColumnName("Densidad_Observada_Inicial");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.Property(e => e.VolumenBrutoEfectoPantallaFinal).HasColumnName("Volumen_Bruto_Efecto_Pantalla_Final");

                entity.Property(e => e.VolumenBrutoEfectoPantallaInicial).HasColumnName("Volumen_Bruto_Efecto_Pantalla_Inicial");

                entity.HasOne(d => d.IdReciboNavigation)
                    .WithOne(p => p.TRecibosMedidaTanquePantallaFlotante)
                    .HasForeignKey<TRecibosMedidaTanquePantallaFlotante>(d => d.IdRecibo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Medida_Tanque_Pantalla_Flotante_T_Recibos_Medida_Tanque");
            });

            modelBuilder.Entity<TRecibosSalidasDuranteBombeo>(entity =>
            {
                entity.HasKey(e => e.IdRecibo);

                entity.ToTable("T_Recibos_Salidas_Durante_Bombeo");

                entity.Property(e => e.IdRecibo)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Recibo");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FactorCorreccion).HasColumnName("Factor_Correccion");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.IdContador)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("Id_Contador");

                entity.Property(e => e.Observaciones).HasMaxLength(254);

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.Property(e => e.VolumenSalidaBruto).HasColumnName("Volumen_Salida_Bruto");

                entity.Property(e => e.VolumenSalidaNeto).HasColumnName("Volumen_Salida_Neto");

                entity.HasOne(d => d.IdReciboNavigation)
                    .WithOne(p => p.TRecibosSalidasDuranteBombeo)
                    .HasForeignKey<TRecibosSalidasDuranteBombeo>(d => d.IdRecibo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Salidas_Durante_Bombeo_T_Recibos_Medida_Tanque");
            });

            modelBuilder.Entity<TRecibosTipo>(entity =>
            {
                entity.HasKey(e => e.IdTipo);

                entity.ToTable("T_Recibos_Tipo");

                entity.Property(e => e.IdTipo)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Tipo");

                entity.Property(e => e.ColorHex)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.Icono)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TRecibosTransporte>(entity =>
            {
                entity.HasKey(e => e.IdRecibo);

                entity.ToTable("T_Recibos_Transporte");

                entity.Property(e => e.IdRecibo)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Recibo");

                entity.Property(e => e.CedulaConductor)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Cedula_Conductor");

                entity.Property(e => e.CodigoAutorizacionSicom)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Codigo_Autorizacion_SICOM");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FechaEntrada)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Entrada");

                entity.Property(e => e.FechaGuia)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Guia");

                entity.Property(e => e.FechaSalida)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Salida");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.IdProveedor)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Id_Proveedor");

                entity.Property(e => e.IdTransportadora)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Transportadora");

                entity.Property(e => e.NoGuia)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("No_Guia");

                entity.Property(e => e.PlacaCabezote)
                    .IsRequired()
                    .HasMaxLength(6)
                    .HasColumnName("Placa_Cabezote");

                entity.Property(e => e.PlacaTrailer)
                    .IsRequired()
                    .HasMaxLength(6)
                    .HasColumnName("Placa_Trailer");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.IdProveedorNavigation)
                    .WithMany(p => p.TRecibosTransportes)
                    .HasForeignKey(d => d.IdProveedor)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Transporte_T_Proveedores");

                entity.HasOne(d => d.IdReciboNavigation)
                    .WithOne(p => p.TRecibosTransporte)
                    .HasForeignKey<TRecibosTransporte>(d => d.IdRecibo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Transporte_T_Recibos_Base");

                entity.HasOne(d => d.IdTransportadoraNavigation)
                    .WithMany(p => p.TRecibosTransportes)
                    .HasForeignKey(d => d.IdTransportadora)
                    .HasConstraintName("FK_T_Recibos_Transporte_T_Compañias_Transportadoras");
            });

            modelBuilder.Entity<TRecibosVolumen>(entity =>
            {
                entity.HasKey(e => e.IdRecibo);

                entity.ToTable("T_Recibos_Volumen");

                entity.Property(e => e.IdRecibo)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Recibo");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.IdContador)
                    .IsRequired()
                    .HasMaxLength(20)
                    .HasColumnName("Id_Contador");

                entity.Property(e => e.PlacaTrailer)
                    .IsRequired()
                    .HasMaxLength(6)
                    .HasColumnName("Placa_Trailer");

                entity.Property(e => e.TemperaturaRecibo).HasColumnName("Temperatura_Recibo");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.Property(e => e.VolumenBruto).HasColumnName("Volumen_Bruto");

                entity.Property(e => e.VolumenNeto).HasColumnName("Volumen_Neto");

                entity.Property(e => e.VolumenRemisionado).HasColumnName("Volumen_Remisionado");

                entity.HasOne(d => d.IdContadorNavigation)
                    .WithMany(p => p.TRecibosVolumen)
                    .HasForeignKey(d => d.IdContador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Volumen_T_Contadores");

                entity.HasOne(d => d.IdReciboNavigation)
                    .WithOne(p => p.TRecibosVolumen)
                    .HasForeignKey<TRecibosVolumen>(d => d.IdRecibo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Recibos_Volumen_T_Recibos_Base");
            });

            modelBuilder.Entity<TShipTo>(entity =>
            {
                entity.ToTable("T_Clientes_ERP_Ship_To");

                entity.Property(e => e.Ship_to)
                    .HasColumnName("Ship_to");

                entity.Property(e => e.Nombre_1)
                    .HasColumnName("Nombre_1");

                entity.Property(e => e.Nombre_2)
                    .HasColumnName("Nombre_2");

                entity.Property(e => e.Nombre_3)
                    .HasColumnName("Nombre_3");

                entity.Property(e => e.Nombre_4)
                    .HasColumnName("Nombre_4");

                entity.Property(e => e.Ciudad)
                    .HasColumnName("Ciudad");

                entity.Property(e => e.Distrito)
                    .HasColumnName("Distrito");

                entity.Property(e => e.Codigo_Postal)
                    .HasColumnName("Codigo_Postal");

                entity.Property(e => e.Region)
                    .HasColumnName("Region");

                entity.Property(e => e.Telefono_1)
                    .HasColumnName("Telefono_1");

                entity.Property(e => e.Telefono_2)
                    .HasColumnName("Telefono_2");

            });

            modelBuilder.Entity<TSoldTo>(entity =>
            {
                entity.ToTable("T_Clientes_ERP_Sold_To");

                entity.Property(e => e.Sold_to)
                    .HasColumnName("Sold_To");

                entity.Property(e => e.Nombre_1)
                    .HasColumnName("Nombre_1");

                entity.Property(e => e.Nombre_2)
                    .HasColumnName("Nombre_2");

                entity.Property(e => e.Nombre_3)
                    .HasColumnName("Nombre_3");

                entity.Property(e => e.Nombre_4)
                    .HasColumnName("Nombre_4");

                entity.Property(e => e.Ciudad)
                    .HasColumnName("Ciudad");

                entity.Property(e => e.Distrito)
                    .HasColumnName("Distrito");

                entity.Property(e => e.Codigo_Postal)
                    .HasColumnName("Codigo_Postal");

                entity.Property(e => e.Region)
                    .HasColumnName("Region");

                entity.Property(e => e.Telefono_1)
                    .HasColumnName("Telefono_1");

                entity.Property(e => e.Telefono_2)
                    .HasColumnName("Telefono_2");

            });

            modelBuilder.Entity<TTanque>(entity =>
            {
                entity.HasKey(e => new { e.IdTanque, e.IdTerminal })
                    .HasName("PK_T_Tanques_1");

                entity.ToTable("T_Tanques");

                entity.Property(e => e.IdTanque)
                    .HasMaxLength(25)
                    .HasColumnName("Id_Tanque");

                entity.Property(e => e.IdTerminal)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Terminal");

                entity.Property(e => e.AforadoPor)
                    .IsRequired()
                    .HasMaxLength(254)
                    .HasColumnName("Aforado_Por");

                entity.Property(e => e.CapacidadNominal).HasColumnName("Capacidad_Nominal");

                entity.Property(e => e.CapacidadOperativa).HasColumnName("Capacidad_Operativa");

                entity.Property(e => e.ClaseTanque)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Clase_Tanque");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FechaAforo)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Aforo");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id")
                    .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.IdEstado).HasColumnName("Id_Estado");

                entity.Property(e => e.IdProducto)
                    .IsRequired()
                    .HasMaxLength(40)
                    .HasColumnName("Id_Producto");

                entity.Property(e => e.Observaciones).HasMaxLength(254);

                entity.Property(e => e.PantallaFlotante).HasColumnName("Pantalla_Flotante");

                entity.Property(e => e.TipoTanque)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasColumnName("Tipo_Tanque");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.Property(e => e.VolumenNoBombeable).HasColumnName("Volumen_No_Bombeable");

                entity.Property(e => e.AlturaMaximaAforo).HasColumnName("Altura_Maxima_Aforo");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TTanques)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Tanques_T_Tanques_Estados");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.TTanques)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Tanques_T_Productos");

                entity.HasOne(d => d.IdTerminalNavigation)
                    .WithMany(p => p.TTanques)
                    .HasForeignKey(d => d.IdTerminal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Tanques_T_Terminales");
                
            });

            modelBuilder.Entity<TTanquesAforo>(entity =>
            {
                entity.HasKey(e => new { e.IdTanque, e.IdTerminal, e.Nivel });

                entity.ToTable("T_Tanques_Aforo");

                entity.Property(e => e.IdTanque)
                    .HasMaxLength(25)
                    .HasColumnName("Id_Tanque");

                entity.Property(e => e.IdTerminal)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Terminal");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId).HasColumnName("Fila_Id");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.IdT)
                    .WithMany(p => p.TTanquesAforos)
                    .HasForeignKey(d => new { d.IdTanque, d.IdTerminal })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Tanques_Aforo_T_Tanques");
            });

            modelBuilder.Entity<TTanquesAforoResponsable>(entity =>
            {
                entity.HasKey(e => new { e.IdAforo, e.IdTanque, e.IdTerminal })
                    .HasName("PK_T_Tanques_Aforo_Responsables_1");

                entity.ToTable("T_Tanques_Aforo_Responsables");

                entity.Property(e => e.IdAforo)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Id_Aforo");

                entity.Property(e => e.IdTanque)
                    .HasMaxLength(25)
                    .HasColumnName("Id_Tanque");

                entity.Property(e => e.IdTerminal)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Terminal");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.Fecha).HasColumnType("datetime");

                entity.Property(e => e.Responsable)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.IdT)
                    .WithMany(p => p.TTanquesAforoResponsables)
                    .HasForeignKey(d => new { d.IdTanque, d.IdTerminal })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Tanques_Aforo_Responsables_T_Tanques");
            });

            modelBuilder.Entity<TTanquesEstado>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("T_Tanques_Estados");

                entity.Property(e => e.IdEstado).HasColumnName("Id_Estado");

                entity.Property(e => e.ColorHex)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.Icono)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TTanquesPantallaFlotante>(entity =>
            {
                entity.HasKey(e => new { e.IdTanque, e.IdTerminal })
                    .HasName("PK_T_Tanques_Pantalla_Flotante_1");

                entity.ToTable("T_Tanques_Pantalla_Flotante");

                entity.Property(e => e.IdTanque)
                    .HasMaxLength(25)
                    .HasColumnName("Id_Tanque");

                entity.Property(e => e.IdTerminal)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Terminal");

                entity.Property(e => e.DensidadAforo).HasColumnName("Densidad_Aforo");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.GalonesPorGrado).HasColumnName("Galones_Por_Grado");

                entity.Property(e => e.NivelCorreccionFinal).HasColumnName("Nivel_Correccion_Final");

                entity.Property(e => e.NivelCorreccionInicial).HasColumnName("Nivel_Correccion_Inicial");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.IdTanqueNavigation)
                    .WithOne(p => p.IdTanquesPantallaFlotanteNavigation)
                    .HasForeignKey<TTanquesPantallaFlotante>(d => new { d.IdTanque, d.IdTerminal })
                    .HasConstraintName("FK_T_Tanques_Pantalla_Flotante_T_Tanques");


            });

            modelBuilder.Entity<TTAS>(entity =>
            {
                entity.HasKey(e => e.Id_TAS);

                entity.ToTable("T_TAS");

                entity.Property(e => e.Id_TAS)
                    .HasColumnName("Id_TAS");

                entity.Property(e => e.Id_Modo)
                    .HasColumnName("Id_Modo");

                entity.HasOne(d => d.IdDespachosModosNavigation)
                    .WithMany(p => p.TTAS)
                    .HasForeignKey(d => d.Id_Modo)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_TAS_T_Despachos_Modos");

            });

            modelBuilder.Entity<TTASCortes>(entity =>
            {
                entity.HasKey(e => e.Id_Corte);

                entity.ToTable("T_TAS_Cortes");

                entity.Property(e => e.Id_Corte)
                    .HasColumnName("Id_Corte");

                entity.Property(e => e.Id_Terminal)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Terminal");

                entity.Property(e => e.Fecha_Corte)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Corte");

                entity.Property(e => e.Fecha_Cierre_Kairos)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Cierre_Kairos");

                entity.Property(e => e.Id_TAS_Folio)
                    .HasMaxLength(30)
                    .HasColumnName("Id_TAS_Folio");

                entity.Property(e => e.Id_TAS)
                    .HasColumnName("Id_TAS");

                entity.Property(e => e.Ultima_Edicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.Property(e => e.Editado_Por)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.HasOne(d => d.IdTASNavigation)
                    .WithMany(p => p.TTASCortes)
                    .HasForeignKey(d => d.Id_TAS)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_TAS_Cortes_T_TAS");

                entity.HasOne(d => d.IdTerminalNavigation)
                    .WithMany(p => p.TTASCortes)
                    .HasForeignKey(d => d.Id_Terminal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_TAS_Cortes_T_Terminales");

            });

            modelBuilder.Entity<TTerminalCompañia>(entity =>
            {
                entity.HasKey(e => new { e.IdTerminal, e.IdCompañia });

                entity.ToTable("T_Terminal_Compañias");

                entity.Property(e => e.IdTerminal)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Terminal");

                entity.Property(e => e.IdCompañia)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Compañia");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.IdCompañiaAgrupadora)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Id_Compañia_Agrupadora");

                entity.Property(e => e.PorcentajePropiedad).HasColumnName("Porcentaje_Propiedad");

                entity.Property(e => e.SicomCompañiaTerminal)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("SICOM_Compañia_Terminal");

                entity.HasOne(d => d.IdCompañiaNavigation)
                    .WithMany(p => p.TTerminalCompañia)
                    .HasForeignKey(d => d.IdCompañia)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Terminal_Compañias_T_Compañias");

                entity.HasOne(d => d.IdTerminalNavigation)
                    .WithMany(p => p.TTerminalCompañia)
                    .HasForeignKey(d => d.IdTerminal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Terminal_Compañias_T_Terminales");
            });

            modelBuilder.Entity<TTerminalContador>(entity =>
            {
                entity.HasKey(e => new { e.IdTerminal, e.IdContador });

                entity.ToTable("T_Terminales_Contadores");

                entity.Property(e => e.IdTerminal)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Terminal");

                entity.Property(e => e.IdContador)
                    .HasMaxLength(20)
                    .HasColumnName("Id_Contador");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");


                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                     .ValueGeneratedOnAdd()
                     .HasColumnName("Fila_Id")
                     .ValueGeneratedOnAddOrUpdate();

                entity.HasOne(d => d.IdTerminalNavigation)
                    .WithMany(p => p.TTerminalContador)
                    .HasForeignKey(d => d.IdTerminal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Terminales_Contadores_T_Terminales");

                entity.HasOne(d => d.IdContadorNavigation)
                    .WithMany(p => p.TTerminalContador)
                    .HasForeignKey(d => d.IdContador)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Terminales_Contadores_T_Contadores");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.TTerminalContador)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Terminales_Contadores_T_Productos");
            });

            modelBuilder.Entity<TTerminalCompañiasProducto>(entity =>
            {
                entity.HasKey(e => new { e.IdProducto, e.IdTerminal, e.IdCompañia });

                entity.ToTable("T_Terminal_Compañias_Productos");

                entity.Property(e => e.IdProducto)
                    .HasMaxLength(40)
                    .HasColumnName("Id_Producto");

                entity.Property(e => e.IdTerminal)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Terminal");

                entity.Property(e => e.IdCompañia)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Compañia");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId).HasColumnName("Fila_Id");

                entity.Property(e => e.IdCompañíaQueAsume)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Id_Compañía_que_asume");

                entity.Property(e => e.ParticipaEnVariaciones).HasColumnName("Participa_En_Variaciones");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.IdCompañíaQueAsumeNavigation)
                    .WithMany(p => p.TTerminalCompañiasProductos)
                    .HasForeignKey(d => d.IdCompañíaQueAsume)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Terminal_Compañias_Productos_T_Compañias");

                entity.HasOne(d => d.IdProductoNavigation)
                    .WithMany(p => p.TTerminalCompañiasProductos)
                    .HasForeignKey(d => d.IdProducto)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Terminales_Compañias_Productos_T_Productos");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.TTerminalCompañiasProductos)
                    .HasForeignKey(d => new { d.IdTerminal, d.IdCompañia })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Terminal_Compañias_Productos_T_Terminal_Compañias");
            });

            modelBuilder.Entity<TTerminal>(entity =>
            {
                entity.HasKey(e => e.IdTerminal);

                entity.ToTable("T_Terminales");

                entity.Property(e => e.IdTerminal)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Terminal");

                entity.Property(e => e.CentroCosto)
                    .IsRequired()
                    .HasMaxLength(25)
                    .HasColumnName("Centro_Costo");

                entity.Property(e => e.Direccion)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                     .ValueGeneratedOnAdd()
                     .HasColumnName("Fila_Id")
                     .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.IdArea)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Id_Area");

                entity.Property(e => e.IdCompañiaOperadora)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Id_Compañia_Operadora");

                entity.Property(e => e.IdEstado).HasColumnName("Id_Estado");

                entity.Property(e => e.Poliducto)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Superintendente)
                    .IsRequired()
                    .HasMaxLength(150);

                entity.Property(e => e.Telefono)
                    .IsRequired()
                    .HasMaxLength(25);

                entity.Property(e => e.Terminal)
                    .IsRequired()
                    .HasMaxLength(80);

                entity.Property(e => e.TipoInformeTerceros)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Tipo_Informe_Terceros");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.Property(e => e.VentasTerceros).HasColumnName("Ventas_Terceros");

                entity.HasOne(d => d.IdAreaNavigation)
                    .WithMany(p => p.TTerminales)
                    .HasForeignKey(d => d.IdArea)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Terminales_T_Areas");

                entity.HasOne(d => d.IdEstadoNavigation)
                    .WithMany(p => p.TTerminales)
                    .HasForeignKey(d => d.IdEstado)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Terminales_T_Terminales_Estados");
            });

            modelBuilder.Entity<TTerminalesEstado>(entity =>
            {
                entity.HasKey(e => e.IdEstado);

                entity.ToTable("T_Terminales_Estados");

                entity.Property(e => e.IdEstado).HasColumnName("Id_Estado");

                entity.Property(e => e.ColorHex)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.Icono)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TTerminalesProductosReceta>(entity =>
            {
                entity.HasKey(e => new { e.IdTerminal, e.IdProducto, e.IdReceta, e.FechaInicio })
                    .HasName("PK_T_Terminales_Productos");

                entity.ToTable("T_Terminales_Productos_Recetas");

                entity.Property(e => e.IdTerminal)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Terminal");

                entity.Property(e => e.IdProducto)
                    .HasMaxLength(40)
                    .HasColumnName("Id_Producto");

                entity.Property(e => e.IdReceta)
                    .HasMaxLength(50)
                    .HasColumnName("Id_Receta");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FechaFin)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Fin");

                entity.Property(e => e.FechaInicio)
                    .HasColumnType("datetime")
                    .HasColumnName("Fecha_Inicio");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.IdTerminalNavigation)
                    .WithMany(p => p.TTerminalesProductosReceta)
                    .HasForeignKey(d => d.IdTerminal)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Terminales_Productos_T_Terminales");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.TTerminalesProductosReceta)
                    .HasForeignKey(d => new { d.IdReceta, d.IdProducto })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_Terminales_Productos_T_Productos_Recetas");
            });

            modelBuilder.Entity<TTrailer>(entity =>
            {
                entity.HasKey(e => e.PlacaTrailer);

                entity.ToTable("T_Trailers");

                entity.Property(e => e.PlacaTrailer)
                    .HasMaxLength(8)
                    .HasColumnName("Placa_Trailer");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TUPermiso>(entity =>
            {
                entity.HasKey(e => e.IdPermiso);

                entity.ToTable("T_U_Permisos");

                entity.Property(e => e.IdPermiso)
                    .ValueGeneratedNever()
                    .HasColumnName("Id_Permiso");

                entity.Property(e => e.Color).HasMaxLength(10);

                entity.Property(e => e.Contenido).HasMaxLength(40);

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.Icono).HasMaxLength(40);

                entity.Property(e => e.IdClase).HasColumnName("Id_Clase");

                entity.Property(e => e.IdPermisoPadre).HasColumnName("Id_Permiso_Padre");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.HasOne(d => d.IdClaseNavigation)
                    .WithMany(p => p.TUPermisos)
                    .HasForeignKey(d => d.IdClase)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_U_Permisos_T_Clases_Permiso");

                entity.HasOne(d => d.IdPermisoPadreNavigation)
                    .WithMany(p => p.InverseIdPermisoPadreNavigation)
                    .HasForeignKey(d => d.IdPermisoPadre)
                    .HasConstraintName("FK_T_U_Permisos_T_U_Permisos_Padre");
            });

            modelBuilder.Entity<TURole>(entity =>
            {
                entity.HasKey(e => e.IdRol);

                entity.ToTable("T_U_Roles");

                entity.Property(e => e.IdRol)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Rol");

                entity.Property(e => e.Descripcion)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAddOrUpdate()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");
            });

            modelBuilder.Entity<TURolesPermiso>(entity =>
            {
                entity.HasKey(e => new { e.IdRol, e.IdPermiso });

                entity.ToTable("T_U_Roles_Permisos");

                entity.Property(e => e.IdRol)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Rol");

                entity.Property(e => e.IdPermiso).HasColumnName("Id_Permiso");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.FilaId)
                    .ValueGeneratedOnAdd()
                    .HasColumnName("Fila_Id");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.IdPermisoNavigation)
                    .WithMany(p => p.TURolesPermisos)
                    .HasForeignKey(d => d.IdPermiso)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_U_Roles_Permisos_T_U_Permisos");

                entity.HasOne(d => d.IdRolNavigation)
                    .WithMany(p => p.TURolesPermisos)
                    .HasForeignKey(d => d.IdRol)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_U_Roles_Permisos_T_U_Roles");
            });

            modelBuilder.Entity<TUUsuario>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("T_U_Usuarios");

                entity.Property(e => e.IdUsuario)
                    .HasMaxLength(128)
                    .HasColumnName("Id_Usuario");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.Email)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.FilaId)
                         .ValueGeneratedOnAdd()
                         .HasColumnName("Fila_Id")
                         .ValueGeneratedOnAddOrUpdate();

                entity.Property(e => e.Nombre)
                    .IsRequired()
                    .HasMaxLength(254);

                entity.Property(e => e.RolId)
                    .IsRequired()
                    .HasMaxLength(10)
                    .HasColumnName("Rol_Id");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.Rol)
                    .WithMany(p => p.TUUsuarios)
                    .HasForeignKey(d => d.RolId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_U_Usuarios_T_U_Roles");
            });

            modelBuilder.Entity<TUUsuariosImagen>(entity =>
            {
                entity.HasKey(e => e.IdUsuario);

                entity.ToTable("T_U_Usuarios_Imagen");

                entity.Property(e => e.IdUsuario)
                    .HasMaxLength(128)
                    .HasColumnName("Id_Usuario");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithOne(p => p.TUUsuarioImagen)
                    .HasForeignKey<TUUsuariosImagen>(d => d.IdUsuario)
                    .HasConstraintName("FK_T_U_Usuarios_Imagen_T_U_Usuarios");
            });

            modelBuilder.Entity<TUUsuariosTerminalCompañia>(entity =>
            {
                entity.HasKey(e => new { e.IdUsuario, e.IdTerminal, e.IdCompañia });

                entity.ToTable("T_U_Usuarios_Terminal_Compañia");

                entity.Property(e => e.IdUsuario)
                    .HasMaxLength(128)
                    .HasColumnName("Id_Usuario");

                entity.Property(e => e.IdTerminal)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Terminal");

                entity.Property(e => e.IdCompañia)
                    .HasMaxLength(10)
                    .HasColumnName("Id_Compañia");

                entity.Property(e => e.EditadoPor)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("Editado_Por");

                entity.Property(e => e.Estado)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.UltimaEdicion)
                    .HasColumnType("datetime")
                    .HasColumnName("Ultima_Edicion");

                entity.HasOne(d => d.IdUsuarioNavigation)
                    .WithMany(p => p.TUUsuariosTerminalCompañia)
                    .HasForeignKey(d => d.IdUsuario)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_U_Usuarios_Terminal_Compañia_T_U_Usuarios");

                entity.HasOne(d => d.Id)
                    .WithMany(p => p.TUUsuariosTerminalCompañia)
                    .HasForeignKey(d => new { d.IdTerminal, d.IdCompañia })
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_T_U_Usuarios_Terminal_Compañia_T_Terminal_Compañias");
            });

            modelBuilder.Entity<VDbTabla>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_DB_Tablas");

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.PrincipalId).HasColumnName("principal_id");

                entity.Property(e => e.SchemaId).HasColumnName("schema_id");
            });

            modelBuilder.Entity<VDbColumna>(entity =>
            {
                entity.HasNoKey();

                entity.ToView("V_DB_Columnas");

                entity.Property(e => e.ColumnId).HasColumnName("column_id");

                entity.Property(e => e.IsIdentity).HasColumnName("is_identity");

                entity.Property(e => e.IsNullable).HasColumnName("is_nullable");

                entity.Property(e => e.MaxLength).HasColumnName("max_length");

                entity.Property(e => e.Name)
                    .HasMaxLength(128)
                    .HasColumnName("name");

                entity.Property(e => e.ObjectId).HasColumnName("object_id");

                entity.Property(e => e.Precision).HasColumnName("precision");

                entity.Property(e => e.Tipo)
                    .IsRequired()
                    .HasMaxLength(128);

                entity.Property(e => e.UserTypeId).HasColumnName("user_type_id");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
