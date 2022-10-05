using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CidadeClima.Models
{
    public partial class Cidade
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Nome { get; set; }

        [ForeignKey("estado_id")]
        public int EstadoId { get; set; }
        public virtual Estado Estado { get; set; }
    }
}
