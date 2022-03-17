using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Configuration;
using System.Net.Http;

namespace PL_MVC.Controllers
{
    public class UsuarioController : Controller
    {
        //
        // GET: /Usuario/
        [HttpGet]
        public ActionResult GetAll()
        { 
            //GetAll con XML

            //ServiceUsuario.UsuarioClient servicioPrueba = new ServiceUsuario.UsuarioClient();
            //ML.Usuario usuario = new ML.Usuario();
            //var resultGetAll = servicioPrueba.GetAll(usuario);
            ////ML.Result result = BL.Usuario.GetAllEF(new ML.Usuario());

            ////ML.Usuario usuario = new ML.Usuario();
            //if (resultGetAll.Correct)
            //{
            //    usuario.Usuarios = resultGetAll.Objects.ToList();
            //}
            //else
            //{
            //    ViewBag.Message = resultGetAll.ErrorMessage;
            //    return PartialView("ModalPV");
            //}

            //return View(usuario);

            //GetAll con JSON - API

            ML.Usuario usuario = new ML.Usuario();
            usuario.Usuarios = new List<object>();

            using( var client = new HttpClient())
            {
                string urlApi = System.Configuration.ConfigurationManager.AppSettings["WebAPI"].ToString();
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.GetAsync("Usuario/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;

                if(result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();

                    foreach(var resultItem in readTask.Result.Objects)
                    {
                        ML.Usuario resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(resultItem.ToString());
                        usuario.Usuarios.Add(resultItemList);
                    }
                }
            }
            return View(usuario);
        }//Fin GetAll
        [HttpPost]
        public ActionResult GetAll(ML.Usuario usuario)
        {
            ServiceUsuario.UsuarioClient servicioPrueba = new ServiceUsuario.UsuarioClient();
            
            var resultUsuarios = servicioPrueba.GetById(usuario.IdUsuario);

            //ML.Result resultUsuarios = BL.Usuario.GetAllEF(usuario);

            if (resultUsuarios.Correct)
            {
                usuario.Usuarios = new List<object>();
                usuario.Usuarios = resultUsuarios.Objects.ToList();
            }
            else
            {
                ViewBag.Message = resultUsuarios.ErrorMessage;
                return PartialView("ModalPV");
            }
            return View(usuario);
        }
        [HttpGet]
        public ActionResult Form(int? IdUsuario)// " ? "  Este dato permite un null //Agregar y Actualizar
         {
             
             ML.Usuario usuario = new ML.Usuario();

             //IdUsuario = (IdUsuario == null) ? 0 : usuario.IdUsuario = IdUsuario.Value;
             ML.Result resultPaises = BL.Pais.GetAllEF();
             usuario.Direccion = new ML.Direccion();
             usuario.Direccion.Colonia = new ML.Colonia();
             usuario.Direccion.Colonia.Municipio = new ML.Municipio();
             usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
             usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
             usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;

             ML.Result resultRol = BL.Rol.GetAllEF();
             if (resultRol.Correct)
             {
                 usuario.Rol = new ML.Rol();
                 usuario.Rol.Roles = resultRol.Objects;

                 if (IdUsuario == null)//add
                 {
                     return View(usuario);
                 }
                 else //update
                 {  
                     //GeyById
                     //ML.Result result = new ML.Result();
                     //result = BL.Usuario.GetByIdEF(IdUsuario.Value);

                     //XML

                     //ServiceUsuario.UsuarioClient servicioPrueba = new ServiceUsuario.UsuarioClient();
                     //var resultGetById = servicioPrueba.GetById(IdUsuario.Value);

                     //GetById con JSON - API
                    
                    ML.Result resultUsuario = new ML.Result();
                    using (HttpClient client = new HttpClient())
                    {
                        string UrlApi = System.Configuration.ConfigurationManager.AppSettings["WebAPI"].ToString();
                        client.BaseAddress = new Uri(UrlApi);
                        var responseTask = client.GetAsync("Usuario/GetById/" + IdUsuario);
                        responseTask.Wait();
                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();
                            ML.Usuario resultItemList = new ML.Usuario();
                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Usuario>(readTask.Result.Object.ToString());
                            //usuario = resultItemList;
                            resultUsuario.Object = resultItemList;
                            resultUsuario.Correct = true;
                            //ML.Result resultUsuario = BL.Usuario.GetByIdEF(IdUsuario.Value);
                            //ServiceUsuario.UsuarioClient serviceUsuario = new ServiceUsuario.UsuarioClient();
                            //var resultUsuario = serviceUsuario.GetById(IdUsuario.Value);
                            if (resultUsuario.Correct)
                            {
                                usuario = ((ML.Usuario)resultUsuario.Object);
                                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
                                usuario.Rol.Roles = resultRol.Objects;
                                ML.Result resultEstados = BL.Estado.GetByIdEF(usuario.Direccion.Colonia.Municipio.Estado.Pais.IdPais);
                                ML.Result resultMunicipios = BL.Municipio.GetByIdEF(usuario.Direccion.Colonia.Municipio.Estado.IdEstado);
                                ML.Result resultColonias = BL.Colonia.GetByIdEF(usuario.Direccion.Colonia.IdColonia);

                                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;
                                usuario.Direccion.Colonia.Municipio.Estado.Estados = resultEstados.Objects;
                                usuario.Direccion.Colonia.Municipio.Municipios = resultMunicipios.Objects;
                                usuario.Direccion.Colonia.Colonias = resultColonias.Objects;

                                //usuario.Rol = new ML.Rol(); //Instancia antes de asignarle usuario al objeto 
                                //usuario = ((ML.Usuario)resultUsuario.Object);

                                return View(usuario);
                            }
                            else
                            {
                                ViewBag.Message = "Ha ocurrido un error"+ resultUsuario.ErrorMessage;
                            }
                        }
                    }
                }
            }

            return View();
        }
        [HttpPost]
        public ActionResult Form(ML.Usuario usuario)
        {
            ServiceUsuario.UsuarioClient servicioPrueba = new ServiceUsuario.UsuarioClient();
            
                //ML.Result result = new ML.Result();

                HttpPostedFileBase file = Request.Files["ImagenData"];

                if (file.ContentLength > 0)
                {
                    usuario.Imagen = ConvertToBytes(file);
                }
             if (ModelState.IsValid)
                        {
                if (usuario.IdUsuario == 0)
                {
                    ML.Result resultDireccion = BL.Direccion.Add(usuario.Direccion);

                    if (resultDireccion.Correct)
                    {
                        usuario.Direccion.IdDireccion = ((int)resultDireccion.Object); //unboxing de la direccion donde se asigna el ID

                        //var resultAdd = servicioPrueba.Add(usuario);
                        //result = BL.Usuario.AddEF(usuario);
                        using (var client = new HttpClient())
                        {
                            string urlApi = System.Configuration.ConfigurationManager.AppSettings["WebAPI"].ToString();
                            client.BaseAddress = new Uri(urlApi);

                            //HTTP POST
                            var postTask = client.PostAsJsonAsync<ML.Usuario>("Usuario/Add", usuario);
                            postTask.Wait();
                        
                            var resultAdd = postTask.Result;
                            if (resultAdd.IsSuccessStatusCode)
                            {
                                
                                    ViewBag.Mensaje = "El usuario ha sido insertado correctamente";
                                }
                                else
                                {
                                    ViewBag.Mensaje = "Ocurrió un error al insertar el usuario " + resultAdd.IsSuccessStatusCode;
                                }
                            
                        }
                    }
                }
                else //update 
                {
                    //var resultUpdate = servicioPrueba.Update(usuario);
                    ////result = BL.Usuario.UpdateEF(usuario);
                    //ML.Result resultDireccion = BL.Direccion.GetByIdEF(usuario.Direccion.Colonia.IdColonia);
                    //if (resultUpdate.Correct)
                    //{
                    //    ViewBag.Mensaje = "El Usuario se ha actualizado correctamente";
                    //}
                    //else
                    //{
                    //    ViewBag.Mensaje = "El Usuario no se ha actualizado correctamente " + resultUpdate.ErrorMessage;
                    //}

                    ML.Result resultDireccion = BL.Direccion.GetByIdEF(usuario.Direccion.Colonia.IdColonia);
                    
                        //result = BL.Usuario.UsuarioUpdateEF(usuario);
                        using (var client = new HttpClient())
                        {
                            string urlApi = System.Configuration.ConfigurationManager.AppSettings["WebAPI"].ToString();
                            client.BaseAddress = new Uri(urlApi);

                            var putTask = client.PutAsJsonAsync<ML.Usuario>("Usuario/Update/" + usuario.IdUsuario, usuario);

                            putTask.Wait();

                            var resultUpdate = putTask.Result;

                            if (resultUpdate.IsSuccessStatusCode)
                            {
                                ViewBag.Mensaje = "El usuario se ha actualizado correctamente";
                            }
                            else
                            {
                                ViewBag.Mensaje = "El usuario no se ha actualizado correctamente " + resultUpdate.IsSuccessStatusCode;
                            }
                        }
                }
            }
            else
            {
                ML.Result resultUsuario = BL.Usuario.GetAllEF(new ML.Usuario());

                ML.Result resultPaises = BL.Pais.GetAllEF();
                usuario.Direccion = new ML.Direccion();
                usuario.Direccion.Colonia = new ML.Colonia();
                usuario.Direccion.Colonia.Municipio = new ML.Municipio();
                usuario.Direccion.Colonia.Municipio.Estado = new ML.Estado();
                usuario.Direccion.Colonia.Municipio.Estado.Pais = new ML.Pais();
                usuario.Direccion.Colonia.Municipio.Estado.Pais.Paises = resultPaises.Objects;

                 ML.Result resultRol = BL.Rol.GetAllEF();
             
                 usuario.Rol = new ML.Rol();
                 usuario.Rol.Roles = resultRol.Objects;
                 return View(usuario);
            }
            return PartialView("ModalPV");
        }
        public byte[] ConvertToBytes(HttpPostedFileBase Imagen)
        {
            byte[] data = null;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Imagen.InputStream);
            data = reader.ReadBytes((int)Imagen.ContentLength);

            return data;
        }
        [HttpGet]
        public ActionResult Delete(int IdUsuario, int IdDireccion)
        {
            //Delete con XML

            //ServiceUsuario.UsuarioClient servicioPrueba = new ServiceUsuario.UsuarioClient();
            //var resultDelete= servicioPrueba.Delete(IdUsuario, IdDireccion);
            ////ML.Result result = BL.Usuario.DeleteEF(IdUsuario,IdDireccion);
            //if (resultDelete.Correct)
            //{
            //    ViewBag.Mensaje = "El usuario se ha eliminado correctamente";
            //}
            //else
            //{
            //    ViewBag.Mensaje = "El usuario no se ha eliminado correctamente " + resultDelete.ErrorMessage;
            //}

            //return PartialView("ModalPV");

            //Delete con JSON - API

            using (var client = new HttpClient())
            {
                string urlApi = System.Configuration.ConfigurationManager.AppSettings["WebAPI"].ToString();
                client.BaseAddress = new Uri(urlApi);

                //HTTP POST
                var postTask = client.DeleteAsync("Usuario/Delete/"+ IdUsuario +"/"+ IdDireccion);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "El usuario se ha eliminado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "El usuario no se ha eliminado correctamente " + result.IsSuccessStatusCode;
                }
           
            }
            return PartialView("ModalPV");
        }
        public JsonResult GetEstado(int IdPais)
        {
            var result = BL.Estado.GetByIdEF(IdPais);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetMunicipio(int IdEstado)
        {
            var result = BL.Municipio.GetByIdEF(IdEstado);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetColonia(int IdMunicipio)
        {
            var result = BL.Colonia.GetByIdEF(IdMunicipio);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateStatus(int IdUsuario)
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);
            if (result.Correct)
            {
                usuario = ((ML.Usuario)result.Object);

                usuario.Estatus = (usuario.Estatus) ? false : true;
                ML.Result resultUpdate = BL.Usuario.UpdateEF(usuario);

                ViewBag.Mensaje = "Se actualizo el estatus del usuario";

            }
            else
            {
                ViewBag.Mensaje = "No se pudo actualizar el estatus del usuario";
            }
            return PartialView("ModalPV");
        }

        //public ActionResult ImportExcel()
        //{
        //    return View();
        //}
        //[ActionName("Importexcel")]
        //[HttpPost]
        //public ActionResult Importexcel1()
        //{


        //    if (Request.Files["FileUpload1"].ContentLength > 0)
        //    {
        //        string extension = System.IO.Path.GetExtension(Request.Files["FileUpload1"].FileName).ToLower();
        //        string query = null;
        //        string connString = "";
        //        string[] validFileTypes = { ".xlsx" };

        //        string path1 = string.Format("{0}/{1}", Server.MapPath("~/Content/Uploads"), Request.Files["FileUpload1"].FileName);
        //        if (!Directory.Exists(path1))
        //        {
        //            Directory.CreateDirectory(Server.MapPath("~/Content/Uploads"));
        //        }
        //        if (validFileTypes.Contains(extension))
        //        {
        //            if (System.IO.File.Exists(path1))
        //            {
        //                System.IO.File.Delete(path1);
        //            }
        //            Request.Files["FileUpload1"].SaveAs(path1);
        //            if (extension.Trim() == ".xlsx")
        //            {
        //                connString = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + path1 + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
        //                DataTable dt = ConvertXSLXtoDataTable(path1, connString);
        //                ViewBag.Data = dt;
        //            }
        //        }
        //        else
        //        {
        //            ViewBag.Error = "El archivo a subir debe tener una extension .xlsx";
        //        }
        //    }
        //    return View();
        //}

        //public static DataTable ConvertXSLXtoDataTable(string strFilePath, string connString)
        //{
        //    OleDbConnection oledbConn = new OleDbConnection(connString);
        //    DataTable dt = new DataTable();
        //    DataSet ds = new DataSet();
        //    try
        //    {

        //        oledbConn.Open();
        //        using (DataTable Sheets = oledbConn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, null))
        //        {

        //            for (int i = 0; i < Sheets.Rows.Count; i++)
        //            {
        //                string worksheets = Sheets.Rows[i]["TABLE_NAME"].ToString();
        //                OleDbCommand cmd = new OleDbCommand(String.Format("SELECT * FROM [Sheet1$]", worksheets), oledbConn);
        //                OleDbDataAdapter oleda = new OleDbDataAdapter();
        //                oleda.SelectCommand = cmd;

        //                oleda.Fill(ds);
        //                                        ML.Result resultErrores = new ML.Result();
        //                                          resultErrores.Objects = new List<object>();
        //                                          string error = "";
        //                                          foreach (DataRow row in dt.Rows) //
        //                                        {

        //                                            if (row[0].ToString() == "")
        //                                            {
        //                                                error += "Falta el UserName del usuario";
        //                                            }
        //                                            if (row[1].ToString() == "")
        //                                            {
        //                                                error += "Falta el nombre del usuario";
        //                                            }
        //                                            if (row[2].ToString() == "")
        //                                            {
        //                                                error += "Falta el Apellido Paterno del usuario";
        //                                            }
        //                                            if (row[3].ToString() == "")
        //                                            {
        //                                                error += "Falta el Apellido Materno del usuario";
        //                                            }
        //                                            if (row[4].ToString() == "")
        //                                            {
        //                                                error += "Falta el Email del usuario";
        //                                            }
        //                                            if (row[5].ToString() == "")
        //                                            {
        //                                                error += "Falta el Sexo del usuario";
        //                                            }
        //                                            if (row[6].ToString() == "")
        //                                            {
        //                                                error += "Falta el Telefono del usuario";
        //                                            }
        //                                            if (row[7].ToString() == "")
        //                                            {
        //                                                error += "Falta el Celular del usuario";
        //                                            }
        //                                            if (row[8].ToString() == "")
        //                                            {
        //                                                error += "Falta la fecha de nacimiento del usuario";
        //                                            }
        //                                            if (row[9].ToString() == "")
        //                                            {
        //                                                error += "Falta el CURP del usuario";
        //                                            }
        //                                            resultErrores.Objects.Add(error);
        //                                        }
        //                                          resultErrores.Correct = true;
        //            }

        //            dt = ds.Tables[0];
        //        }

        //    }
        //    catch (Exception ex)
        //    {
        //    }
        //    finally
        //    {

        //        oledbConn.Close();
        //    }

        //    return dt;

        //}

        [HttpGet]
        public ActionResult CargaMasiva()
        {
            ML.Result result = new ML.Result();
            //result.Objects = new List<object>();
            return View(result);
        }
        [HttpPost]
        public ActionResult CargaMasiva(ML.Result result)
        {
            HttpPostedFileBase file = Request.Files["ExcelUsuarios"];
            string pathFolder = System.Configuration.ConfigurationManager.AppSettings["pathFolder"].ToString();

            if (result.Correct == false)
            {
                if (file.ContentLength > 0) //primera vez  extension, guardar, datatable-registros, información registros
                {
                    string extensionArchivo = Path.GetExtension(file.FileName).ToLower();
                    string extensionModulo = ConfigurationManager.AppSettings["TipoExcel"].ToString(); //xlsx

                    if (extensionArchivo == extensionModulo)
                    {    //FileName
                        //LayoutUsuarios + 11012021 + .xlsx

                        string direccionExcel = Server.MapPath(pathFolder) + Path.GetFileNameWithoutExtension(file.FileName) + '-' + DateTime.Now.ToString("yyyyMMddHHmmss") + ".xlsx";
                        if (!System.IO.File.Exists(direccionExcel))
                        {
                            //APP SETTINGS
                            file.SaveAs(direccionExcel);
                            string ConnectionString = System.Configuration.ConfigurationManager.AppSettings["ConexionExcel"].ToString() + direccionExcel;
                            ML.Result resultDataTable = BL.Usuario.GetAllByExcel(ConnectionString);

                            if (resultDataTable.Correct)
                            {
                                ML.Result resultValidacion = BL.Usuario.ValidarRegistros(resultDataTable.Objects);
                                if (resultValidacion.Objects.Count == 0)
                                {
                                    //Los registros ya fueron validados
                                    resultValidacion.Correct = true;
                                    Session["rutaArchivoExcel"] = direccionExcel;
                                }
                                return View(resultValidacion);
                            }
                            else
                            {
                                ViewBag.Mensaje = "El excel no tiene registros de usuario";
                            }
                        }
                        else
                        {
                            //borrar
                        }

                    }
                    else
                    {
                        ViewBag.Mensaje = "Seleccione un archivo con extensión .xlsx";
                    }
                }
                else
                {
                    ViewBag.Mensaje = "Seleccione un archivo";
                }
            }
            else
            {
                string rutaArchivoExcel = Session["rutaArchivoExcel"].ToString();
                string ConnectionString = ConfigurationManager.AppSettings["ConexionExcel"].ToString() + rutaArchivoExcel;

                ML.Result resultDataTable = BL.Usuario.GetAllByExcel(ConnectionString);

                ML.Result resultErrores = new ML.Result();
                resultErrores.Objects = new List<object>();

                foreach (ML.Usuario usuario in resultDataTable.Objects)
                {
                    ML.Result resultAdd = BL.Usuario.AddEF(usuario);
                    if (resultAdd.Correct == false)
                    {
                        resultErrores.Objects.Add(
                            "No se insertó el usuario con nombre: " + usuario.Nombre + " Apellidos:"
                            + usuario.ApellidoPaterno + " " + usuario.ApellidoMaterno +
                            " Error: " + resultAdd.ErrorMessage
                        );

                    }
                    else
                    {
                        ViewBag.Mensaje = "Hubo un/unos errores al subir los usuarios, Revise el LOG";
                    }

                }

                if (resultErrores.Objects.Count > 0)
                {
                    string direccionFileErrores = Server.MapPath(@"~/Errores/") + "Errores-" + DateTime.Now.ToString("yyyyMMddHHmmss") + ".txt";

                    TextWriter tw = new StreamWriter(direccionFileErrores);

                    foreach (string error in resultErrores.Objects)
                    {
                        tw.WriteLine(error);
                    }

                    tw.Close();

                }
                else
                {
                    ViewBag.Mensaje = "Usuarios correctamente agregados";
                }

            }
            // 

            return PartialView("ModalPV");
        }

    }
}