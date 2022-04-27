using iTextSharp.text.pdf;
using PDFtoXLSX.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PDFtoXLSX
{
    public partial class LeitorPDF
    {
        public DadosNota ExtrairDados(Models.PdfPage pagina)
        {
            var numeroNota = ExtrairNumeroNota(pagina.content);
            var dataEmissao = ExtrairDataEmissaoNota(pagina.content);
            var prestador = ExtrairDadosPrestador(pagina.content);
            var tomador = ExtrairDadosTomador(pagina.content);


            var dados = new DadosNota()
            {
                Numero = numeroNota,
                DataEmissao = dataEmissao,
                Prestador = prestador,
                Tomador = tomador,
            };

            return dados;
        }

        public string ExtrairNumeroNota(string conteudo)
        {
            string rgxDados = "(?<=\\n NOTA FISCAL\\n)(.*)(?=\\nPREFEITURA DE PETRÓPOLIS)";

            return ExtrairConteudo(conteudo, rgxDados);
        }
        
        public DateTime ExtrairDataEmissaoNota(string conteudo)
        {
            string rgxDados = "(?<=\\n DATA DE EMISSÃO NOTA\\n)(.*)(?=\\nNOTA FISCAL IMPERIAL\\n)";

            string DataEmissao = ExtrairConteudo(conteudo, rgxDados);

            var data = DateTime.Parse(DataEmissao);

            return data;
        }

        public Prestador ExtrairDadosPrestador(string conteudo)
        {
            string rgxDados = "(?<=\\n RAZÃO SOCIAL PRESTADOR NOME FANTASIA PRESTADOR\\n)(.*)(?=\\nTOMADOR DE SERVIÇOS\\n NOME DO TOMADOR\\n)";

            var dadosPrestador = ExtrairConteudo(conteudo, rgxDados);

            string rgxRazaoSocial = "(?<=\\n RAZÃO SOCIAL PRESTADOR NOME FANTASIA PRESTADOR\\n)(.*)(?=\\n ENDEREÇO COMPLEMENTO\\n)";
            string razaoSocial = ExtrairConteudo(dadosPrestador, rgxRazaoSocial);

            var prestador = new Prestador(razaoSocial);

            return prestador;
        }

        public Tomador ExtrairDadosTomador(string conteudo)
        {
            string rgxDados = "(?<=\\nTOMADOR DE SERVIÇOS\\n NOME DO TOMADOR\\n)(.*)(?=\\nDISCRIMINAÇÃO DOS SERVIÇOS\\n)";

            var dadosTomador = ExtrairConteudo(conteudo, rgxDados);

            var rgxNome = "(?<=\\n NOME DO TOMADOR\\n)(.*)(?=\\n ENDEREÇO COMPLEMENTO\\n)";
            var nome = ExtrairConteudo(dadosTomador, rgxNome);

            var rgxCPFCNPJ = "(?<=TELEFONE E-MAIL\\n)(.*)";
            var cpfcnpj = ExtrairConteudo(dadosTomador, rgxCPFCNPJ);
            cpfcnpj = cpfcnpj.Substring(0, cpfcnpj.IndexOf(' '));

            var tomador = new Tomador(nome, cpfcnpj);

            return tomador;
        }

        public string ExtrairDadosServicos(string conteudo)
        {
            string rgxDados = "(?<=\\nDISCRIMINAÇÃO DOS SERVIÇOS\\n)(.*)(?=OBSERVAÇÕES(.*)IMPOSTOS FEDERAIS  IMPOSTOS MUNICIPAIS)";

            return ExtrairConteudo(conteudo, rgxDados);
        }

        private string ExtrairConteudo(string informacoes, string pattern)
        {
            string conteudo = Regex.Match(informacoes, pattern).ToString();

            return conteudo.Trim();
        }
    }
}
