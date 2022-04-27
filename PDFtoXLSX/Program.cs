using Ganss.Excel;
using PDFtoXLSX;

var caminhoRaiz = @"G:\Documentos\Funcionários\Jessica Amaduro\NOTAS ARMAZÉM";

var leitor = new LeitorPDF(caminhoRaiz);

leitor.LerPasta();
leitor.LerArquivosPDF();

foreach(var documento in leitor.Conteudo)
{
    foreach(var pagina in documento)
    {
        leitor.ExtrairDados(pagina);
    }
}

foreach(var nota in leitor.NotasFiscaisServico)
{
    Console.WriteLine(nota.Itens);
}

new ExcelMapper().Save("notas.xlsx", leitor.NotasFiscaisServico, "Notas Fiscais");

Console.ReadKey();