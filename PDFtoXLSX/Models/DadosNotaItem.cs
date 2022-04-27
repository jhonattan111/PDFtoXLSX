namespace PDFtoXLSX.Models
{
    public class DadosNotaItem
    {
        public int Quantidade { get; set; }
        public string Descricao { get; set; }
        public decimal ValorUnitario { get; set; }
        public decimal Aliquota { get; set; }
        public decimal ValorISS { get; set; }
        public decimal ValorTotal { get; set; }
        public decimal ValorLiquido { get; set; }
        public bool NotaAnteriorRetida { get; set; }
    }
}