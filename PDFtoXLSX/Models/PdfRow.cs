using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFtoXLSX.Models
{
    public class PdfRow
    {
        public string content { get; set; }
        public List<string> words { get; set; }
    }
}
