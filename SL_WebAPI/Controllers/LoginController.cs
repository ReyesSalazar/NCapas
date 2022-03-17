using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SL_WebAPI.Controllers
{
    public class LoginController : ApiController
    {
        // POST api/Login
        [HttpPost]
        [Route("api/Login/")]
        public IHttpActionResult Login([FromBody]ML.Usuario usuario)
        {
            ML.Result result = BL.Usuario.UserNameGetById(usuario);
            if (result.Correct)
            {
                return Content(HttpStatusCode.OK,result);
            }
            else
            {
                return Content(HttpStatusCode.NotFound, result);
            }
        }
        
        [HttpPost]
        [Route("api/Login/Authenticate")]
        public IHttpActionResult Authenticate([FromBody]ML.Usuario usuario) //Ingresan formulario
        {
            ML.Usuario usuarioItem = new ML.Usuario();
            ML.Result result = BL.Usuario.UserNameGetById(usuario);
            usuarioItem = (ML.Usuario)result.Object;//BL Con datos correctos


            if (result.Correct)
            {
                if (usuario.UserName == usuarioItem.UserName)
                {
                    if (usuario.Password == usuarioItem.Password)
                    {
                        usuarioItem.Token = SL_WebAPI.Controllers.TokenGeneratorController.TokenGenerator.GenerateTokenJwt(usuario.UserName);
                        result.Correct = true;
                        result.Object = usuarioItem;
                        return Ok(result);
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Contraseña incorrecta";
                        return Content(HttpStatusCode.Unauthorized, result);
                    }
                }
                else
                {
                    result.Correct = false;
                    result.ErrorMessage = "Usuario incorrecto";
                    return Content(HttpStatusCode.Unauthorized, result);
                }
            }
            else
            {
                result.ErrorMessage = "No se encontró el usuario";
                return Content(HttpStatusCode.NotFound, result);
            }
        }

        
    }
}
