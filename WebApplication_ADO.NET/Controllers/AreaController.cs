using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_ADO.NET.DAL;
using WebApplication_ADO.NET.Models;

namespace WebApplication_ADO.NET.Controllers
{
    public class AreaController : Controller
    {
        AreaDAL areaDAL = new AreaDAL();
        ClienteDAL clienteDAL = new ClienteDAL();
        // GET: Area
        public ActionResult Index()
        {
            var areaList = areaDAL.GetAllAreas();

            if (areaList.Count == 0)
            {
                TempData["InfoMessage"] = "Areas actualmente no disponibles en la Base de Datos.";
            }

            return View(areaList);
        }

        // GET: Area/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Area/Create
        public ActionResult Create()
        {
            // Obtener la lista de clientes desde tu base de datos y almacenarla en ViewBag o en un modelo.
            List<Cliente> clientes = clienteDAL.GetAllClientes(); // Aquí debes implementar la obtención de clientes.

            // Almacena la lista de clientes en ViewBag para usarla en la vista.
            ViewBag.ClientesList = new SelectList(clientes, "id_cliente", "nombre");

            return View();
            //return View();
        }

        // POST: Area/Create
        [HttpPost]
        public ActionResult Create(Area area)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isInserted = areaDAL.InsertArea(area);

                    if (isInserted)
                    {
                        TempData["SuccessMessage"] = "Area registrada exitosamente.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "La descripcion del area ya existe, no se puede realizar el registro.";
                    }
                }

                // Recargar la lista de clientes para volver a mostrarla en caso de error.
                List<Cliente> clientes = clienteDAL.GetAllClientes(); // Aquí debes implementar la obtención de clientes.
                ViewBag.ClientesList = new SelectList(clientes, "id_cliente", "nombre");
                return View(area); // Devuelve la vista con el modelo para que el usuario pueda corregir errores.
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(area); // Devuelve la vista con el modelo para que el usuario pueda corregir errores.
            }
        }

        // GET: Area/Edit/5
        public ActionResult Edit(int id)
        {
            // Obtener la lista de clientes desde tu base de datos y almacenarla en ViewBag o en un modelo.
            List<Cliente> clientes = clienteDAL.GetAllClientes(); // Aquí debes implementar la obtención de clientes.

            // Almacena la lista de clientes en ViewBag para usarla en la vista.
            ViewBag.ClientesList = new SelectList(clientes, "id_cliente", "nombre");

            try
            {
                var area = areaDAL.GetArea(id).FirstOrDefault();
                if (area == null)
                {
                    TempData["InfoMessage"] = "Area no disponible.";
                    return RedirectToAction("Index");
                }
                return View(area);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
            //return View();
        }

        // POST: Area/Edit/5
        [HttpPost, ActionName("Edit")]
        public ActionResult UpdateArea(Area area)
        {

            try
            {
                if (ModelState.IsValid)
                {
                    bool isUpdate = areaDAL.UpdateArea(area);

                    if (isUpdate)
                    {
                        TempData["SuccessMessage"] = "Area actualizada exitosamente.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "La descripcion del area ya existe, no se puede actualizar el registro.";
                    }
                }
                // Recargar la lista de clientes para volver a mostrarla en caso de error.
                List<Cliente> clientes = clienteDAL.GetAllClientes(); // Aquí debes implementar la obtención de clientes.
                ViewBag.ClientesList = new SelectList(clientes, "id_cliente", "nombre");
                return View(area); // Devuelve la vista con el modelo para que el usuario pueda corregir errores.
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(area); // Devuelve la vista con el modelo para que el usuario pueda corregir errores.
            }
            //try
            //{
            //    // TODO: Add update logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }

        // GET: Area/Delete/5
        public ActionResult Delete(int id)
        {
            try
            {
                var area = areaDAL.GetArea(id).FirstOrDefault();
                if (area == null)
                {
                    TempData["InfoMessage"] = "Area no disponible.";
                    return RedirectToAction("Index");
                }
                return View(area);
            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
        }

        // POST: Area/Delete/5
        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteArea(int id)
        {

            try
            {
                string result = areaDAL.DeleteArea(id);

                if (result.Contains("eliminada"))
                {
                    TempData["SuccessMessage"] = result;
                    return RedirectToAction("Index");
                }
                else
                {
                    TempData["ErrorMessage"] = result;
                    return RedirectToAction("Delete");
                }


            }
            catch (Exception ex)
            {

                TempData["ErrorMessage"] = ex.Message;
                return View();
            }
            //try
            //{
            //    // TODO: Add delete logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //    return View();
            //}
        }
    }
}
