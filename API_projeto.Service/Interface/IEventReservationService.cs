using API_projeto.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_projeto.Service.Interface
{
    public interface IEventReservationService
    {
        public Task<bool> Inserir(EventReservationDto eventReservation);
        public Task<bool> EditarQuantidade(int numero, long idReservation);
        public Task<List<EventReservationDto>> ConsultaPersonTitle(string nome, string tituloEvento);
        
        public Task<bool> DeletarReserva(long idReservation);

    }
}
