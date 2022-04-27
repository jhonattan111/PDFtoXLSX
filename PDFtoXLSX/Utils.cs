using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFtoXLSX
{
    public static class Utils
    {
        public static int RetornarUltimosValoresIndice(this string conteudo, string pattern, int posicao = 0)
        {
            string resultado = conteudo;
            string preResultado = conteudo;

            for (int i = 0; i >= posicao; i--)
            {
                var posicaoUltimo = preResultado.LastIndexOf(pattern);
                preResultado = conteudo.Substring(0, conteudo.Length - (conteudo.Length - posicaoUltimo));
            }

            return preResultado.Length;
        }

        public static int RetornarPrimeirosValoresIndice(this string conteudo, string pattern, int posicao = 0)
        {
            string resultado = conteudo;
            string preResultado = conteudo;

            for (int i = 0; i <= posicao; i++)
            {
                var posicaoPrimeiro = preResultado.IndexOf(pattern);
                preResultado = conteudo.Substring(posicaoPrimeiro);
            }

            return preResultado.Length;
        }
    }
}
