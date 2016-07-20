namespace SICRE.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("ca_persona")]
	[DisplayColumn("nombre")]
	public partial class Persona
	{
		[Key]
		public int idpersona { get; set; }

		[Display(Name = "Código")]
		[Required(ErrorMessage = "Ingrese un código")]
		[StringLength(10)]
		public string codigo { get; set; }

		[Display(Name = "Nombre")]
		[Required(ErrorMessage = "Ingrese un nombre")]
		[StringLength(200)]
		public string nombre { get; set; }

		[Display(Name = "Correo")]
		[StringLength(100)]
		public string correo { get; set; }

		[Display(Name = "Domicilio")]
		[StringLength(200)]
		public string domicilio { get; set; }
	}
}
