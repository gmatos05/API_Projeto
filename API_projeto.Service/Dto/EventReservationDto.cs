using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_projeto.Service.Dto
{
    public class EventReservationDto
    {
        [Required( ErrorMessage = "IDEVENT é obrigatório!")]
        [Range( 0, int.MaxValue, ErrorMessage ="Precisa informar um numero inteiro nao nulo " )]
        public long IdEvent { get; set; }
        [Required(ErrorMessage = "PERSONNAME é obrigatório!")]
        public string personName { get; set; }
        [Required(ErrorMessage = "QUANTITY é obrigatório!")]
        
        public long Quantity { get; set; }
    }
}
