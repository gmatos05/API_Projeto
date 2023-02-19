using API_projeto.Service.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_projeto.Service.Interface
{
    public interface ICityEventService
    {
        public Task<bool> Inserir(CityEventDto cityEvent);
        public Task<List<CityEventDto>> Consultar(string nome);
        public Task<List<CityEventDto>> ConsultarLocalData(string local, DateTime data);
        public Task<List<CityEventDto>> ConsultaPrecoData(decimal minPrice, decimal maxPrice, DateTime data);
        public Task<bool> AtualizarEvento(CityEventDto cityevent, int id);
        public Task<bool> DeletarEvento(int id);


    }
}
