using financeiro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace financeiro.Data
{
    public interface IFinanca
    {
        IEnumerable<Relatoriodespesa> GetAllDespesas(); //pega todas as despesas
        IEnumerable<Relatoriodespesa> GetFiltrarDespesas(string criterio); //filtra todas as despesas
        void AddDespesa(Relatoriodespesa despesa);// addiciona uma nova despesa
        int UpdateDespesa(Relatoriodespesa despesa); //atualiza a despesa
        Relatoriodespesa GetDespesa(int id); //gera o relatorio
        void DeleteDespesa(int id); //Deleta a despesa
        Dictionary<string, decimal> CalcularDespesaPorPeriodo(int periodo); // calcula por periodo
        Dictionary<string, decimal> CalcularDespesaPorPeriodoSemanal(int periodo);// calcula por periodo semanal

    }
}
