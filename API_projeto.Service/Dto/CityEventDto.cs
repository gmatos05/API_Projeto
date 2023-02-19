using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace API_projeto.Service.Dto
{
    public class CityEventDto
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Nome é obrigatório!")]
        [MaxLength(255)]
        public string Title { get; set; }

        public string? Description { get; set; }
        [Required(ErrorMessage = "Data é obrigatório!")]
        public DateTime DateHourEvent { get; set; }
       [Required(ErrorMessage = "Local é obrigatório!")]
        public string Local { get; set; }
        public string? Address { get; set; }
        [Required( ErrorMessage = "Preço é obrigatório!")]
        public decimal? Price { get; set; }
        public bool Status { get; set; }


    }
}
