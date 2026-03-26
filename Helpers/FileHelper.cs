namespace Pdf2Xls.Helpers;

public static class FileHelper
{
	public static List<string> ObterPdfs(string pasta)
	{
		if (!Directory.Exists(pasta))
			return new List<string>();

		return Directory.GetFiles(pasta, "*.pdf", SearchOption.TopDirectoryOnly).ToList();
	}
}