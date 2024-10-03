using Dto.Model.MemoryDb;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Command.Carrito
{
    public class EliminarDeCarritoEvento : INotification
    {
        public int carritoId { get; }

        public EliminarDeCarritoEvento(int carritoId)
        {
            this.carritoId = carritoId;
        }
    }

    public class EliminarDeCarritoEventoEventHandler : INotificationHandler<EliminarDeCarritoEvento>
    {
        private readonly CarritoDictionary _carritoDictionary;

        public EliminarDeCarritoEventoEventHandler(CarritoDictionary carritoDictionary)
        {
            _carritoDictionary = carritoDictionary;
        }

        public Task Handle(EliminarDeCarritoEvento notification, CancellationToken cancellationToken)
        {
            try
            {                
                _carritoDictionary.Delete(notification.carritoId);
                return Task.CompletedTask;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
