using PDFtoXLSX;

var caminhoRaiz = @"C:\Users\jhona\Desktop\notas";

var leitor = new LeitorPDF(caminhoRaiz);

leitor.LerPasta();
leitor.LerArquivosPDF();

foreach(var documento in leitor.Conteudo)
{
    foreach(var pagina in documento)
    {
        Console.WriteLine(pagina.content);
    }
}

Console.ReadKey();