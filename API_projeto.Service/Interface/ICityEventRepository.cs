using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_projeto.Service.Interface
{
    public interface ICityEventRepository
    {
        public Task<bool> InserirEvento(CityEventEntity cityevent);
        public Task<bool> EditarEvento(CityEventEntity cityevent, int id);
        public Task<List<CityEventEntity>> ConsultaTitulo(string nome);
        
        public Task<List<CityEventEntity>> ConsultaLocalData(string local, DateTime data);
        public Task<List<CityEventEntity>> ConsultaPrecoData(decimal minPrice, decimal maxPrice, DateTime data);
        public Task<bool> Inativa(int idEvento);
        public Task<bool> ExcluirEvento(int id);
        public Task<bool> ConsultaReservas(int idEvento);
    }
}
