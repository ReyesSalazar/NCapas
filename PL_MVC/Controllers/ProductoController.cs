using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Net.Mime;
using System.Data;
using System.Net.Http;

namespace PL_MVC.Controllers
{
    public class ProductoController : Controller
    {
        public static ML.Result ReadFile(HttpPostedFileBase file)
        {
            StreamReader TextFile = new StreamReader(file.InputStream);
            string line;
            bool FirstLine = true;

            ML.Result resultErrores = new ML.Result();
            resultErrores.Objects = new List<object>();

            while ((line = TextFile.ReadLine()) != null)
            {
                if (FirstLine)
                {
                    FirstLine = false;
                    line = TextFile.ReadLine();
                }
                string[] datos = line.Split('|');
                ML.Producto producto = new ML.Producto();
                producto.Nombre = datos[0];
                producto.PrecioUnitario = Decimal.Parse(datos[1]);
                producto.Stock = datos[2];
                producto.Proveedor = new ML.Proveedor();
                producto.Proveedor.IdProveedor = int.Parse(datos[3]);
                producto.Departamento = new ML.Departamento();
                producto.Departamento.IdDepartamento = int.Parse(datos[4]);
                producto.Descripcion = datos[5];

                ML.Result result = BL.Producto.AddEF(producto);
                if (result.Correct)
                {
                    result.Correct = true;
                }
                else
                {
                    resultErrores.Objects.Add("No se insertó el producto con Nombre: " + producto.Nombre + " " + producto.Descripcion//+ " Precio Unitario" 
                        //+ producto.PrecioUnitario + " Stock" + producto.Stock + "Proveedor" + producto.Proveedor.IdProveedor +
                        //"Departamento" +producto.Departamento.IdDepartamento 
                                                    + " Error: " + result.ErrorMessage);
                }
            }
            return resultErrores;

        }
        [HttpGet] // Mostrar views
        public ActionResult GetAll() //Mostrar una tabla con la información de las materias
        { //El método se debe de llamar igual que la vista

            //GetAll con XML

            //ServiceProducto.ProductoClient servicioProducto = new ServiceProducto.ProductoClient();
            //ML.Producto producto = new ML.Producto();
            //var resultProductos = servicioProducto.GetAll();
            ////ML.Producto producto = new ML.Producto();

            ////ML.Result result = BL.Producto.GetAllEF();
            //ML.Result resultAreas = BL.Area.GetAllEF();

            //if (resultProductos.Correct)
            //{
            //    producto.Productos = new List<object>();
            //    producto.Productos = resultProductos.Objects.ToList();


            //    if (resultAreas.Correct)
            //    {
            //        producto.Departamento = new ML.Departamento();
            //        producto.Departamento.Area = new ML.Area();
            //        producto.Departamento.Area.Areas = resultAreas.Objects;
            //        return View(producto);
            //    }
            //    else
            //    {
            //        ViewBag.Message = resultAreas.ErrorMessage;
            //        return PartialView("ModalPV");
            //    }
            //}
            //else
            //{
            //    ViewBag.Message = resultProductos.ErrorMessage;
            //    return PartialView("ModalPV");
            //}

            //GetAll con JSON -API

            ML.Result resultAreas = BL.Area.GetAllEF();
            ML.Producto producto = new ML.Producto();
            producto.Productos = new List<object>();

            using (var client = new HttpClient())
            {
                if (resultAreas.Correct)
                {
                    producto.Departamento = new ML.Departamento();
                    producto.Departamento.Area = new ML.Area();
                    producto.Departamento.Area.Areas = resultAreas.Objects;
                }
                string urlApi = System.Configuration.ConfigurationManager.AppSettings["WebAPI"].ToString();
                client.BaseAddress = new Uri(urlApi);

                var responseTask = client.GetAsync("Producto/GetAll");
                responseTask.Wait();

                var result = responseTask.Result;

                if (result.IsSuccessStatusCode)
                {
                    var readTask = result.Content.ReadAsAsync<ML.Result>();

                    foreach (var resultItem in readTask.Result.Objects)
                    {
                        ML.Producto resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(resultItem.ToString());
                        producto.Productos.Add(resultItemList);
                    }
                }
            }
            return View(producto);
        }
        [HttpPost]
        public ActionResult GetAll(ML.Producto producto) //Mostrar una tabla con la información de las materias
        {
            ML.Result resultAreas = BL.Area.GetAllEF();
            ML.Result resultDepartamento = BL.Departamento.GetAllEF();
            ML.Result result = BL.Producto.GetByIdDepartamento(producto.Departamento.IdDepartamento);


            if (result.Correct)
            {
                producto.Productos = new List<object>();
                producto.Productos = result.Objects;


                if (resultAreas.Correct)
                {

                    producto.Departamento.Area.Areas = new List<object>();
                    producto.Departamento.Departamentos = resultDepartamento.Objects;
                    producto.Departamento.Area.Areas = resultAreas.Objects;

                    return View(producto);
                }
                else
                {
                    ViewBag.Message = resultAreas.ErrorMessage;
                    return PartialView("ModalPV");
                }

            }
            else
            {
                ViewBag.Message = result.ErrorMessage;
                return PartialView("ModalPV");
            }
        }
        public FileResult DescargarArchivo()
        {
            return File(Session["RutaDeDescarga"].ToString(), MediaTypeNames.Application.Octet, "document.txt");
        }
        [HttpGet]
        public ActionResult Form(int? IdProducto)// " ? "  Este dato permite un null //Agregar y Actualizar
        {
            ML.Producto producto = new ML.Producto();
            ML.Result resultDepartamento = BL.Departamento.GetAllEF();
            if (resultDepartamento.Correct)
            {
                producto.Departamento = new ML.Departamento();
                producto.Departamento.Departamentos = resultDepartamento.Objects;
                if (IdProducto == null)//add
                {
                    return View(producto);
                }
                else //update
                {
                    //GeyById

                    //GetById con XML

                    //ServiceProducto.ProductoClient servicioProducto = new ServiceProducto.ProductoClient();
                    //var resultProductos = servicioProducto.GetById(IdProducto.Value);

                    ////ML.Result result = new ML.Result();
                    ////result = BL.Producto.GetByIdEF(IdProducto.Value);

                    //if (resultProductos.Correct)
                    //{
                    //    producto = ((ML.Producto)resultProductos.Object);
                    //    producto.Departamento.Departamentos = resultDepartamento.Objects;
                    //    return View(producto);
                    //}
                    //else
                    //{

                    //}

                    //GetById con JSON - API
                    string UrlApi = System.Configuration.ConfigurationManager.AppSettings["WebAPI"].ToString();
                    using (HttpClient client = new HttpClient())
                    {
                        client.BaseAddress = new Uri(UrlApi);
                        var responseTask = client.GetAsync("Producto/GetById/" + IdProducto);
                        responseTask.Wait();
                        var result = responseTask.Result;
                        if (result.IsSuccessStatusCode)
                        {
                            var readTask = result.Content.ReadAsAsync<ML.Result>();
                            readTask.Wait();
                            ML.Producto resultItemList = new ML.Producto();
                            resultItemList = Newtonsoft.Json.JsonConvert.DeserializeObject<ML.Producto>(readTask.Result.Object.ToString());
                            producto = resultItemList;
                            producto.Departamento.Departamentos = resultDepartamento.Objects;
                        }
                    }
                }
            } return View(producto);
        }
        [HttpPost]
        public ActionResult Form(ML.Producto producto)
        {
            //ServiceProducto.ProductoClient servicioProducto = new ServiceProducto.ProductoClient();
           
            //ML.Result result = new ML.Result();

            HttpPostedFileBase file = Request.Files["ImagenData"];

            if (file.ContentLength > 0)
            {
                producto.Imagen = ConvertToBytes(file);
            }

            if (producto.IdProducto == 0)
            {
               
                //var resultProductos = servicioProducto.Add(producto);
                //result = BL.Producto.AddEF(producto);
                using (var client = new HttpClient())
                {
                    string urlApi = System.Configuration.ConfigurationManager.AppSettings["WebAPI"].ToString();
                    client.BaseAddress = new Uri(urlApi);

                    //HTTP POST
                    var postTask = client.PostAsJsonAsync<ML.Producto>("Producto/Add", producto);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "El Producto se ha registrado correctamente";
                    }
                    else
                    {
                        ViewBag.Mensaje = "El Producto no se ha registrado correctamente " + result.IsSuccessStatusCode;
                    }
                    
                }
                return View("ModalPV");

                //XML 

                //if (resultProductos.Correct)
                //{
                //    ViewBag.Mensaje = "El Producto se ha registrado correctamente";
                //}
                //else
                //{
                //    ViewBag.Mensaje = "El Producto no se ha registrado correctamente " + resultProductos.ErrorMessage;
                //}

            }
            else
            {
                //var resultProductos = servicioProducto.Update(producto);
                //result = BL.Producto.UpdateEF(producto);
                using (var client = new HttpClient())
                {
                    string urlApi = System.Configuration.ConfigurationManager.AppSettings["WebAPI"].ToString();
                    client.BaseAddress = new Uri(urlApi);

                    //HTTP PUT
                    var postTask = client.PutAsJsonAsync<ML.Producto>("Producto/Update/"+ producto.IdProducto,producto);
                    postTask.Wait();

                    var result = postTask.Result;
                    if (result.IsSuccessStatusCode)
                    {
                        ViewBag.Mensaje = "El Producto se ha actualizado correctamente";
                    }
                    else
                    {
                        ViewBag.Mensaje = "El Producto no se ha actualizado correctamente " + result.IsSuccessStatusCode;
                    }
                }

                //XML

                //if (resultProductos.Correct)
                //{
                //    ViewBag.Mensaje = "El Producto se ha actualizado correctamente";
                //}
                //else
                //{
                //    ViewBag.Mensaje = "El Producto no se ha actualizado correctamente " + resultProductos.ErrorMessage;
                //}

            }
            return View("ModalPV");
        }
        public byte[] ConvertToBytes(HttpPostedFileBase Imagen)
        {
            byte[] data = null;
            System.IO.BinaryReader reader = new System.IO.BinaryReader(Imagen.InputStream);
            data = reader.ReadBytes((int)Imagen.ContentLength);

            return data;
        }
        [HttpGet]
        public ActionResult Delete(int IdProducto)
        {
            //ServiceProducto.ProductoClient servicioProducto = new ServiceProducto.ProductoClient();

            ML.Producto producto = new ML.Producto();
            producto.IdProducto = IdProducto;
            //ML.Result result = BL.Producto.DeleteEF(IdProducto);
            
            //Delete con XML 

            //var resultProductos = servicioProducto.Delete(IdProducto);

            //if (resultProductos.Correct)
            //{
            //    ViewBag.Mensaje = "El producto se ha eliminado correctamente";
            //}
            //else
            //{
            //    ViewBag.Mensaje = "El producto no se ha eliminado correctamente " + resultProductos.ErrorMessage;
            //}

            //return PartialView("ModalPV");

            //Delete con JSON - API

            using (var client = new HttpClient())
            {
                string urlApi = System.Configuration.ConfigurationManager.AppSettings["WebAPI"].ToString();
                client.BaseAddress = new Uri(urlApi);

                //HTTP POST
                var postTask = client.DeleteAsync("Producto/Delete/" + IdProducto);
                postTask.Wait();

                var result = postTask.Result;
                if (result.IsSuccessStatusCode)
                {
                    ViewBag.Mensaje = "El producto se ha eliminado correctamente";
                }
                else
                {
                    ViewBag.Mensaje = "El producto no se ha eliminado correctamente " + result.IsSuccessStatusCode;
                }
            }
            return PartialView("ModalPV");
        }
        public JsonResult GetDepartamento(int IdArea)
        {
            var result = BL.Producto.GetByIdArea(IdArea);
            return Json(result.Objects, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult Carrito(int? IdProducto)
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<object>();
            if (IdProducto != null)
            {
                if (Session["Carrito"] == null)
                {
                    ML.VentaProducto ventaProdcuto = new ML.VentaProducto();

                    ventaProdcuto.Producto = new ML.Producto();
                    ventaProdcuto.Producto.IdProducto = IdProducto.Value;

                    ventaProdcuto.Cantidad = 1;

                    ML.Result resultProducto = BL.Producto.GetByIdEF(IdProducto.Value);

                    if (resultProducto.Correct)
                    {
                        ventaProdcuto.Producto = (ML.Producto)resultProducto.Object;
                        result.Objects.Add(ventaProdcuto);
                    }
                    Session["Carrito"] = result.Objects;
                }
                else
                {//Se comprueba si ya existe informacion en el carrito
                    result.Objects = (List<Object>)Session["Carrito"];

                    bool Existe = false;
                    var indice = 0;//variable para el index

                    foreach (ML.VentaProducto ventaProducto in result.Objects)
                    {//recorre el objeto venta producto
                        if (ventaProducto.Producto.IdProducto == IdProducto)
                        {//compara el id del producto con el de ventaProducto
                            Existe = true;
                            indice = result.Objects.IndexOf(ventaProducto);
                        }
                    }
                    if (Existe == true)
                    {
                        foreach (ML.VentaProducto ventaProducto in result.Objects)
                        {
                            ventaProducto.Cantidad = ventaProducto.Cantidad + 1;//contador
                        }
                    }
                    else
                    {
                        ML.VentaProducto ventaProducto = new ML.VentaProducto();

                        ventaProducto.Producto = new ML.Producto();
                        ventaProducto.Producto.IdProducto = IdProducto.Value;
                        ventaProducto.Cantidad = 1;

                        ML.Result resultProducto = BL.Producto.GetByIdEF(IdProducto.Value);

                        if (resultProducto.Correct)
                        {
                            ventaProducto.Producto = (ML.Producto)resultProducto.Object;
                            result.Objects.Add(ventaProducto);
                        }
                        Session["Carrito"] = result.Objects;
                    }
                }
            }
                else
                {
                    ViewBag.Mensaje = "Ocurrio un error al obtener la informacion del carrito" + result.ErrorMessage;
                    return PartialView("ModalPV");
            }
            return View(result);
        }//Fin de carrito
        public ActionResult DeleteCarrito(int IdVentaProducto)//Elimina los productos del carrito
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();
            result.Objects = (List<Object>)Session["Carrito"];
            int contador = 0;

            if (result.Objects.Count == 0)
            {
                return View("Carrito", result);// si no hay productos en el carrito
            }
            else
            {
                foreach (ML.VentaProducto producto in result.Objects.ToList())
                {
                    contador++;
                    if (producto.IdVentaProducto == IdVentaProducto)
                    {
                        break;
                    }
                    else
                    {
                        ViewBag.Mensaje = "El producto no se ha podido eliminar del carrito " + result.ErrorMessage;
                    }
                }

                result.Objects.RemoveAt(contador - 1);

                return View("Carrito", result);
            }

        }//fin delete carrito
        public ActionResult Sumar(int IdVentaProducto)//Incrementa los productos del carrito
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();
            result.Objects = (List<Object>)Session["Carrito"];

            foreach (ML.VentaProducto ventaProducto in result.Objects.ToList())
            {
                if (ventaProducto.IdVentaProducto == IdVentaProducto)
                {
                    ventaProducto.Cantidad++;
                    break;
                }
                else
                {
                    ViewBag.Mensaje = "El producto no se ha podido incrementar " + result.ErrorMessage;
                }
            }

            return View("Carrito", result);

        }//Fin de sumar
        public ActionResult Restar(int IdVentaProducto)//Decrementa los productos del carrito
        {
            ML.Result result = new ML.Result();
            result.Objects = new List<Object>();
            result.Objects = (List<Object>)Session["Carrito"];

            int contador = 0;
            bool comprobar = false;
            foreach (ML.VentaProducto ventaProducto in result.Objects.ToList())
            {
                contador++;
                if (ventaProducto.IdVentaProducto == IdVentaProducto)
                {
                    ventaProducto.Cantidad--;
                    comprobar = false;
                    if (ventaProducto.Cantidad <= 0)
                    {
                        comprobar = true;
                    }
                    break;
                }
                else
                {
                    ViewBag.Mensaje = "El producto no se ha podido decrementar " + result.ErrorMessage;
                }

            }

            if (comprobar == true)
            {
                result.Objects.RemoveAt(contador - 1);
            }

            return View("Carrito", result);

        }//Fin de restar

    }
}