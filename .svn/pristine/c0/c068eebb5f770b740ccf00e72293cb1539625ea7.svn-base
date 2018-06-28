using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SECI.Business;
using SECI.Entities;


namespace SECI.Controllers
{
    public class MapaController : Controller
    {
        //// GET: Mapa
        //public ActionResult Index()
        //{

        //    return View();
        //}

        //// GET: Mapa/Details/5
        //public ActionResult Details(int id)
        //{
        //    return View();
        //}

        //// GET: Mapa/Create
        //public ActionResult Create()
        //{
        //    FacadeMapa.ObtenerMapas(); // 

        //    return View();
        //}

        //// POST: Mapa/Create
        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Mapa/Edit/5
        //public ActionResult Edit(int id)
        //{
        //    return View();
        //}

        //// POST: Mapa/Edit/5
        //[HttpPost]
        //public ActionResult Edit(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add update logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //// GET: Mapa/Delete/5
        //public ActionResult Delete(int id)
        //{
        //    return View();
        //}

        //// POST: Mapa/Delete/5
        //[HttpPost]
        //public ActionResult Delete(int id, FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add delete logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        public ActionResult ListarMapas(int Mapa, string ClaveMapa, int Hoja, int Encabezado, string FechaCreacion, string FechaModif)
        {
            Resultado<Mapas> entidad = new Resultado<Mapas>();
            entidad.Lista = new List<Mapas>();

            try
            {
                entidad.Lista = FacadeMapa.ObtenerMapasByPrefijo(ClaveMapa);
                entidad.ProcesoExitoso = 1;
            }

            catch (Exception ex)
            {
                entidad.ProcesoExitoso = 0;
                entidad.Mensaje = "Ocurrió un error:" + ex.Message;
            }
            return Json(entidad, JsonRequestBehavior.DenyGet);

        }

    }

}

