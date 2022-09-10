using financeiro.Data;
using financeiro.Models;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace financeiro.Controllers
{
    public class DespesaController : Controller
    {
       
        private readonly IFinanca _dal;
        public DespesaController(IFinanca dal)
        {
            _dal = dal;
        }
        [HttpGet]
        public IActionResult Index(string criterio)
        {
            var lstDespesas = _dal.GetAllDespesas().ToList();
            if (!String.IsNullOrEmpty(criterio))
            {
                lstDespesas = _dal.GetFiltrarDespesas(criterio).ToList();
            }
            return View(lstDespesas);
        }
        public ActionResult AddEditDespesa(int itemId)
        {
            Relatoriodespesa model = new Relatoriodespesa();
            if (itemId > 0)
            {
                model = _dal.GetDespesa(itemId);
            }
            return PartialView("_despesaForm", model);
        }
        [HttpPost]
        public ActionResult Create(Relatoriodespesa novaDespesa)
        {
            if (ModelState.IsValid)
            {
                if (novaDespesa.ItemId > 0)
                {
                    _dal.UpdateDespesa(novaDespesa);
                }
                else
                {
                    _dal.AddDespesa(novaDespesa);
                }
            }
            return RedirectToAction("Index");
        }
        [HttpPost]
        public IActionResult Delete(int id)
        {
            _dal.DeleteDespesa(id);
            return RedirectToAction("Index");
        }
        public ActionResult DespesaResumo()
        {
            return PartialView("_despesaReport");
        }
        public JsonResult GetDepesaPorPeriodo()
        {
            Dictionary<string, decimal> despesaPeriodo = _dal.CalcularDespesaPorPeriodo(7);
            return new JsonResult(despesaPeriodo);
        }
        public JsonResult GetDepesaPorPeriodoSemanal()
        {
            Dictionary<string, decimal> despesaPeriodoSemanal = _dal.CalcularDespesaPorPeriodoSemanal(7);
            return new JsonResult(despesaPeriodoSemanal);
        }
    }
}
