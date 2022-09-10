using financeiro.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace financeiro.Data
{
    public class Financa : IFinanca
    {
      
        private readonly AppDbContext db; // database arquivo de banco de dados que foi gerado pelo migrations so leitura
        
        public Financa(AppDbContext context) // construtor 
        {
            db = context;
        }
        
        public IEnumerable<Relatoriodespesa> GetAllDespesas()
        {
            try
            {
                
                return db.relatoriodespesas.ToList();
            }
            catch { throw; }
        }
        public IEnumerable<Relatoriodespesa> GetFiltrarDespesas(string criterio)
        {
            List<Relatoriodespesa> desp = new List<Relatoriodespesa>();
            try
            {
                desp = GetAllDespesas().ToList();
                return desp.Where(x => x.ItemNome.IndexOf(criterio, StringComparison.OrdinalIgnoreCase) != -1);

            }
            catch { throw; }
        }

        public void AddDespesa(Relatoriodespesa despesa)
        {
            try
            {
                db.relatoriodespesas.Add(despesa);
                db.SaveChanges();
            }
            catch { throw; }
        }

        public int UpdateDespesa(Relatoriodespesa despesa)
        {
            try
            {
                db.Entry(despesa).State = EntityState.Modified;

                db.SaveChanges();
                return 1;
            }
            catch { throw; }
        }
        public Relatoriodespesa GetDespesa(int id)
        {
            try
            {
                Relatoriodespesa despesa = db.relatoriodespesas.Find(id);
                return despesa;

            }
            catch { throw; }
        }
        public void DeleteDespesa(int id)
        {
            try
            {
                Relatoriodespesa desp = db.relatoriodespesas.Find(id);
                db.relatoriodespesas.Remove(desp);
                db.SaveChanges();

            }
            catch { throw; }
        }
        public Dictionary<string, decimal> CalcularDespesaPorPeriodo(int periodo)
        {
            Dictionary<string, decimal> SomaDespesasPeriodo = new Dictionary<string, decimal>();

            decimal despAlimentaçao = db.relatoriodespesas.Where(cat => cat.Categoria == "Alimentação" && (cat.DataDespesa >
            DateTime.Now.AddMonths(-periodo))).Select(cat => cat.Valor).Sum();


            decimal despCompras = db.relatoriodespesas.Where(cat => cat.Categoria == "Compras" && (cat.DataDespesa >
            DateTime.Now.AddMonths(-periodo))).Select(cat => cat.Valor).Sum();


            decimal despTransporte = db.relatoriodespesas.Where(cat => cat.Categoria == "Transporte" && (cat.DataDespesa >
            DateTime.Now.AddMonths(-periodo))).Select(cat => cat.Valor).Sum();


            decimal despSaude = db.relatoriodespesas.Where(cat => cat.Categoria == "Saude" && (cat.DataDespesa >
            DateTime.Now.AddMonths(-periodo))).Select(cat => cat.Valor).Sum();


            decimal despMoradia = db.relatoriodespesas.Where(cat => cat.Categoria == "Moradia" && (cat.DataDespesa >
            DateTime.Now.AddMonths(-periodo))).Select(cat => cat.Valor).Sum();

            decimal despLazer = db.relatoriodespesas.Where(cat => cat.Categoria == "Lazer" && (cat.DataDespesa >
            DateTime.Now.AddMonths(-periodo))).Select(cat => cat.Valor).Sum();


            SomaDespesasPeriodo.Add("Alimentação", despAlimentaçao);
            SomaDespesasPeriodo.Add("Compras", despCompras);
            SomaDespesasPeriodo.Add("Transporte", despTransporte);
            SomaDespesasPeriodo.Add("Saude", despSaude);
            SomaDespesasPeriodo.Add("Moradia", despMoradia);
            SomaDespesasPeriodo.Add("Lazer", despLazer);


            return SomaDespesasPeriodo;

        }

        public Dictionary<string, decimal> CalcularDespesaPorPeriodoSemanal(int periodo)
        {
            Dictionary<string, decimal> SomaDespesasPeriodoSemanal = new Dictionary<string, decimal>();

            decimal despAlimentaçao = db.relatoriodespesas.Where(cat => cat.Categoria == "Alimentação" && (cat.DataDespesa >
            DateTime.Now.AddDays(-periodo))).Select(cat => cat.Valor).Sum();


            decimal despCompras = db.relatoriodespesas.Where(cat => cat.Categoria == "Compras" && (cat.DataDespesa >
            DateTime.Now.AddDays(-periodo))).Select(cat => cat.Valor).Sum();


            decimal despTransporte = db.relatoriodespesas.Where(cat => cat.Categoria == "Transporte" && (cat.DataDespesa >
            DateTime.Now.AddDays(-periodo))).Select(cat => cat.Valor).Sum();


            decimal despSaude = db.relatoriodespesas.Where(cat => cat.Categoria == "Saude" && (cat.DataDespesa >
            DateTime.Now.AddDays(-periodo))).Select(cat => cat.Valor).Sum();


            decimal despMoradia = db.relatoriodespesas.Where(cat => cat.Categoria == "Moradia" && (cat.DataDespesa >
            DateTime.Now.AddDays(-periodo))).Select(cat => cat.Valor).Sum();

            decimal despLazer = db.relatoriodespesas.Where(cat => cat.Categoria == "Lazer" && (cat.DataDespesa >
            DateTime.Now.AddDays(-periodo))).Select(cat => cat.Valor).Sum();


            SomaDespesasPeriodoSemanal.Add("Alimentação", despAlimentaçao);
            SomaDespesasPeriodoSemanal.Add("Compras", despCompras);
            SomaDespesasPeriodoSemanal.Add("Transporte", despTransporte);
            SomaDespesasPeriodoSemanal.Add("Saude", despSaude);
            SomaDespesasPeriodoSemanal.Add("Moradia", despMoradia);
            SomaDespesasPeriodoSemanal.Add("Lazer", despLazer);

            return SomaDespesasPeriodoSemanal;
        }

    }
}

