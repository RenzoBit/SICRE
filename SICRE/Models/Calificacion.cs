namespace SICRE.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

    [Table("cr_calificacion")]
	[DisplayColumn("deuda")]
	public partial class Calificacion
    {
        [Key]
        public int idcalificacion { get; set; }

		[Display(Name = "Perfil crediticio")]
		[Required(ErrorMessage = "Seleccione un perfil crediticio")]
		public int idcrediticio { get; set; }

		[Display(Name = "Período")]
		[Required(ErrorMessage = "Seleccione un período")]
		public int idperiodo { get; set; }

        [Display(Name = "Deuda total")]
		[Required(ErrorMessage = "Ingrese una deuda total")]
		[DefaultValue(0)]
		public decimal deuda { get; set; }

        [Display(Name = "Entidades")]
		[Required(ErrorMessage = "Ingrese un número de entidades")]
		[DefaultValue(0)]
		public int entidad { get; set; }

        [Display(Name = "% normal")]
		[Required(ErrorMessage = "Ingrese el porcentaje normal")]
		[DefaultValue(0)]
		public decimal normal { get; set; }

        [Display(Name = "% en problema")]
		[Required(ErrorMessage = "Ingrese el porcentaje en problema")]
		[DefaultValue(0)]
		public decimal problema { get; set; }

        [Display(Name = "% deficiente")]
		[Required(ErrorMessage = "Ingrese el porcentaje deficiente")]
		[DefaultValue(0)]
		public decimal deficiente { get; set; }

        [Display(Name = "% dudoso")]
		[Required(ErrorMessage = "Ingrese el porcentaje dudoso")]
		[DefaultValue(0)]
		public decimal dudoso { get; set; }

        [Display(Name = "% perdida")]
		[Required(ErrorMessage = "Ingrese el porcentaje en perdida")]
		[DefaultValue(0)]
		public decimal perdida { get; set; }

        public Crediticio crediticio { get; set; }

        public Periodo periodo { get; set; }
    }
}
