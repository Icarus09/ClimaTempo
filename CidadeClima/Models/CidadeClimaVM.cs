using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace CidadeClima.Models
{
    public class CidadeClimaVM
    {
        public string Cidade { get; set; }

        public string EstadoUF { get; set; }

        public int TemperaturaMaxima { get; set; }

        public int TemperaturaMinima { get; set; }

        public string DiaSemana { get; set; }

        public string Clima { get; set; }

    }
}