using Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IProductoService
    {
        bool Registrar(Producto producto);

        List<Producto> Listar();

        Producto? BuscarPorCodigo(string codigo);

        bool ExisteCodigo(string codigo);

        bool ActualizarStock(int idProducto, int cantidad);
    }
}
