namespace SICRE.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;

    [Table("cr_crediticio")]
	[DisplayColumn("fecha")]
	public partial class Crediticio
    {
        public Crediticio()
        {
            lcalificacion = new HashSet<Calificacion>();
        }

        [Key]
        public int idcrediticio { get; set; }

        [Display(Name = "Cliente")]
		[Required(ErrorMessage = "Seleccione un cliente")]
		public int idpersona { get; set; }

        [Display(Name = "Fecha de creación")]
		[Required(ErrorMessage = "Seleccione una fecha de creación")]
		[Column(TypeName = "date")]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime fechacreacion { get; set; }

		[NotMapped]
		[Display(Name = "R.U.C. Cliente")]
		[Required(ErrorMessage = "Ingrese un R.U.C.")]
		[StringLength(11)]
        public string cliente_ruc { set; get; }

		[NotMapped]
		[Display(Name = "Razón social cliente")]
		[Required(ErrorMessage = "Ingrese una razon social")]
		[StringLength(100)]
        public string cliente_razon { set; get; }

        public ICollection<Calificacion> lcalificacion { get; set; }

        public Cliente cliente { get; set; }
    }
}
