using Pdf2Xls.UI;

namespace Pdf2Xls;

internal static class Program
{
	[STAThread]
	static void Main()
	{
		ApplicationConfiguration.Initialize();
		Application.Run(new MainForm());
	}
}