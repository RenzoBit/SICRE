namespace SICRE.Models
{
	using System;
	using System.Data.Entity;
	using System.ComponentModel.DataAnnotations.Schema;
	using System.Linq;

	public partial class BDSICRE : DbContext
	{
		public BDSICRE()
			: base("name=BDSICRE")
		{
		}

		public virtual DbSet<Cliente> Cliente { get; set; }
		public virtual DbSet<Persona> Persona { get; set; }

		public virtual DbSet<Analista> Analista { get; set; }
		public virtual DbSet<Aprobacion> Aprobacion { get; set; }
		public virtual DbSet<Calificacion> Calificacion { get; set; }
		public virtual DbSet<Crediticio> Crediticio { get; set; }
		public virtual DbSet<Habilitacion> Habilitacion { get; set; }
		public virtual DbSet<Motivo> Motivo { get; set; }
		public virtual DbSet<Periodo> Periodo { get; set; }
		public virtual DbSet<Solicitud> Solicitud { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder)
		{
			modelBuilder.Entity<Cliente>()
				.Property(e => e.ruc)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<Cliente>()
				.HasMany(e => e.lcrediticio)
				.WithRequired(e => e.cliente)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Cliente>()
				.HasMany(e => e.lsolicitudd)
				.WithRequired(e => e.distribuidor)
				.HasForeignKey(e => e.iddistribuidor)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Cliente>()
				.HasMany(e => e.lsolicitudi)
				.WithRequired(e => e.importador)
				.HasForeignKey(e => e.idimportador)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Persona>()
				.Property(e => e.codigo)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<Persona>()
				.Property(e => e.nombre)
				.IsUnicode(false);

			modelBuilder.Entity<Persona>()
				.Property(e => e.correo)
				.IsUnicode(false);

			modelBuilder.Entity<Persona>()
				.Property(e => e.domicilio)
				.IsUnicode(false);

			modelBuilder.Entity<Analista>()
				.HasMany(e => e.lsolicitud)
				.WithRequired(e => e.analista)
				.HasForeignKey(e => e.idanalista)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Aprobacion>()
				.Property(e => e.descripcion)
				.IsUnicode(false);

			modelBuilder.Entity<Aprobacion>()
				.HasMany(e => e.lsolicitud)
				.WithRequired(e => e.aprobacion)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Crediticio>()
				.HasMany(e => e.lcalificacion)
				.WithRequired(e => e.crediticio)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Habilitacion>()
				.Property(e => e.descripcion)
				.IsUnicode(false);

			modelBuilder.Entity<Habilitacion>()
				.HasMany(e => e.lsolicitud)
				.WithRequired(e => e.habilitacion)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Motivo>()
				.Property(e => e.descripcion)
				.IsUnicode(false);

			modelBuilder.Entity<Motivo>()
				.HasMany(e => e.lsolicitud)
				.WithRequired(e => e.motivo)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Periodo>()
				.Property(e => e.descripcion)
				.IsFixedLength()
				.IsUnicode(false);

			modelBuilder.Entity<Periodo>()
				.HasMany(e => e.lcalificacion)
				.WithRequired(e => e.periodo)
				.WillCascadeOnDelete(false);

			modelBuilder.Entity<Solicitud>()
				.Property(e => e.observacionaprobacion)
				.IsUnicode(false);

			modelBuilder.Entity<Solicitud>()
				.Property(e => e.observacionhabilitacion)
				.IsUnicode(false);
		}
	}
}
