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
        public List<DadosNotaItem> Itens { get; set; }
    }
}