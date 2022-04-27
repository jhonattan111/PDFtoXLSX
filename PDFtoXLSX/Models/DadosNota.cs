using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFtoXLSX.Models
{
    public class DadosNota
    {
        public DadosNota()
        {
            Itens = new List<DadosNotaItem>();
        }

        public string Numero { get; set; }
        public DateTime DataEmissao { get; set; }
        public Prestador Prestador { get; set; }
        public Tomador Tomador { get; set; }
        public Informacoes Informacoes { get; set; }
        public List<DadosNotaItem> Itens { get; set; }
    }

    public class Informacoes
    {
        public Informacoes() { }
        public Informacoes(bool retido, decimal inss, decimal pispasep, decimal cofins, decimal ir, decimal csll, decimal aliquotaiss, decimal baseCalculo, decimal totaliss, decimal valorLiquido)
        {
            Retido = retido;
            INSS = inss;
            PISPASEP = pispasep;
            COFINS = cofins;
            IR = ir;
            CSLL = csll;
            AliquotaISS = aliquotaiss;
            BaseCalculo = baseCalculo;
            TotalISS = totaliss;
            ValorLiquido = valorLiquido;

        }

        public bool Retido { get; set; }
        public decimal INSS { get; set; }
        public decimal PISPASEP { get; set; }
        public decimal COFINS { get; set; }
        public decimal IR { get; set; }
        public decimal CSLL { get; set; }
        public decimal AliquotaISS{ get; set; }
        public decimal BaseCalculo { get; set; }
        public decimal TotalISS { get; set; }
        public decimal ValorLiquido { get; set; }
    }
}