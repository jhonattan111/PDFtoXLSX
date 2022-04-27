using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFtoXLSX.Models
{
    public class Prestador
    {
        public Prestador(string razaoSocial)
        {
            RazaoSocial = razaoSocial;
        }
        public string RazaoSocial { get; set; }
    }
}
