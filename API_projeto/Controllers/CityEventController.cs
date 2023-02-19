using API_projeto.Filter;
using API_projeto.Repository;
using API_projeto.Service.Dto;
using API_projeto.Service.Interface;
using API_projeto.Service.Service;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Data;

namespace API_projeto.Controllers
{

    [ApiController]
    [Route("[controller]")]
    [Consumes("application/json")]
    [Produces("application/json")]
    [TypeFilter(typeof(ExcecaoGeralFilter))]
    public class CityEventController : ControllerBase
    {
        public ICityEventService _CityEventService;

        public CityEventController(ICityEventService CityEventService)
        {
            _CityEventService = CityEventService;
        }

        

        [HttpPost("Inserir")]

         [Authorize(Roles = "admin")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Inserir(CityEventDto cityEvent )
        {
            if (!await _CityEventService.Inserir(cityEvent))
            {
                return BadRequest();
            }

           return CreatedAtAction(nameof(Consultar), cityEvent);
            /*
           _CityEventService.Inserir(cityEvent);

           return CreatedAtAction(nameof(Inserir), cityEvent);

           */
        }
        [HttpGet("Consultar/Titulo")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Consultar(string nome)
        {
                return Ok(_CityEventService.Consultar(nome));
            
        }
        [HttpGet("Consultar/Local/Data")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConsultarLocalData(string local, DateTime data)
        {


            return Ok(_CityEventService.ConsultarLocalData(local,data));


        }
        //ConsultaPrecoData
        [HttpGet("Consultar/Preco/Data")]
        
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> ConsultaPrecoData(decimal minPrice, decimal maxPrice,DateTime data)
        {


            return Ok(_CityEventService.ConsultaPrecoData(minPrice,maxPrice, data));


        }
        [HttpPut("Atualizar")]

        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task< IActionResult> AtualizarEvento(CityEventDto cityevent, int id)
        {
            if (!await _CityEventService.AtualizarEvento(cityevent, id))
            {
                return BadRequest();
            }

            return NoContent();
            
        }
        [HttpDelete("Deletar")]

        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> DeletarEvento(int idEvent){
            if(await _CityEventService.DeletarEvento(idEvent)==false){
                return BadRequest();
            }
            return NoContent();
        }
    }
}