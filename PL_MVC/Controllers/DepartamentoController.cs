using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class DepartamentoController : Controller
    {
        [HttpGet]
        public ActionResult DepartamentoGetAll()
        {
            ML.Result result = BL.Departamento.GetAllEF();

            ML.Departamento departamento = new ML.Departamento();
            if (result.Correct)
            {
                departamento.Departamentos = result.Objects;
            }
            else
            {
                //error
            }

            return View(departamento);
        }//Fin GetAll
        [HttpGet]
        public ActionResult FormDepartamento(int? IdDepartamento)// " ? "  Este dato permite un null //Agregar y Actualizar
        {
            ML.Departamento departamento = new ML.Departamento();

            ML.Result resultDepartamento = BL.Area.GetAllEF();
            if (resultDepartamento.Correct)
            {
                departamento.Area = new ML.Area();
                departamento.Area.Areas = resultDepartamento.Objects;

                if (IdDepartamento == null)//add
                {

                    return View(departamento);
                }
                else //update
                {
                    //GeyById
                    ML.Result result = new ML.Result();
                    result = BL.Departamento.GetByIdEF(IdDepartamento.Value);
                    if (result.Correct)
                    {
                        departamento.Area = new ML.Area(); //InstanciaR antes de asignarle usuario al objeto 
                        departamento = ((ML.Departamento)result.Object);
                        departamento.Area.Areas = resultDepartamento.Objects;
                        return View(departamento);
                    }
                    else
                    {

                    }
                }

            } return View();
        }
        [HttpPost]
        public ActionResult FormDepartamento(ML.Departamento departamento)
        {
            ML.Result result = new ML.Result();

            if (departamento.IdDepartamento == 0)
            {
                result = BL.Departamento.AddEF(departamento);
                if (result.Correct)
                {
                    ViewBag.Mensaje = "El Usuario se ha registrado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "El Usuario no se ha registrado correctamente " + result.ErrorMessage;
                }

            }
            else
            {
                result = BL.Departamento.UpdateEF(departamento);

                if (result.Correct)
                {
                    ViewBag.Mensaje = "El Usuario se ha actualizado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "El Usuario no se ha actualizado correctamente " + result.ErrorMessage;
                }


            }
            return View("ModalPv");
        }
        [HttpGet]
        public ActionResult Delete(int IdDepartamento)
        {
            ML.Result result = BL.Departamento.DeleteEF(IdDepartamento);

            if (result.Correct)
            {
                ViewBag.Mensaje = "El usuario se ha eliminado correctamente";
            }
            else
            {
                ViewBag.Mensaje = "El usuario no se ha eliminado correctamente " + result.ErrorMessage;
            }

            return PartialView("ModalPV");
        }
	}
}