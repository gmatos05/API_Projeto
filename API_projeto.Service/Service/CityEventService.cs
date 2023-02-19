using API_projeto.Service.Dto;
using API_projeto.Service.Interface;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_projeto.Service.Service
{
    public class CityEventService :ICityEventService
    {
        private ICityEventRepository  _repository;
        private IMapper _mapper;

        public CityEventService(ICityEventRepository eventoRepository,IMapper mapper)
        {
            _repository = eventoRepository;
            _mapper = mapper;

        }
        public async  Task<bool> Inserir(CityEventDto cityEvent)
        {

            CityEventEntity entity = _mapper.Map<CityEventEntity>(cityEvent);
            
            return await _repository.InserirEvento(entity);
                
            
            

        }
        public async Task<bool>DeletarEvento(int id)
        {
            bool booleano  = await _repository.ConsultaReservas(id);
            if(booleano==true){
                return await _repository.ExcluirEvento(id);
            }
            return await _repository.Inativa(id);
        }
        public async Task<List<CityEventDto>> Consultar(string nome)
        {
            List<CityEventEntity> entity = await _repository.ConsultaTitulo(nome);
            if(entity ==null)
            {
                return null;
            }
           
            List<CityEventDto> cityEventdto = _mapper.Map<List<CityEventDto>>(entity);
            return cityEventdto;
        }
        public async Task<List<CityEventDto>> ConsultarLocalData(string local, DateTime data)
        {
            List<CityEventEntity> entity = await _repository.ConsultaLocalData(local, data);
            if (entity == null)
            {
                return null;
            }
            List<CityEventDto> cityEventdto = _mapper.Map<List<CityEventDto>>(entity);
            //return await _repository.ConsultaLocalData(local, data);
            return cityEventdto;
        }
        public async Task<List<CityEventDto>> ConsultaPrecoData(decimal minPrice, decimal maxPrice, DateTime data)
        {
            List<CityEventEntity> entity = await _repository.ConsultaPrecoData(minPrice,maxPrice,data);
            if (entity == null)
            {
                return null;
            }
            List<CityEventDto> cityEventdto = _mapper.Map<List<CityEventDto>>(entity);
            //return _repository.ConsultaPrecoData(minPrice, maxPrice, data);
            return cityEventdto;
        }
        public async Task<bool> AtualizarEvento(CityEventDto cityevent, int id)
        {
            CityEventEntity entity = _mapper.Map<CityEventEntity>(cityevent);
            return (await _repository.EditarEvento(entity, id));
            /* if (!_repository.EditarEvento(cityevent, id))
             {
                 return null;
             }
             return cityevent;*/
        }


    }
}
