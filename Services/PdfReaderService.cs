using UglyToad.PdfPig;

namespace Pdf2Xls.Services;

public class PdfReaderService
{
	public string LerTexto(string caminhoPdf)
	{
		using var document = PdfDocument.Open(caminhoPdf);

		var texto = "";

		foreach (var page in document.GetPages())
		{
			texto += page.Text + "\n";
		}

		return texto;
	}
}