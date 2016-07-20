namespace SICRE.Models
{
	using System;
	using System.Collections.Generic;
	using System.ComponentModel;
	using System.ComponentModel.DataAnnotations;
	using System.ComponentModel.DataAnnotations.Schema;

    [Table("cr_solicitud")]
	[DisplayColumn("fechacreacion")]
	public partial class Solicitud
    {
        [Key]
        public int idsolicitud { get; set; }

        [Display(Name = "Importador")]
		[Required(ErrorMessage = "Seleccione un importador")]
		public int idimportador { get; set; }

        [Display(Name = "Distribuidor")]
		[Required(ErrorMessage = "Seleccione un distribuidor")]
		public int iddistribuidor { get; set; }

		[Display(Name = "Analista")]
		[Required(ErrorMessage = "Seleccione un analista")]
		public int idanalista { get; set; }

        [Display(Name = "Aprobación")]
		[Required(ErrorMessage = "Seleccione un estado de aprobación")]
		public int idaprobacion { get; set; }

        [Display(Name = "Habilitación")]
		[Required(ErrorMessage = "Seleccione un estado de habilitación")]
		public int idhabilitacion { get; set; }

        [Display(Name = "Motivo")]
		[Required(ErrorMessage = "Seleccione un motivo")]
		public int idmotivo { get; set; }

        [Display(Name = "Monto")]
		[Required(ErrorMessage = "Ingrese un monto")]
		[DefaultValue(0)]
		public decimal monto { get; set; }

        [Display(Name = "Fecha de creación")]
		[Required(ErrorMessage = "Seleccione una fecha de creación")]
		[Column(TypeName = "date")]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime fechacreacion { get; set; }

        [Display(Name = "Fecha de revisión")]
		[Column(TypeName = "date")]
		[DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)]
		public DateTime? fecharevision { get; set; }

        [Display(Name = "Observación de aprobación")]
		[StringLength(200)]
        public string observacionaprobacion { get; set; }

        [Display(Name = "Observación de habilitación")]
		[StringLength(200)]
		public string observacionhabilitacion { get; set; }

		[Display(Name = "Importe de L.C. aprobado")]
		[Required(ErrorMessage = "Ingrese un aprobado")]
		[DefaultValue(0)]
		public decimal aprobado { get; set; }

		[Display(Name = "Importe a garantizar")]
		[Required(ErrorMessage = "Ingrese un garantizado")]
		[DefaultValue(0)]
		public decimal garantizado { get; set; }

		[Display(Name = "Solicitar garantías")]
		[Required(ErrorMessage = "Seleccione un valor")]
		[DefaultValue(false)]
		public bool garantia { get; set; }

		[NotMapped]
		[Display(Name = "R.U.C. Importador")]
		[Required(ErrorMessage = "Ingrese un R.U.C.")]
		[StringLength(11)]
        public string importador_ruc { set; get; }

		[NotMapped]
		[Display(Name = "R.U.C. Distribuidor")]
		[Required(ErrorMessage = "Ingrese un R.U.C.")]
		[StringLength(11)]
        public string distribuidor_ruc { set; get; }

		[NotMapped]
		[Display(Name = "Razón social importador")]
		[Required(ErrorMessage = "Ingrese una razon social")]
		[StringLength(100)]
        public string importador_razon { set; get; }

		[NotMapped]
		[Display(Name = "Razón social distribuidor")]
		[Required(ErrorMessage = "Ingrese una razon social")]
		[StringLength(100)]
        public string distribuidor_razon { set; get; }

        public Aprobacion aprobacion { get; set; }

        public Cliente importador { get; set; }

		public Cliente distribuidor { get; set; }

		public Analista analista { get; set; }

        public Habilitacion habilitacion { get; set; }

        public Motivo motivo { get; set; }
    }
}
