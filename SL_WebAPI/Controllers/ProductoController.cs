using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebAPI.Controllers
{
    public class ProductoController : ApiController
    {
        // GET api/producto
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/producto/5
        [HttpGet]
        [Route("api/Producto/GetAll/")]
        public IHttpActionResult GetAll()
        {
            ML.Result result = BL.Producto.GetAllEF();

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        [HttpGet]
        [Route("api/Producto/GetById/{IdProducto}")]
        public IHttpActionResult GetById(int IdProducto)
        {
            ML.Result result = BL.Producto.GetByIdEF(IdProducto);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        // POST api/producto
        [HttpPost]
        [Route("api/Producto/Add")]
        public IHttpActionResult Add([FromBody] ML.Producto producto)
        {
            ML.Result result = BL.Producto.AddEF(producto);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        // PUT api/producto/5
        [HttpPut]
        [Route("api/Producto/Update/{IdProducto}")]
        public IHttpActionResult Update(int IdProducto, [FromBody]ML.Producto producto)
        {
            producto.IdProducto = IdProducto;
            ML.Result results = BL.Producto.UpdateEF(producto);
            if (results.Correct)
            {
                return Content(HttpStatusCode.OK, results);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, results);
            }
        }

        // DELETE api/producto/5
        [HttpDelete]
        [Route("api/Producto/Delete/{IdProducto}")]
        public IHttpActionResult Delete(int IdProducto)
        {
            ML.Result result = BL.Producto.DeleteEF(IdProducto);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }
    }
}
