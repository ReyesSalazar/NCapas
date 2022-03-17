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
    public class Producto
    {
        public static Result AddEF(ML.Producto producto)
        {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {
                    var query = context.ProductoAdd(producto.Nombre, producto.PrecioUnitario, producto.Stock, producto.Proveedor.IdProveedor, producto.Departamento.IdDepartamento, producto.Descripcion, producto.Imagen);

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
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
        }//Fin AddEF
        public static Result UpdateEF(ML.Producto producto)
        {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {
                    var query = context.ProductoUpdate(producto.Nombre, producto.PrecioUnitario, producto.Stock, producto.Proveedor.IdProveedor, producto.Departamento.IdDepartamento, producto.Descripcion, producto.Imagen, producto.IdProducto);
                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se actualizó el producto";
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
        public static Result DeleteEF(int IdProducto)
        {
            Result result = new Result();

            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {
                    var query = context.ProductoDelete(IdProducto);
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
        public static Result GetByIdEF(int IdProducto)
        {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {

                    var query = context.ProductoGetById(IdProducto).FirstOrDefault();

                    result.Objects = new List<object>();

                    if (query != null)
                    {

                        ML.Producto producto = new ML.Producto();
                        producto.IdProducto = query.IdProducto;
                        producto.Nombre = query.Nombre;
                        producto.PrecioUnitario = Convert.ToDecimal(query.PrecioUnitario);
                        producto.Stock = query.Stock;
                        producto.Proveedor = new ML.Proveedor();
                        producto.Proveedor.IdProveedor = query.IdProveedor.Value;
                        producto.Departamento = new ML.Departamento();
                        producto.Departamento.IdDepartamento = query.IdDepartamento.Value;
                        producto.Descripcion = query.Descripcion;
                        producto.Imagen = query.Imagen;

                        result.Object = producto;


                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "Ocurrió un error al obtener los registros en la tabla Producto";
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
                    var query = context.ProductoGetAll().ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = obj.IdProducto;
                            producto.Nombre = obj.NombreProducto;
                            producto.PrecioUnitario = Convert.ToDecimal(obj.PrecioUnitario);
                            producto.Stock = obj.Stock;
                            producto.Proveedor = new ML.Proveedor();
                            producto.Proveedor.IdProveedor = obj.IdProveedor.Value;
                            producto.Proveedor.Nombre = obj.ProveedorNombre;
                            producto.Proveedor.Telefono = obj.Telefono;
                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.IdDepartamento = obj.IdDepartamento.Value;
                            producto.Departamento.Nombre = obj.DepartamentoNombre;
                            producto.Descripcion = obj.Descripcion;
                            producto.Imagen = obj.Imagen; 

                            result.Objects.Add(producto);
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

        public static ML.Result GetByIdArea(int IdDepartamento)
           {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {

                    var query = context.DepartamentoGetByIdArea(IdDepartamento).ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {

                        foreach (var obj in query)
                        {
                            ML.Departamento departamento = new ML.Departamento();
                            departamento.IdDepartamento = obj.IdDepartamento;
                            departamento.Nombre = obj.Nombre;

                            result.Objects.Add(departamento);
                        }
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
        public static ML.Result GetByIdDepartamento(int IdDepartamento)
        {
            Result result = new Result();
            try
            {
                using (DL_EF.RSalazarProgramacionNCapasEntities context = new DL_EF.RSalazarProgramacionNCapasEntities())
                {

                    var query = context.ProductoGetByIdDepartamento(IdDepartamento).ToList();

                    result.Objects = new List<object>();

                    if (query != null)
                    {
                        foreach (var obj in query)
                        {
                            ML.Producto producto = new ML.Producto();
                            producto.IdProducto = obj.IdProducto;
                            producto.Nombre = obj.Nombre;
                            producto.PrecioUnitario = obj.PrecioUnitario.Value;
                            producto.Stock = obj.Stock;
                            producto.Proveedor = new ML.Proveedor();
                            producto.Proveedor.IdProveedor = obj.IdProveedor.Value;
                            producto.Departamento = new ML.Departamento();
                            producto.Departamento.IdDepartamento = obj.IdDepartamento.Value;
                            producto.Departamento.Nombre = obj.Nombre;
                            producto.Descripcion = obj.Descripcion;
                            producto.Imagen = obj.Imagen;

                        result.Objects.Add(producto);
                        }

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
    }
}
