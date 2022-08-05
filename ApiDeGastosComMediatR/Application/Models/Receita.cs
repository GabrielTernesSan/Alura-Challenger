using System.ComponentModel.DataAnnotations;

namespace ApiDeGastosComMediatR.Application.Models
{
    public class Receita
    {
        [Key]
        public int ReceitaId { get; set; }
        [Required, MinLength(5)]
        public string Descricao { get; set; }
        [Required]
        public double Valor { get; set; }
        [Required, MinLength(8)]
        public string Data { get; set; }
    }
}
