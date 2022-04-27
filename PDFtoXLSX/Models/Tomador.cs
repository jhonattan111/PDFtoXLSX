using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFtoXLSX.Models
{
    public class Tomador
    {
        public Tomador(string nome, string cpfcnpj)
        {
            Nome = nome;
            CPFCNPJ = cpfcnpj;
        }

        public string Nome { get; set; }
        public string CPFCNPJ { get; set; }
    }
}
