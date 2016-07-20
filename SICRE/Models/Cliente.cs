namespace SICRE.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

	[Table("ca_cliente")]
	[DisplayColumn("nombre")]
	public partial class Cliente : Persona
	{
		public Cliente()
		{
			lcrediticio = new HashSet<Crediticio>();
			lsolicitudi = new HashSet<Solicitud>();
			lsolicitudd = new HashSet<Solicitud>();
		}

		[Display(Name = "Deuda")]
		[Required(ErrorMessage = "Ingrese un valor de deuda")]
		[DefaultValue(false)]
		public bool deuda { get; set; }

		[Display(Name = "R.U.C.")]
		[StringLength(11)]
		public string ruc { get; set; }

		public ICollection<Crediticio> lcrediticio { get; set; }

		public ICollection<Solicitud> lsolicitudi { get; set; }

		public ICollection<Solicitud> lsolicitudd { get; set; }
	}
}
