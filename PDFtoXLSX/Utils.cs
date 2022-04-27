using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PDFtoXLSX
{
    public static class Utils
    {
        public static int RetornarUltimosValoresIndice(this string conteudo, string padrao, int posicao = 0)
        {
            string resultado = conteudo;

            for (int i = 0; i >= posicao; i--)
            {
                var posicaoUltimo = resultado.LastIndexOf(padrao);
                resultado = conteudo.Substring(0, conteudo.Length - (conteudo.Length - posicaoUltimo));
            }

            return resultado.Length;
        }

        public static int RetornarPrimeirosValoresIndice(this string conteudo, string padrao, int posicao = 0)
        {
            string resultado = conteudo;

            for (int i = 0; i <= posicao; i++)
            {
                var posicaoPrimeiro = resultado.IndexOf(padrao);
                resultado = conteudo.Substring(posicaoPrimeiro);
            }

            return resultado.Length;
        }

        public static bool ToBool(this string e)
        {
            if (e.ToUpper() == "SIM") return true;

            return false;
        }
    }
}
