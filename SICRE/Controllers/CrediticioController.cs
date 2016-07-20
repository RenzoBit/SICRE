using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SICRE.Models;

namespace SICRE.Controllers
{
	public class CrediticioController : Controller
	{
		private BDSICRE db = new BDSICRE();

		public ActionResult Index()
		{
			List<Crediticio> l = db.Crediticio.OrderByDescending(x => x.fechacreacion).Include(x => x.cliente).ToList();
			foreach (Crediticio o in l)
				o.lcalificacion = db.Calificacion.Where(x => x.idcrediticio == o.idcrediticio).Include(x => x.periodo).ToList();
			return View(l);
		}

		[HttpPost]
		public ActionResult Index(DateTime? fechacreacion = null, string ruc = "", string razon = "")
		{
			List<Crediticio> l = db.Crediticio.Where(
				x => (null == fechacreacion || x.fechacreacion == fechacreacion)
				&& ("" == ruc.Trim() || x.cliente.ruc.Contains(ruc.Trim()))
				&& ("" == razon.Trim() || x.cliente.nombre.Contains(razon.Trim())))
				.OrderByDescending(x => x.fechacreacion).Include(x => x.cliente).ToList();
			foreach (Crediticio o in l)
				o.lcalificacion = db.Calificacion.Where(x => x.idcrediticio == o.idcrediticio).Include(x => x.periodo).ToList();
			return View(l);
		}

		public ActionResult Details(int id = 0)
		{
			Crediticio crediticio = db.Crediticio.Include(x => x.cliente).SingleOrDefault(x => x.idcrediticio == id);
			crediticio.cliente_ruc = crediticio.cliente.ruc;
			crediticio.cliente_razon = crediticio.cliente.nombre;
			Session["lcalificacion"] = db.Calificacion.Where(x => x.idcrediticio == id).Include(x => x.periodo).ToList();
			if (crediticio == null)
				return HttpNotFound();
			return View(crediticio);
		}

		public ActionResult Create()
		{
			Crediticio crediticio = new Crediticio() { fechacreacion = DateTime.Now };
			Session["lcalificacion"] = new List<Calificacion>();
			return View(crediticio);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Create(Crediticio crediticio)
		{
			if (0 < db.Crediticio.Where(c => c.idpersona == crediticio.idpersona).Count())
				ModelState.AddModelError("cliente_ruc", "Ya existe un perfil para este cliente");
			if (0 == ((List<Calificacion>)Session["lcalificacion"]).Count())
				ModelState.AddModelError("idpersona", "Agregue una calificación");
			if (ModelState.IsValid)
			{
				db.Crediticio.Add(crediticio);
				db.SaveChanges();
				foreach (Calificacion o in ((List<Calificacion>)Session["lcalificacion"]))
				{
					db.Calificacion.Add(new Calificacion()
					{
						idcrediticio = crediticio.idcrediticio,
						idperiodo = o.idperiodo,
						deuda = o.deuda,
						entidad = o.entidad,
						normal = o.normal,
						problema = o.problema,
						deficiente = o.deficiente,
						dudoso = o.dudoso,
						perdida = o.perdida
					});
					db.SaveChanges();
				}
				return RedirectToAction("Index");
			}
			return View(crediticio);
		}

		public ActionResult Edit(int id = 0)
		{
			Crediticio crediticio = db.Crediticio.Include(x => x.cliente).SingleOrDefault(x => x.idcrediticio == id);
			crediticio.cliente_ruc = crediticio.cliente.ruc;
			crediticio.cliente_razon = crediticio.cliente.nombre;
			Session["lcalificacion"] = db.Calificacion.Where(x => x.idcrediticio == id).Include(x => x.periodo).ToList();
			if (crediticio == null)
				return HttpNotFound();
			return View(crediticio);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult Edit(Crediticio crediticio)
		{
			if (0 < db.Crediticio.Where(c => c.idpersona == crediticio.idpersona && c.idcrediticio != crediticio.idcrediticio).Count())
				ModelState.AddModelError("cliente_ruc", "Ya existe un perfil para este cliente");
			if (0 == ((List<Calificacion>)Session["lcalificacion"]).Count())
				ModelState.AddModelError("idpersona", "Agregue una calificación");
			if (ModelState.IsValid)
			{
				db.Entry(crediticio).State = EntityState.Modified;
				db.SaveChanges();
				List<Calificacion> lcalificacion = db.Calificacion.Where(x => x.idcrediticio == crediticio.idcrediticio).ToList();
				foreach (Calificacion calificacion in lcalificacion)
				{
					db.Calificacion.Remove(calificacion);
					db.SaveChanges();
				}
				foreach (Calificacion o in ((List<Calificacion>)Session["lcalificacion"]))
				{
					db.Calificacion.Add(new Calificacion()
					{
						idcrediticio = crediticio.idcrediticio,
						idperiodo = o.idperiodo,
						deuda = o.deuda,
						entidad = o.entidad,
						normal = o.normal,
						problema = o.problema,
						deficiente = o.deficiente,
						dudoso = o.dudoso,
						perdida = o.perdida
					});
					db.SaveChanges();
				}
				return RedirectToAction("Index");
			}
			return View(crediticio);
		}

		protected override void Dispose(bool disposing)
		{
			db.Dispose();
			base.Dispose(disposing);
		}

		public ActionResult IndexCalificacion()
		{
			return PartialView((List<Calificacion>)Session["lcalificacion"]);
		}

		public ActionResult IndexCalificacion2()
		{
			return PartialView((List<Calificacion>)Session["lcalificacion"]);
		}

		public ActionResult CreateCalificacion(int idcalificacion, int idcrediticio, int posicion)
		{
			Calificacion calificacion = posicion == 0 ? (new Calificacion() { idcalificacion = idcalificacion, idcrediticio = idcrediticio }) : ((List<Calificacion>)Session["lcalificacion"])[posicion - 1];
			ViewBag.idperiodo = new SelectList(db.Periodo, "idperiodo", "descripcion", calificacion.idperiodo);
			ViewBag.posicion = posicion.ToString();
			return PartialView(calificacion);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public ActionResult CreateCalificacion(Calificacion calificacion, FormCollection collection)
		{
			ViewBag.posicion = collection["posicion"];
			int i = 0;
			if (calificacion.normal + calificacion.problema + calificacion.deficiente + calificacion.dudoso + calificacion.perdida != 100)
				ModelState.AddModelError("normal", "Ingrese correctamente los porcentajes");
			if (calificacion.deuda <= 0)
				ModelState.AddModelError("deuda", "Ingrese correctamente la deuda");
			if (calificacion.entidad <= 0)
				ModelState.AddModelError("entidad", "Ingrese correctamente el número de entidades");
			foreach (Calificacion o in (List<Calificacion>)Session["lcalificacion"])
			{
				i++;
				if (o.idperiodo == calificacion.idperiodo && i != int.Parse(collection["posicion"]))
				{
					ModelState.AddModelError("idperiodo", "Ya existe una calificación para este período");
					break;
				}
			}
			if (ModelState.IsValid)
			{
				ViewBag.OK = "1";
				calificacion.periodo = db.Periodo.Find(calificacion.idperiodo);
				if (collection["posicion"] == "0")
					((List<Calificacion>)Session["lcalificacion"]).Add(calificacion);
				else
					((List<Calificacion>)Session["lcalificacion"])[Convert.ToInt32(collection["posicion"]) - 1] = calificacion;
			}
			ViewBag.idperiodo = new SelectList(db.Periodo, "idperiodo", "descripcion", calificacion.idperiodo);
			return PartialView(calificacion);
		}
	}
}