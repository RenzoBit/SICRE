namespace SICRE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cr_aprobacion")]
	[DisplayColumn("descripcion")]
	public partial class Aprobacion
    {
        public Aprobacion()
        {
            lsolicitud = new HashSet<Solicitud>();
        }

        [Key]
        public int idaprobacion { get; set; }

        [Display(Name = "Descripci�n")]
		[Required(ErrorMessage = "Ingrese una descripci�n")]
        [StringLength(50)]
        public string descripcion { get; set; }

        public ICollection<Solicitud> lsolicitud { get; set; }
    }
}
