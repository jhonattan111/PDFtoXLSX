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
        public void ExtrairDados(Models.PdfPage pagina)
        {
            var numeroNota = ExtrairNumeroNota(pagina.content);
            var dataEmissao = ExtrairDataEmissaoNota(pagina.content);
            var prestador = ExtrairDadosPrestador(pagina.content);
            var tomador = ExtrairDadosTomador(pagina.content);
            //var itens = ExtrairDadosItens(pagina.content);
            var informacoes = ExtrairDadosInformacoes(pagina.content);


            var dados = new DadosNota()
            {
                Numero = numeroNota,
                DataEmissao = dataEmissao,
                Prestador = prestador,
                Tomador = tomador,
                //Itens = itens,
                Informacoes = informacoes,
            };

            NotasFiscaisServico.Add(dados);
        }

        private string ExtrairNumeroNota(string conteudo)
        {
            string rgxDados = "(?<=\\n NOTA FISCAL\\n)(.*)(?=\\nPREFEITURA DE PETRÓPOLIS)";

            return ExtrairConteudo(conteudo, rgxDados);
        }

        private DateTime ExtrairDataEmissaoNota(string conteudo)
        {
            string rgxDados = "(?<=\\n DATA DE EMISSÃO NOTA\\n)(.*)(?=\\nNOTA FISCAL IMPERIAL\\n)";
            string DataEmissaoSTR = ExtrairConteudo(conteudo, rgxDados);
            DateTime DataEmissao = new DateTime();
            DateTime.TryParse(DataEmissaoSTR, out DataEmissao);

            return DataEmissao;
        }

        private Prestador ExtrairDadosPrestador(string conteudo)
        {

            
            string pos1 = @"PRESTADOR DE SERVIÇOS";
            string pos2 = @"TOMADOR DE SERVIÇOS";

            conteudo = ExtrairConteudo(conteudo, pos1, pos2);

            //var inicioConteudo = conteudo.IndexOf(pos1);
            //var fimConteudo = conteudo.IndexOf(pos2);
            //
            //conteudo = conteudo.Substring(inicioConteudo, fimConteudo - inicioConteudo);

            string rgxRazaoSocial = "(?<=\\n RAZÃO SOCIAL PRESTADOR NOME FANTASIA PRESTADOR\\n)(.*)(?=\\n ENDEREÇO COMPLEMENTO\\n)";
            string razaoSocial = ExtrairConteudo(conteudo, rgxRazaoSocial);

            var prestador = new Prestador(razaoSocial);

            return prestador;
        }

        private Tomador ExtrairDadosTomador(string conteudo)
        {
            string pos1 = @"TOMADOR DE SERVIÇOS";
            string pos2 = @"DISCRIMINAÇÃO DOS SERVIÇOS";

            conteudo = ExtrairConteudo(conteudo, pos1, pos2);

            //var inicioConteudo = conteudo.IndexOf(pos1);
            //var fimConteudo = conteudo.IndexOf(pos2);
            //
            //conteudo = conteudo.Substring(inicioConteudo, fimConteudo - inicioConteudo);

            var rgxNome = "(?<=\\n NOME DO TOMADOR\\n)(.*)(?=\\n ENDEREÇO COMPLEMENTO\\n)";
            var nome = ExtrairConteudo(conteudo, rgxNome);

            var rgxCPFCNPJ = "(?<=TELEFONE E-MAIL\\n)(.*)";
            var cpfcnpj = ExtrairConteudo(conteudo, rgxCPFCNPJ);

            var rgxModelocpfcnpj = @"([\d]{2}\.[\d]{3}\.[\d]{3}\/[\d]{4}\-[\d]{2})|([\d]{3}\.[\d]{3}\.[\d]{3}\-[\d]{2})";

            if (!Regex.Match(conteudo, rgxModelocpfcnpj).Success)
                cpfcnpj = cpfcnpj.Substring(0, cpfcnpj.IndexOf(' '));

            var tomador = new Tomador(nome, cpfcnpj);

            return tomador;
        }

        private Informacoes ExtrairDadosInformacoes(string conteudo)
        {
            string pos1 = @"IMPOSTOS FEDERAIS  IMPOSTOS MUNICIPAIS";
            string pos2 = @"DESCRIÇÃO DA ATIVIDADE DA PRESTAÇÃO";

            conteudo = ExtrairConteudo(conteudo, pos1, pos2);

            var pos3 = Utils.RetornarUltimosValoresIndice(conteudo, "\n", -2);
            var valoresImposto = conteudo.Substring(pos3);
            valoresImposto = valoresImposto.Replace(" %", " ").Replace("\n", " ").Trim();

            var vi = valoresImposto.Split(" ").Where(d => !string.IsNullOrWhiteSpace(d)).ToArray();

            var retido = vi[8].ToBool();

            decimal aliquotaiss = 0m;
            decimal.TryParse(vi[5], out aliquotaiss);

            decimal basecalculo = 0m;
            decimal.TryParse(vi[6], out basecalculo);

            decimal totaliss = 0m;
            decimal.TryParse(vi[7], out totaliss);

            decimal valorliquido = 0m;
            decimal.TryParse(vi[9], out valorliquido);

            var info = new Informacoes(retido, 0, 0, 0, 0, 0, aliquotaiss, basecalculo, totaliss, valorliquido);

            return info;
        }

        private List<DadosNotaItem> ExtrairDadosItens(string conteudo)
        {
            string pos1 = @"DISCRIMINAÇÃO DOS SERVIÇOS";
            string pos2 = @"OBSERVAÇÕES";

            conteudo = ExtrairConteudo(conteudo, pos1, pos2);

            //var inicioConteudo = conteudo.IndexOf(pos1);
            //var fimConteudo = conteudo.IndexOf(pos2);
            //
            //conteudo = conteudo.Substring(inicioConteudo, fimConteudo - inicioConteudo);

            var layout = ExtrairlayoutServicos(conteudo);

            if(layout == ELayout.NaoRetido)
            {
                var notas = ExtrairDadosServicoNaoRetido(conteudo);

                return notas;
            }

            if(layout == ELayout.Retido)
            {
                string dadosServico = "";

                var nota = ExtrairDadosServicoRetido(conteudo);
            }

            return new List<DadosNotaItem>();
        }

        private List<DadosNotaItem> ExtrairDadosServicoRetido(string conteudo)
        {

            return new List<DadosNotaItem>();
        }

        private List<DadosNotaItem> ExtrairDadosServicoNaoRetido(string conteudo)
        {
            string pos1 = "UNID QUANT. DESCRIÇÃO DO SERVIÇO VALOR UNIT. VALOR TOTAL";
            string pos2 = "\n     \n";

            conteudo = ExtrairConteudo(conteudo, pos1, pos2);

            var dadosItens = new List<DadosNotaItem>();

            var itens = conteudo.Split('\n').Where(d => !string.IsNullOrWhiteSpace(d)).ToList();
            var rgxDescricao = @"(?<=[\d]  )(.*)(?=  [\d])";

            foreach (var item in itens)
            {
                string conteudoValor = item.Substring(item.RetornarUltimosValoresIndice("  ", -1));
                string conteudoDescicao = ExtrairConteudo(item, rgxDescricao);
                string[] cv = conteudoValor.Trim().Split(' ').Where(d => !string.IsNullOrWhiteSpace(d)).ToArray();
                List<decimal> valores = new List<decimal>();
                
                foreach (var c in cv)
                {
                    decimal v = 0m;

                    var d = c.Replace(".", ",");

                    decimal.TryParse(d, out v);

                    valores.Add(v);

                }

                var itemNFS = new DadosNotaItem(0, conteudoDescicao, valores[0], 0, 0, valores[1], 0, false); 

                dadosItens.Add(itemNFS);
            }

            return dadosItens;
        }

        private ELayout ExtrairlayoutServicos(string conteudo)
        {
            string rgxLayout = "(?<=\\n)(.*)";

            var layout = ExtrairConteudo(conteudo, rgxLayout);

            if (layout.Contains("ALÍQUOTA"))
                return ELayout.Retido;

            return ELayout.NaoRetido;
        }

        private string ExtrairConteudo(string informacoes, string pattern)
        {
            string conteudo = Regex.Match(informacoes, pattern).ToString();

            return conteudo.Trim();
        }

        private string ExtrairConteudo(string informacoes, string pos1, string pos2)
        {
            var inicioConteudo = informacoes.IndexOf(pos1) + pos1.Length;
            var fimConteudo = informacoes.IndexOf(pos2);

            informacoes = informacoes.Substring(inicioConteudo, fimConteudo - inicioConteudo);
            return informacoes;
        }
    }
}
