using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebAPI.Controllers
{
    public class UsuarioController : ApiController
    {
        // GET api/usuario
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET api/usuario/5

        [HttpGet]
        [Route("api/Usuario/GetAll/")]
        public IHttpActionResult GetAll()
        {
            ML.Usuario usuario = new ML.Usuario();
            ML.Result result = BL.Usuario.GetAllEF(usuario);

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
        [Route("api/Usuario/GetById/{IdUsuario}")]
        public IHttpActionResult GetById(int IdUsuario)
        {
            ML.Result result = BL.Usuario.GetByIdEF(IdUsuario);

            if(result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        // POST api/usuario
        [HttpPost]
        [Route("api/Usuario/Add")]
        public IHttpActionResult Add([FromBody] ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.AddEF(usuario);

            if (result.Correct)
            {
                return Content(HttpStatusCode.OK, result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        // PUT api/usuario/5
        [HttpPut]
        [Route("api/Usuario/Update/{IdUsuario}")]
        public IHttpActionResult Update(int IdUsuario,[FromBody] ML.Usuario usuario)
        {
            usuario.IdUsuario = IdUsuario;
            ML.Result results = BL.Usuario.UpdateEF(usuario);
            if (results.Correct)
            {
                return Content(HttpStatusCode.OK, results);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, results);
            }
        }

        // DELETE api/usuario/5
        [HttpDelete]
        [Route("api/Usuario/Delete/{IdUsuario}/{IdDireccion}")]
        public IHttpActionResult Delete(int IdUsuario, int IdDireccion)
        {
            //GetById - Usuario  obtener su IdDireccion

            ML.Result result = BL.Usuario.DeleteEF(IdUsuario, IdDireccion);
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
