using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;

namespace PL_MVC.Controllers
{
    public class LoginController : Controller
    {
        //public ActionResult Add()
        //{
        //    return RedirectToAction("Add", "Usuario");
        //}

        [HttpGet]
        public ActionResult login()
        {
            ML.Usuario login = new ML.Usuario();
            return View(login);
        }

        public ActionResult IniciarSesion()
        {
            ML.Usuario login = new ML.Usuario();
            return View();
        }
        [HttpPost]
        public ActionResult login(ML.Usuario usuario)
        {
            //ML.Result result = BL.Usuario.UserNameGetById(usuario);

            //if (result.Correct)
            //{
            //    if (((ML.Usuario)result.Object).Password == usuario.Password)
            //    {
            //        return RedirectToAction("Index", "Home");
            //    }
            //    else
            //    {
            //        ViewBag.Mensaje = "El usuario o la contrasena son incorrectos ";
            //    }
            //}
            //else
            //{
            //    ViewBag.Mensaje = "El usuario no existe ";
            //}


            //var Password1 = usuario.Password;

            //Login con API
            ServiceUsuario.UsuarioClient servicioPrueba = new ServiceUsuario.UsuarioClient();
            //using (var client = new HttpClient())
            //{
            //    string urlApi = System.Configuration.ConfigurationManager.AppSettings["WebAPI"].ToString();
            //    client.BaseAddress = new Uri(urlApi);

            //    //HTTP POST
            //    var postTask = client.PostAsJsonAsync<ML.Usuario>("Login/", usuario);
            //    postTask.Wait();

            //    var resultLogin = postTask.Result;
            //    if (resultLogin.IsSuccessStatusCode)
            //    {
            //        if (resultLogin == usuario.Password)
            //        {
            //            return RedirectToAction("Index", "Home");
            //        }
            //        else
            //        {
            //            ViewBag.Mensaje = "El usuario o la contrasena son incorrectos ";
            //        }
            //    }
            //    else
            //    {
            //        ViewBag.Mensaje = "El usuario no existe " + resultLogin.IsSuccessStatusCode;
            //    }

            //}

            //Login API-TOKEN

            ML.Result result = new ML.Result();

            using (var client = new HttpClient())
            {

                string urlApi = System.Configuration.ConfigurationManager.AppSettings["WebAPI"].ToString();
                client.BaseAddress = new Uri(urlApi);

                var postTask = client.PostAsJsonAsync<ML.Usuario>("Login/", usuario);
                postTask.Wait();

                var resultApi = postTask.Result;
                if (resultApi.IsSuccessStatusCode) //usuario si existe
                {
                    var stream = resultApi.Content;

                    var readTask = resultApi.Content.ReadAsAsync<ML.Result>();
                    readTask.Wait();


                    using (var clientToken = new HttpClient())
                    {
                        clientToken.BaseAddress = new Uri(urlApi);

                        var postTaskToken = clientToken.PostAsJsonAsync<ML.Usuario>("Login/Authenticate", usuario);
                        postTaskToken.Wait();
                        var resultApiToken = postTaskToken.Result;


                        if (resultApiToken.IsSuccessStatusCode)
                        {
                            var readTaskAPI = resultApiToken.Content.ReadAsAsync<ML.Result>();
                            readTaskAPI.Wait();

                            usuario = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTaskAPI.Result.Object.ToString());
                            Session["UsuarioSession"] = usuario;

                            return RedirectToAction("Index", "Home");

                        }
                        else
                        {
                            var readTaskAPI = resultApiToken.Content.ReadAsAsync<ML.Result>();
                            readTaskAPI.Wait();


                            ViewBag.Mensaje = "El usuario o la contrasena son incorrectos ";
                        }

                    }
                }
                else
                {
                    ViewBag.Message = "El UserName no existe";
                }
                return View("ModalPV");

            }
        }
    }
}