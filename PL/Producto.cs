using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PL
{
    public class Producto
    {
        //public static void Delete()
        //{
        //    ServiceProducto.ProductoClient servicioPrueba = new ServiceProducto.ProductoClient();
        //    var resultDelete = servicioPrueba.Delete(80);

        //    if (resultDelete.Correct)
        //    {
        //        Console.WriteLine("El producto se elimino correctamente");
        //    }
        //    else
        //    {
        //        Console.WriteLine(resultDelete.ErrorMessage);
        //    }
        //}

        public static void GetAll()
        {
            ServiceProducto.ProductoClient producto = new ServiceProducto.ProductoClient();
            var resultGetAll = producto.GetAll();

            if (resultGetAll.Correct)
            {
                foreach (ML.Producto productoresult in resultGetAll.Objects.ToList())
                {
                    Console.WriteLine("IdProducto: " + productoresult.IdProducto);
                    Console.WriteLine("Nombre: " + productoresult.Nombre);
                    Console.WriteLine("Precio Unitario: " + productoresult.PrecioUnitario);
                    Console.WriteLine("Stock: " + productoresult.Stock);
                    Console.WriteLine("IdProveedor: " + productoresult.Proveedor.IdProveedor);
                    Console.WriteLine("IdDepartamento: " + productoresult.Departamento.IdDepartamento);
                    Console.WriteLine("Descripcion: " + productoresult.Descripcion);
                    Console.WriteLine("Imagen: " + productoresult.Imagen);


                    Console.WriteLine("--------");
                }
                Console.WriteLine("Los registros fueron recuperados exitosamente" ,resultGetAll.Objects);
            }
            else
            {
                Console.WriteLine("Ocurrió un error al traer la información del usuario: " + resultGetAll.ErrorMessage);
            }
        }
        public static void GetById()
        {
            Console.WriteLine("Ingrese el Id del producto");
            int IdProducto = int.Parse(Console.ReadLine());

            ServiceProducto.ProductoClient producto = new ServiceProducto.ProductoClient();
            var resultGetById = producto.GetById(IdProducto);

            if (resultGetById.Correct)
            {

                Console.WriteLine("IdProducto: " + ((ML.Producto)resultGetById.Object).IdProducto);
                Console.WriteLine("Nombre: " + ((ML.Producto)resultGetById.Object).Nombre);
                Console.WriteLine("Precio Unitario: " + ((ML.Producto)resultGetById.Object).PrecioUnitario);
                Console.WriteLine("Stock: " + ((ML.Producto)resultGetById.Object).Stock);
                Console.WriteLine("IdProveedor: " + ((ML.Producto)resultGetById.Object).Proveedor.IdProveedor);
                Console.WriteLine("IdDepartamento: " + ((ML.Producto)resultGetById.Object).Departamento.IdDepartamento);
                Console.WriteLine("Descripcion: " + ((ML.Producto)resultGetById.Object).Descripcion);
                Console.WriteLine("Imagen: " + ((ML.Producto)resultGetById.Object).Imagen);

                Console.WriteLine("El registro fue recuperado exitosamente", resultGetById.Object);
            }
            else
            {
                Console.WriteLine("Ocurrió un error al traer la información de producto: " + resultGetById.ErrorMessage);
            }
        }
        public static void Add()
        {
            ServiceProducto.ProductoClient servicioProducto = new ServiceProducto.ProductoClient();

            ML.Producto producto = new ML.Producto();


            Console.WriteLine("Ingrese el Nombre de producto");
            producto.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el Precio Unitario");
            producto.PrecioUnitario = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el Stock");
            producto.Stock = Console.ReadLine();

            Console.WriteLine("Ingrese el Id del Proveedor");
            producto.Proveedor = new ML.Proveedor();
            producto.Proveedor.IdProveedor = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el Id del Departamento");
            producto.Departamento = new ML.Departamento();
            producto.Departamento.IdDepartamento = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingresa la Descripcion del Producto");
            producto.Descripcion = Console.ReadLine();

            var resultAdd = servicioProducto.Add(producto);
            if (resultAdd.Correct)
            {
                Console.WriteLine("El Prodcuto se ha insertado correctamente");
            }
            else
            {
                Console.WriteLine("Ocurrio un error al insertar el Prodcuto " + resultAdd.ErrorMessage);
            }
        }
        public static void Update()
        {
            ServiceProducto.ProductoClient servicioProducto = new ServiceProducto.ProductoClient();

            ML.Producto producto = new ML.Producto();

            Console.WriteLine("Ingrese el Id del Producto que desea modificar");
            producto.IdProducto = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el Nombre de producto");
            producto.Nombre = Console.ReadLine();

            Console.WriteLine("Ingrese el Precio Unitario");
            producto.PrecioUnitario = decimal.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el Stock");
            producto.Stock = Console.ReadLine();

            Console.WriteLine("Ingrese el Id del Proveedor");
            producto.Proveedor = new ML.Proveedor();
            producto.Proveedor.IdProveedor = int.Parse(Console.ReadLine());

            Console.WriteLine("Ingrese el Id del Departamento");
            producto.Departamento = new ML.Departamento();
            producto.Departamento.IdDepartamento = int.Parse(Console.ReadLine());


            Console.WriteLine("Ingresa la Descripcion del Producto");
            producto.Descripcion = Console.ReadLine();

            var resultUpdate = servicioProducto.Update(producto);
            if (resultUpdate.Correct)
            {
                Console.WriteLine("El Producto se ha modificado correctamente");
            }
            else
            {
                Console.WriteLine("Ocurrio un error al modificar el Producto " + resultUpdate.ErrorMessage);
            }
        }
        public static void Delete()
        {
            ServiceProducto.ProductoClient servicioProducto = new ServiceProducto.ProductoClient();
            ML.Producto producto = new ML.Producto();

            Console.WriteLine("Ingrese el Id del prodcuto que desea eliminar");
            producto.IdProducto = int.Parse(Console.ReadLine());

            var resultDelete = servicioProducto.Delete(producto.IdProducto);

            if (resultDelete.Correct)
            {
                Console.WriteLine("Se ha eliminado el Producto correctamente");
            }
            else
            {
                Console.WriteLine("No se ha eliminado el Prodcuto correctamente " + resultDelete.ErrorMessage);
            }
        }
    }
}
