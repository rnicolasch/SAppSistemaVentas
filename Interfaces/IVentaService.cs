using Modelos;
using System;
using System.Collections.Generic;
using System.Text;

namespace Interfaces
{
    public interface IVentaService
    {
        bool Registrar(Venta venta);

        List<Venta> Listar();

        decimal ObtenerTotalVentas();
    }
}
