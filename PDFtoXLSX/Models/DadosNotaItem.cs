namespace PDFtoXLSX.Models
{
    public class DadosNotaItem
    {
        public DadosNotaItem(int quantidade, string descricao, decimal valorUnitario, decimal aliquota, decimal valorISS, decimal valorTotal, decimal valorLiquido, bool retido)
        {
            Quantidade = quantidade;
            Descricao = descricao;
            ValorUnitario = valorUnitario;
            Aliquota = aliquota;
            ValorISS = valorISS;
            ValorTotal = valorTotal;
            ValorLiquido = valorLiquido;
            Retido = retido;
        }


        public int Quantidade { get; set; }
        public string Descricao { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Aliquota { get; set; }
        public decimal ValorISS { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorLiquido { get; set; }
        public bool Retido { get; set; }
    }
}