using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;
using PDFtoXLSX;
using PDFtoXLSX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFtoXLSX
{
    public partial class LeitorPDF
    {
        public LeitorPDF(string pastaRaiz)
        {
            log = new List<string>();
            Conteudo = new List<List<Models.PdfPage>>();
            CaminhoArquivos = new List<string>();
            _pastaRaiz = pastaRaiz;
            NotasFiscaisServico = new List<DadosNota>();
        }

        private string _pastaRaiz {get; set;}
        public List<string> log { get; set; }
        public List<string> CaminhoArquivos { get; set; }
        public List<List<Models.PdfPage>> Conteudo { get; set; }
        public List<DadosNota> NotasFiscaisServico { get; set; }

        private void LerPDF(string caminho)
        {
            if(File.Exists(caminho))
            {
                using (PdfReader reader = new PdfReader(caminho))
                {
                    for (int i = 1; i <= reader.NumberOfPages; i++)
                    {
                        var pages = new List<Models.PdfPage>();

                        pages.Add(new Models.PdfPage()
                        {
                            content = PdfTextExtractor.GetTextFromPage(reader, i)
                        });

                        Conteudo.Add(pages);
                    }
                }
            }
            else
            {
                log.Add($"Arquivo {caminho} não existe");
            }
        }

        public void LerPasta()
        {
            if(Directory.Exists(_pastaRaiz))
            {
                CaminhoArquivos.AddRange(Directory.GetFiles(_pastaRaiz, "*.pdf"));

                var diretorios = Directory.GetDirectories(_pastaRaiz);

                foreach(var diretorio in diretorios)
                {
                    CaminhoArquivos.AddRange(Directory.GetFiles(diretorio, "*.pdf"));
                }
            }
            else
            {
                log.Add($"Pasta {_pastaRaiz} não existe");
            }
        }

        public void LerArquivosPDF()
        {
            foreach(var caminhoArquivo in CaminhoArquivos)
            {
                LerPDF(caminhoArquivo);
            }
        }
    }
}
