namespace SICRE.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("cr_analista")]
	[DisplayColumn("nombre")]
	public partial class Analista : Persona
	{
		public Analista()
		{
			lsolicitud = new HashSet<Solicitud>();
		}

		[Display(Name = "Autonomía")]
		[Required(ErrorMessage = "Ingrese la autonomía")]
		[DefaultValue(0)]
		public decimal autonomia { get; set; }

		public ICollection<Solicitud> lsolicitud { get; set; }
	}
}
