using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using CidadeClima.Data;
using CidadeClima.Models;
using Microsoft.EntityFrameworkCore;


namespace CidadeClima.Controllers
{
    public class PrevisaoClimasController : Controller
    {
        private ClimaTempoSimplesContext db = new ClimaTempoSimplesContext();

        // GET: PrevisaoClimas
        public ActionResult Index()
        {
            var Dia = DefinirDia();

            var MaisQuentes = db.PrevisaoClima.Include(c => c.Cidade).ThenInclude(e=> e.Estado)
                                               .Where(s => s.DataPrevisao == Dia)
                                               .OrderByDescending(t => t.TemperaturaMaxima)
                                               .Select(z => new PrevisaoClima
                                               {
                                                   Cidade = z.Cidade,
                                                   TemperaturaMaxima = z.TemperaturaMaxima,
                                                   TemperaturaMinima = z.TemperaturaMinima
                                                  
                                               }).ToList();

            var MaisFrias = db.PrevisaoClima.Include(c => c.Cidade).ThenInclude(e => e.Estado)
                                               .Where(s => s.DataPrevisao == Dia)
                                               .OrderBy(t => t.TemperaturaMaxima)
                                               .Select(z => new PrevisaoClima 
                                               {

                                                   Cidade = z.Cidade,
                                                   TemperaturaMaxima = z.TemperaturaMaxima,
                                                   TemperaturaMinima = z.TemperaturaMinima
                                               }).ToList();

            var previsaoclima = new PrevisaoClimaVM();
            previsaoclima.CidadesMaisQuentes = new List<CidadeClimaVM>() {
                new CidadeClimaVM(),
                new CidadeClimaVM(),
                new CidadeClimaVM()
            };



            previsaoclima.CidadesMaisFrias = new List<CidadeClimaVM>(){
                new CidadeClimaVM(),
                new CidadeClimaVM(),
                new CidadeClimaVM()
            };

            for (var i = 0; i < 3; i++){
                previsaoclima.CidadesMaisQuentes[i].Cidade = MaisQuentes[i].Cidade.Nome;
                previsaoclima.CidadesMaisQuentes[i].TemperaturaMaxima = Convert.ToInt32(MaisQuentes[i].TemperaturaMaxima);
                previsaoclima.CidadesMaisQuentes[i].TemperaturaMinima = Convert.ToInt32(MaisQuentes[i].TemperaturaMinima);
                previsaoclima.CidadesMaisQuentes[i].EstadoUF = MaisQuentes[i].Cidade.Estado.Uf;

                previsaoclima.CidadesMaisFrias[i].Cidade = MaisFrias[i].Cidade.Nome;
                previsaoclima.CidadesMaisFrias[i].TemperaturaMaxima = Convert.ToInt32(MaisFrias[i].TemperaturaMaxima);
                previsaoclima.CidadesMaisFrias[i].TemperaturaMinima = Convert.ToInt32(MaisFrias[i].TemperaturaMinima);
                previsaoclima.CidadesMaisFrias[i].EstadoUF = MaisFrias[i].Cidade.Estado.Uf;

            }
            previsaoclima.Cidades = new SelectList(db.Cidade, "Id", "Nome");

       
 

            return View(previsaoclima);
        }

        public PartialViewResult GetCidadeClimas(int cidadeId)
        {
            var Dia = DefinirDia();
            var PrevisoesProxDias = new List<CidadeClimaVM>() {
               new CidadeClimaVM(),
               new CidadeClimaVM(),
               new CidadeClimaVM(),
               new CidadeClimaVM(),
               new CidadeClimaVM(),
               new CidadeClimaVM(),
               new CidadeClimaVM()
            };
            var Cidade = db.PrevisaoClima.Where(c => c.CidadeId == cidadeId && c.DataPrevisao > Dia).ToList();

            for (var i = 0; i < 7; i++){
                PrevisoesProxDias[i].TemperaturaMaxima = Convert.ToInt32(Cidade[i].TemperaturaMaxima);
                PrevisoesProxDias[i].TemperaturaMinima = Convert.ToInt32(Cidade[i].TemperaturaMinima);
                PrevisoesProxDias[i].Clima = Cidade[i].Clima;
                PrevisoesProxDias[i].DiaSemana = Cidade[i].DataPrevisao.ToString("dddd", new CultureInfo("pt-BR"));
            }
                return PartialView("_PrevisoesDias", PrevisoesProxDias);
        }

        private DateTime DefinirDia()
        {
            var Dia = DateTime.Today;
            return Dia;
        }

        // GET: PrevisaoClimas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrevisaoClima previsaoClima = db.PrevisaoClima.Find(id);
            if (previsaoClima == null)
            {
                return HttpNotFound();
            }
            return View(previsaoClima);
        }

        // GET: PrevisaoClimas/Create
        public ActionResult Create()
        {
            ViewBag.CidadeId = new SelectList(db.Cidade, "Id", "Nome");
            return View();
        }

        // POST: PrevisaoClimas/Create
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public ActionResult Create([Bind(Include = "CidadeId,DataPrevisao,Clima,TemperaturaMinima,TemperaturaMaxima")] PrevisaoClima previsaoClima)
        {
            if (ModelState.IsValid)
            {
                var cidade = db.Cidade.Single(x => x.Id == previsaoClima.CidadeId);
                previsaoClima.CidadeId = cidade.Id;
                db.PrevisaoClima.Add(previsaoClima);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.CidadeId = new SelectList(db.Cidade, "Id", "Nome", previsaoClima.CidadeId);
            return View(previsaoClima);
        }

        // GET: PrevisaoClimas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrevisaoClima previsaoClima = db.PrevisaoClima.Find(id);
            if (previsaoClima == null)
            {
                return HttpNotFound();
            }
            ViewBag.CidadeId = new SelectList(db.Cidade, "Id", "Nome", previsaoClima.CidadeId);
            return View(previsaoClima);
        }

        // POST: PrevisaoClimas/Edit/5
        // Para se proteger de mais ataques, habilite as propriedades específicas às quais você quer se associar. Para 
        // obter mais detalhes, veja https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,CidadeId,DataPrevisao,Clima,TemperaturaMinima,TemperaturaMaxima")] PrevisaoClima previsaoClima)
        {
            if (ModelState.IsValid)
            {
                db.Entry(previsaoClima).State = Microsoft.EntityFrameworkCore.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.CidadeId = new SelectList(db.Cidade, "Id", "Nome", previsaoClima.CidadeId);
            return View(previsaoClima);
        }

        // GET: PrevisaoClimas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PrevisaoClima previsaoClima = db.PrevisaoClima.Find(id);
            if (previsaoClima == null)
            {
                return HttpNotFound();
            }
            return View(previsaoClima);
        }

        // POST: PrevisaoClimas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PrevisaoClima previsaoClima = db.PrevisaoClima.Find(id);
            db.PrevisaoClima.Remove(previsaoClima);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
