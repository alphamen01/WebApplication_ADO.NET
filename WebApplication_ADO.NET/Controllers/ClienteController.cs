using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication_ADO.NET.DAL;
using WebApplication_ADO.NET.Models;

namespace WebApplication_ADO.NET.Controllers
{
    public class ClienteController : Controller
    {
        ClienteDAL clienteDAL = new ClienteDAL();

        // GET: Cliente
        public ActionResult Index()
        {
            var clienteList = clienteDAL.GetAllClientes();

            if (clienteList.Count == 0)
            {
                TempData["InfoMessage"] = "Clientes actualmente no disponibles en la Base de Datos.";
            }

            return View(clienteList);
        }

        // GET: Cliente/Details/5
        public ActionResult Details(int id)
        {
            return View();
        }

        // GET: Cliente/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Cliente/Create
        [HttpPost]
        public ActionResult Create(Cliente cliente)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    bool isInserted = clienteDAL.InsertCliente(cliente);

                    if (isInserted)
                    {
                        TempData["SuccessMessage"] = "Cliente registrado exitosamente.";
                        return RedirectToAction("Index");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "El cliente ya existe, no se puede realizar el registro.";
                    }
                }
                return View(cliente); // Devuelve la vista con el modelo para que el usuario pueda corregir errores.
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return View(cliente); // Devuelve la vista con el modelo para que el usuario pueda corregir errores.
            }
        }

        //public ActionResult Create(Cliente cliente)
        //{

        //    bool isInserted = false;
        //    try
        //    {
        //        if (ModelState.IsValid)
        //        {
        //            isInserted = clienteDAL.InsertCliente(cliente);

        //            if (isInserted)
        //            {
        //                TempData["SuccessMessage"] = "Cliente registrado exitoxamente.....";
        //            }
        //            else
        //            {
        //                TempData["ErrorMessage"] = "No se pudo registrar el cliente.....";
        //            }
        //            return RedirectToAction("Index");
        //        }
        //    }
        //    catch (Exception ex)
        //    {

        //        TempData["ErrorMessage"] = ex.Message;
        //        return View();
        //    }

        //}

        // GET: Cliente/Edit/5
        public ActionResult Edit(int id)
        {
            return View();
        }

        // POST: Cliente/Edit/5
        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        // GET: Cliente/Delete/5
        public ActionResult Delete(int id)
        {
            return View();
        }

        // POST: Cliente/Delete/5
        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
