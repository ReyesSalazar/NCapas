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
    public class Departamento
    {
        public static Result AddEF(ML.Departamento departamento)
        {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {
                    var query = context.DepartamentoAdd(departamento.Nombre, departamento.Area.IdArea);

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
                    }

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }
            return result;
        }//Fin AddEF
        public static Result UpdateEF(ML.Departamento departamento)
        {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {
                    var query = context.DepartamentoUpdate(departamento.Nombre, departamento.Area.IdArea, departamento.IdDepartamento);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizó el status de la credencial";
                    }
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }//Fin UpdateEF
        public static Result DeleteEF(int IdDepartamento)
        {
            Result result = new Result();

            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {
                    var query = context.DepartamentoDelete(IdDepartamento);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se eliminó el registro";
                    }

                    result.Correct = true;
                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }

            return result;
        }//Fin DeleteEF
        public static Result GetByIdEF(int IdDepartamento)
        {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {

                    var query = context.DepartamentoGetById(IdDepartamento).FirstOrDefault();

                    result.Objects = new List<object>();

                    if (query != null)
                    {

                        ML.Departamento departamento = new ML.Departamento();
                        departamento.IdDepartamento = query.IdDepartamento;
                        departamento.Nombre = query.Nombre;
                        departamento.Area = new ML.Area();
                        departamento.Area.IdArea = query.IdArea.Value;


                        result.Object = departamento;


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Departamento";
                    }

                }


            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
            }

            return result;
        }//Fin GetByIdEF
        public static Result GetAllEF()
        {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {
                    var query = context.DepartamentoGetAll().ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Nombre = obj.DepartamentoNombre;
                            departamento.Area = new ML.Area();
                            departamento.Area.IdArea = obj.IdArea.Value;
                            departamento.Area.Nombre = obj.AreaNombre;


                            result.Objects.Add(departamento);
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
