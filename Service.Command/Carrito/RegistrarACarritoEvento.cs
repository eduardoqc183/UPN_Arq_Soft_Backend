using Dto.Model.Dtos;
using Dto.Model.MemoryDb;
using MediatR;
using Persistencia.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Command.Carrito
{
    public class RegistrarACarritoEvento : carritocompra, INotification
    {
        public string NombreProducto { get; set; }
        public decimal Precio { get; set; }

        public RegistrarACarritoEvento(int carritoid, int usuarioId, int productoId, int cantidad, string nombre, decimal precio)
        {
            this.carritocompraid = carritoid;
            this.cantidad = cantidad;
            this.fecharegistro = DateTime.Now;
            this.productoid = productoId;
            this.usuarioid = usuarioId;
            this.Precio = precio;
            NombreProducto = nombre;
        }
    }

    public class RegistrarACarritoEventoHandler : INotificationHandler<RegistrarACarritoEvento>
    {
        private readonly CarritoDictionary carritoDictionary;

        public RegistrarACarritoEventoHandler(CarritoDictionary carritoDictionary)
        {
            this.carritoDictionary = carritoDictionary;
        }

        public Task Handle(RegistrarACarritoEvento notification, CancellationToken cancellationToken)
        {
            try
            {
                var c = new carritoconsultaDto
                {
                    Cantidad = notification.cantidad,
                    CarritoId = notification.carritocompraid,
                    Completado = notification.completado,
                    FechaRegistro = notification.fecharegistro,
                    Producto = notification.NombreProducto,
                    UsuarioId = notification.usuarioid,
                    Precio = notification.Precio
                };

                carritoDictionary.AddOrUpdate(notification.carritocompraid, c);
                return Task.CompletedTask;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
