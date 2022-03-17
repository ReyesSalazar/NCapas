using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using ML;
using DL_EF;
using System.Data.Entity.Core;
namespace BL
{
    public class Pais
    {
        public static Result GetAllEF()
        {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {
                    var query = context.PaisGetAll().ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Pais pais = new ML.Pais();
                            pais.IdPais = obj.IdPais;
                            pais.Nombre = obj.Nombre;


                            result.Objects.Add(pais);
                        }

                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se encontraron registros.";
                    }
                }
            }
            catch (Exception ex)
            {

                result.Correct = false;
                result.ErrorMessage = ex.Message;

            }

            return result;
        } //Fin GetAllEF
    }
}
