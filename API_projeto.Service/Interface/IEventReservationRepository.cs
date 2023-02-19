using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_projeto.Service.Interface
{
    public interface IEventReservationRepository
    {
        public Task<bool> InserirReserva(EventReservationEntity eventReservation);
        public Task<bool> EditarQuantidadeReserva(int numero, long idReservation);
        public Task<List<EventReservationEntity>> ConsultaPersonTitle(string nome, string tituloEvento);
        public Task<bool> Deletar(long id);
    }
}
