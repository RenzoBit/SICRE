namespace SICRE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cr_motivo")]
	[DisplayColumn("descripcion")]
	public partial class Motivo
    {
        public Motivo()
        {
            lsolicitud = new HashSet<Solicitud>();
        }

        [Key]
        public int idmotivo { get; set; }

        [Display(Name = "Descripción")]
		[Required(ErrorMessage = "Ingrese una descripción")]
        [StringLength(50)]
        public string descripcion { get; set; }

        public ICollection<Solicitud> lsolicitud { get; set; }
    }
}
