using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace CidadeClima.Models
{
    public class PrevisaoClimaVM
    {
        public PrevisaoClima PrevisaoClima { get; set;}

        public List<CidadeClimaVM>  CidadesMaisQuentes { get; set; }

        public List<CidadeClimaVM> CidadesMaisFrias { get; set; }

        public int CidadeId { get; set; }

        public SelectList Cidades { get; set;}

    }
}