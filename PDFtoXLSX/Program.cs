using PDFtoXLSX;

var caminhoRaiz = @"C:\Users\55229\Desktop\nfs";

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

Console.ReadKey();