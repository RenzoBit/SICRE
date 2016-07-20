namespace SICRE.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

    [Table("cr_periodo")]
	[DisplayColumn("descripcion")]
	public partial class Periodo
    {
        public Periodo()
        {
            lcalificacion = new HashSet<Calificacion>();
        }

        [Key]
        public int idperiodo { get; set; }

        [Display(Name = "Descripción")]
		[Required(ErrorMessage = "Ingrese una descripción")]
        [StringLength(6)]
        public string descripcion { get; set; }

        [Display(Name = "Activo")]
		[Required(ErrorMessage = "Seleccione un valor para activo")]
		[DefaultValue(false)]
		public bool activo { get; set; }

        public ICollection<Calificacion> lcalificacion { get; set; }
    }
}
