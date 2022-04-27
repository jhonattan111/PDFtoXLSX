using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFtoXLSX.Models
{
    public class PdfPage
    {
        public string content { get; set; }
        public List<PdfRow> rows { get; set; }
    }
}
