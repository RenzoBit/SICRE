using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SICRE.Models;
using System.Data.Entity.Validation;
using System.Diagnostics;

namespace SICRE.Controllers
{
    public class SolicitudController : Controller
    {
        private BDSICRE db = new BDSICRE();

        public ActionResult Index()
		{
			ViewBag.idimportador = new SelectList(db.Cliente, "idpersona", "nombre");
			ViewBag.iddistribuidor = new SelectList(db.Cliente, "idpersona", "nombre");
			ViewBag.idaprobacion = new SelectList(db.Aprobacion, "idaprobacion", "descripcion");
			ViewBag.idhabilitacion = new SelectList(db.Habilitacion, "idhabilitacion", "descripcion");
			ViewBag.idmotivo = new SelectList(db.Motivo, "idmotivo", "descripcion");
            var solicitud = db.Solicitud.OrderByDescending(x => x.fechacreacion).Include(x => x.aprobacion).Include(x => x.importador).Include(x => x.distribuidor).Include(x => x.habilitacion).Include(x => x.motivo);
            return View(solicitud.ToList());
        }

		[HttpPost]
		public ActionResult Index(int idimportador = 0, int iddistribuidor = 0, int idaprobacion = 0, int idhabilitacion = 0, int idmotivo = 0, DateTime? fechacreacion = null)
		{
			ViewBag.idimportador = new SelectList(db.Cliente, "idpersona", "nombre", idimportador);
			ViewBag.iddistribuidor = new SelectList(db.Cliente, "idpersona", "nombre", iddistribuidor);
			ViewBag.idaprobacion = new SelectList(db.Aprobacion, "idaprobacion", "descripcion", idaprobacion);
			ViewBag.idhabilitacion = new SelectList(db.Habilitacion, "idhabilitacion", "descripcion", idhabilitacion);
			ViewBag.idmotivo = new SelectList(db.Motivo, "idmotivo", "descripcion", idmotivo);
			List<Solicitud> l = db.Solicitud.Where(
				x => (0 == idimportador || x.idimportador == idimportador)
				&& (0 == iddistribuidor || x.iddistribuidor == iddistribuidor)
				&& (0 == idaprobacion || x.idaprobacion == idaprobacion)
				&& (0 == idhabilitacion || x.idhabilitacion == idhabilitacion)
				&& (0 == idmotivo || x.idmotivo == idmotivo)
				&& (null == fechacreacion || x.fechacreacion == fechacreacion))
				.OrderByDescending(x => x.fechacreacion).Include(x => x.aprobacion).Include(x => x.importador).Include(x => x.distribuidor).Include(x => x.habilitacion).Include(x => x.motivo).ToList();
			return View(l);
		}

		public ActionResult IndexAnalista()
		{
			ViewBag.idanalista = new SelectList(db.Analista, "idpersona", "nombre");
			ViewBag.idanalistab = new SelectList(db.Analista, "idpersona", "nombre");
			var solicitud = db.Solicitud.OrderByDescending(x => x.fechacreacion).Include(x => x.importador).Include(x => x.distribuidor).Include(x => x.analista).Include(x => x.motivo);
			return View(solicitud.ToList());
		}

		[HttpPost]
		public ActionResult IndexAnalista(int[] idsolicitud, int idanalista = 0, int idanalistab = 0, int asignar = 0)
		{
			List<Solicitud> l = null;
			if (asignar == 1)
			{
				l = db.Solicitud.OrderByDescending(x => x.fechacreacion).Include(x => x.importador).Include(x => x.distribuidor).Include(x => x.analista).Include(x => x.motivo).ToList();
				if (idsolicitud != null && idsolicitud.Length > 0)
				{
					Solicitud solicitud = null;
					foreach (int i in idsolicitud)
					{
						solicitud = db.Solicitud.Find(i);
						solicitud.idanalista = idanalista;
						solicitud.importador_ruc = "x";
						solicitud.distribuidor_ruc = "x";
						solicitud.importador_razon = "x";
						solicitud.distribuidor_razon = "x";
						db.Entry(solicitud).State = EntityState.Modified;
						db.SaveChanges();
					}
				}
			}
			else
				l = db.Solicitud.Where(x => 0 == idanalistab || x.idanalista == idanalistab).OrderByDescending(x => x.fechacreacion).Include(x => x.importador).Include(x => x.distribuidor).Include(x => x.analista).Include(x => x.motivo).ToList();
			ViewBag.idanalista = new SelectList(db.Analista, "idpersona", "nombre", idanalista);
			ViewBag.idanalistab = new SelectList(db.Analista, "idpersona", "nombre", idanalistab);
			return View(l);
		}

		public ActionResult IndexDentro()
		{
			ViewBag.idimportador = new SelectList(db.Cliente, "idpersona", "nombre");
			ViewBag.iddistribuidor = new SelectList(db.Cliente, "idpersona", "nombre");
			ViewBag.idmotivo = new SelectList(db.Motivo, "idmotivo", "descripcion");
			var solicitud = db.Solicitud.OrderByDescending(x => x.fechacreacion).Include(x => x.importador).Include(x => x.distribuidor).Include(x => x.motivo);
			return View(solicitud.ToList());
		}

		[HttpPost]
		public ActionResult IndexDentro(int idimportador = 0, int iddistribuidor = 0, int idmotivo = 0)
		{
			ViewBag.idimportador = new SelectList(db.Cliente, "idpersona", "nombre", idimportador);
			ViewBag.iddistribuidor = new SelectList(db.Cliente, "idpersona", "nombre", iddistribuidor);
			ViewBag.idmotivo = new SelectList(db.Motivo, "idmotivo", "descripcion", idmotivo);
			var solicitud = db.Solicitud.Where(
				x => (0 == idimportador || x.idimportador == idimportador)
				&& (0 == iddistribuidor || x.iddistribuidor == iddistribuidor)
				&& (0 == idmotivo || x.idmotivo == idmotivo)).OrderByDescending(x => x.fechacreacion).Include(x => x.importador).Include(x => x.distribuidor).Include(x => x.motivo);
			return View(solicitud.ToList());
		}

		public ActionResult IndexSuperior()
		{
			ViewBag.idanalista = new SelectList(db.Analista, "idpersona", "nombre");
			ViewBag.idhabilitacion = new SelectList(db.Habilitacion.Where(x => x.idhabilitacion != 1), "idhabilitacion", "descripcion");
			var solicitud = db.Solicitud.Where(x => x.idhabilitacion != 1).OrderByDescending(x => x.fecharevision).Include(x => x.importador).Include(x => x.distribuidor).Include(x => x.analista).Include(x => x.habilitacion);
			return View(solicitud.ToList());
		}

		[HttpPost]
		public ActionResult IndexSuperior(int idanalista = 0, int idhabilitacion = 0, DateTime? fecharevision = null)
		{
			ViewBag.idanalista = new SelectList(db.Analista, "idpersona", "nombre", idanalista);
			ViewBag.idhabilitacion = new SelectList(db.Habilitacion.Where(x => x.idhabilitacion != 1), "idhabilitacion", "descripcion", idhabilitacion);
			var solicitud = db.Solicitud.Where(
				x => (0 == idanalista || x.idanalista == idanalista)
				&& (0 == idhabilitacion || x.idhabilitacion == idhabilitacion)
				&& (null == fecharevision || x.fecharevision == fecharevision)).OrderByDescending(x => x.fecharevision).Include(x => x.importador).Include(x => x.distribuidor).Include(x => x.analista).Include(x => x.habilitacion);
			return View(solicitud.ToList());
		}

        public ActionResult Details(int id = 0)
        {
			Solicitud solicitud = db.Solicitud.Include(x => x.importador).Include(x => x.distribuidor).SingleOrDefault(x => x.idsolicitud == id);
			solicitud.importador_ruc = solicitud.importador.ruc;
			solicitud.importador_razon = solicitud.importador.nombre;
			solicitud.distribuidor_ruc = solicitud.distribuidor.ruc;
			solicitud.distribuidor_razon = solicitud.distribuidor.nombre;
            if (solicitud == null)
                return HttpNotFound();
            ViewBag.idmotivo = new SelectList(db.Motivo, "idmotivo", "descripcion", solicitud.idmotivo);
            return View(solicitud);
        }

        public ActionResult Create()
        {
            ViewBag.idmotivo = new SelectList(db.Motivo, "idmotivo", "descripcion");
			Solicitud soliciud = new Solicitud() { idanalista = 1, idaprobacion = 1, idhabilitacion = 1, fechacreacion = DateTime.Now, aprobado = 0, garantizado = 0, garantia = false };
			return View(soliciud);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Solicitud solicitud)
        {
			if (solicitud.monto <= 0)
				ModelState.AddModelError("monto", "Ingrese correctamente el monto");
            if (ModelState.IsValid)
            {
				solicitud.idanalista = db.Analista.OrderBy(r => Guid.NewGuid()).Take(1).SingleOrDefault().idpersona;
                db.Solicitud.Add(solicitud);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idmotivo = new SelectList(db.Motivo, "idmotivo", "descripcion", solicitud.idmotivo);
            return View(solicitud);
		}

		public ActionResult CreateDentro(int id = 0)
		{
			Solicitud solicitud = db.Solicitud.Include(x => x.importador).Include(x => x.distribuidor).Include(x => x.motivo).SingleOrDefault(x => x.idsolicitud == id);
			solicitud.importador_ruc = "x";
			solicitud.distribuidor_ruc = "x";
			solicitud.importador_razon = "x";
			solicitud.distribuidor_razon = "x";
			ViewBag.idimportador = new SelectList(db.Cliente, "idpersona", "nombre", solicitud.idimportador);
			ViewBag.iddistribuidor = new SelectList(db.Cliente, "idpersona", "nombre", solicitud.iddistribuidor);
			ViewBag.idmotivo = new SelectList(db.Motivo, "idmotivo", "descripcion", solicitud.idmotivo);
			ViewBag.idaprobacion = new SelectList(db.Aprobacion, "idaprobacion", "descripcion", solicitud.idaprobacion);
			Session["lcalificacion"] = db.Calificacion.Where(x => x.idcrediticio == id).Include(x => x.periodo).OrderBy(x => x.periodo.descripcion).ToList();
			return View(solicitud);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateDentro(Solicitud solicitud)
		{
			Analista analista = db.Analista.Find(solicitud.idanalista);
			if (solicitud.idaprobacion == 2 && solicitud.aprobado > analista.autonomia)
				ModelState.AddModelError("aprobado", "Monto aprobado supera autonomía. Solo puede grabar con dictamen recomendar aprobación");
			if (solicitud.idaprobacion == 3 && (solicitud.observacionaprobacion == null || solicitud.observacionaprobacion.Trim() == ""))
				ModelState.AddModelError("observacionaprobacion", "Es obligatorio ingresar una observación para el dictamen rechazar");
			if (solicitud.garantizado > solicitud.aprobado)
				ModelState.AddModelError("garantizado", "El monto no puede ser mayor a la línea de crédito");
			if (solicitud.aprobado < 0)
				ModelState.AddModelError("aprobado", "Ingrese correctamente el importe aprobado");
			if (solicitud.garantizado < 0)
				ModelState.AddModelError("garantizado", "Ingrese correctamente el importe garantizado");
			if (ModelState.IsValid)
			{
				if (solicitud.idaprobacion == 4)
					solicitud.idhabilitacion = 2;
				solicitud.fecharevision = DateTime.Now;
				db.Entry(solicitud).State = EntityState.Modified;
				db.SaveChanges();
				return RedirectToAction("IndexDentro");
			}
			ViewBag.idimportador = new SelectList(db.Cliente, "idpersona", "nombre", solicitud.idimportador);
			ViewBag.iddistribuidor = new SelectList(db.Cliente, "idpersona", "nombre", solicitud.iddistribuidor);
			ViewBag.idmotivo = new SelectList(db.Motivo, "idmotivo", "descripcion", solicitud.idmotivo);
			ViewBag.idaprobacion = new SelectList(db.Aprobacion, "idaprobacion", "descripcion", solicitud.idaprobacion);
			return View(solicitud);
		}

        public ActionResult Edit(int id = 0)
        {
			Solicitud solicitud = db.Solicitud.Include(x => x.importador).Include(x => x.distribuidor).SingleOrDefault(x => x.idsolicitud == id);
			solicitud.importador_ruc = solicitud.importador.ruc;
			solicitud.importador_razon = solicitud.importador.nombre;
			solicitud.distribuidor_ruc = solicitud.distribuidor.ruc;
			solicitud.distribuidor_razon = solicitud.distribuidor.nombre;
            if (solicitud == null)
                return HttpNotFound();
            ViewBag.idmotivo = new SelectList(db.Motivo, "idmotivo", "descripcion", solicitud.idmotivo);
            return View(solicitud);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Solicitud solicitud)
		{
			if (solicitud.monto <= 0)
				ModelState.AddModelError("monto", "Ingrese correctamente el monto");
            if (ModelState.IsValid)
            {
                db.Entry(solicitud).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.idmotivo = new SelectList(db.Motivo, "idmotivo", "descripcion", solicitud.idmotivo);
            return View(solicitud);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }

		[HttpPost]
		public JsonResult ClienteList(string term)
		{
			var data = db.Cliente.Select(x => new { idpersona = x.idpersona, ruc = x.ruc, razon = x.nombre }).Where(x => x.ruc.Contains(term));
			return Json(data, JsonRequestBehavior.AllowGet);
		}
    }
}