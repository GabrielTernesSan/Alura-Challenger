using System.ComponentModel.DataAnnotations;

namespace ApiDeGastosComMediatR.Application.Models
{
    public class Despesa
    {
        [Key]
        public int DespesaId { get; set; }

        [Required, MinLength(3)]
        public string Descricao { get; set; }

        [Required]
        public double Valor { get; set; }

        [Required, MinLength(8)]
        public string Data { get; set; }
    }
}
