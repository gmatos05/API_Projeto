using API_projeto.Repository;
using API_projeto.Service.Dto;
using API_projeto.Service.Interface;
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
    public class EventReservationController : ControllerBase
    {
        public IEventReservationService _EventReservationService;

        public EventReservationController(IEventReservationService CityEventService)
        {
            _EventReservationService = CityEventService;
        }

        //Construtor

        [HttpPost("Inserir")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Inserir(EventReservationDto eventReservation)
        {
            if (!await _EventReservationService.Inserir(eventReservation) == null)
            {
                return BadRequest();
            }

            return CreatedAtAction(nameof(ConsultaPersonTitle), eventReservation);
            /*if (_EventReservationService.Inserir(eventReservation)==null)
             {
                 return BadRequest();
             }
             return CreatedAtAction(nameof(Inserir), eventReservation);*/
        }
        [HttpPatch("Atualizar")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
            public async Task<IActionResult> EditarQuantidade(int numero, long idReservation)
            {
            if (!await _EventReservationService.EditarQuantidade(numero, idReservation))
            {
                return BadRequest();
            }

            return NoContent();
            /*if (_EventReservationService.EditarQuantidade(numero, idReservation)==null)
                {
                    return BadRequest();
                }

                return Ok(_EventReservationService.EditarQuantidade(numero, idReservation));*/
            }
        [HttpGet("Consulta")]
        //[Authorize]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public async Task<ActionResult<List<EventReservationDto>>> ConsultaPersonTitle(string nome, string tituloEvento)
        {
            
            return Ok(await _EventReservationService.ConsultaPersonTitle(nome,tituloEvento));
        }
        [HttpDelete("Deletar")]
        [Authorize(Roles = "admin")]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        public async Task<IActionResult> DeletarReserva(long idReservation)
        {
            if (_EventReservationService.DeletarReserva(idReservation) == null)
            {
                return BadRequest();
            }

            return Ok(_EventReservationService.DeletarReserva(idReservation));
        }
    }
}
