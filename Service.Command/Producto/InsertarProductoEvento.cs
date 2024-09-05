using Dto.Model.Dtos;
using Dto.Model.MemoryDb;
using MediatR;
using Persistencia.Models;

namespace Service.Command.Producto
{
    public class InsertarProductoEvento : productoDto, INotification
    {
        public InsertarProductoEvento(productoDto p)
        {
            this.productoid = p.productoid;
            this.imagenurl = p.imagenurl;
            this.descripcion = p.descripcion;
            this.nombre = p.nombre;
            this.preciosinigv = p.preciosinigv;
            this.codigoproducto = p.codigoproducto;
        }
    }

    public class InsertarProductoEventoEventHandler : INotificationHandler<InsertarProductoEvento>
    {
        private readonly ProductDictionary _productDictionary;

        public InsertarProductoEventoEventHandler(ProductDictionary productDictionary)
        {
            _productDictionary = productDictionary;
        }

        public Task Handle(InsertarProductoEvento notification, CancellationToken cancellationToken)
        {
            _productDictionary.AddOrUpdate(notification.productoid, notification);
            return Task.CompletedTask;
        }
    }
}
