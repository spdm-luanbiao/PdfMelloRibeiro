using Pdf2Xls.Models;
using Pdf2Xls.Services;

namespace Pdf2Xls.Core;

public class ProcessadorPdf
{
	private readonly PdfReaderService _reader = new();
	private readonly TableParserService _parser = new();

	public List<LinhaTabela> Processar(string caminhoPdf)
	{
		var texto = _reader.LerTexto(caminhoPdf);
		return _parser.Parse(texto);
	}
}